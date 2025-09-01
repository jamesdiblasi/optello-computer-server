using Microsoft.Extensions.Logging;
using Polly.Retry;
using System.Collections.Immutable;
using System.Net;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Http
{
    internal class AnthropicRetryStrategyOptions : RetryStrategyOptions<HttpResponseMessage>
    {
        private const double INITIAL_RETRY_DELAY = 0.5;
        private const double MAX_RETRY_DELAY = 8.0;

        private static readonly ImmutableArray<HttpStatusCode> shouldRetryStatusCodes = [
            // Retry on request timeouts.
            HttpStatusCode.RequestTimeout,

            // Retry on lock timeouts.
            HttpStatusCode.Conflict,

            // Retry on rate limits.
            HttpStatusCode.TooManyRequests
        ];

        private readonly ILogger<AnthropicRetryStrategyOptions> logger;

        private ValueTask<bool> ShouldRetry(RetryPredicateArguments<HttpResponseMessage> args)
        {
            if (args.Outcome.Result is not HttpResponseMessage responseMessage)
            {
                return ValueTask.FromResult(false);
            }

            // Note: this is not a standard header
            var shouldRetryHeader = responseMessage.Headers.GetXShouldRetry();

            // If the server explicitly says whether or not to retry, obey.
            if (shouldRetryHeader.HasValue)
            {
                var shouldRetryHeaderValue = shouldRetryHeader.Value;
                logger?.LogDebug($"Retrying as header \"{HttpResponseHeadersExtensions.HEADER_X_SHOULD_RETRY}\" is set to \"{shouldRetryHeaderValue}\"");
                return ValueTask.FromResult(shouldRetryHeaderValue);
            }

            if (shouldRetryStatusCodes.Contains(responseMessage.StatusCode) || responseMessage.StatusCode >= HttpStatusCode.InternalServerError)
            {
                logger?.LogDebug($"Retrying due to status code \"{responseMessage.StatusCode}\"");
                return ValueTask.FromResult(true);
            }

            logger?.LogDebug($"Not retrying");
            return ValueTask.FromResult(false);
        }

        private ValueTask<TimeSpan?> CalculateRetryTimeout(RetryDelayGeneratorArguments<HttpResponseMessage> args) 
        {
            if (args.Outcome.Result is not HttpResponseMessage responseMessage)
            {
                return default;
            }

            // If the API asks us to wait a certain amount of time (and it's a reasonable amount), just do what it says.
            // Try the non-standard `retry-after-ms` header for milliseconds,
            // which is more precise than integer-seconds `retry-after`
            var retryAfter = responseMessage.Headers.GetRetryAfterMs();

            if (retryAfter.HasValue)
            {
                logger.LogDebug($"Use timeout from \"{HttpResponseHeadersExtensions.HEADER_RETRY_AFTER_MS}\" header. value=\"{retryAfter.Value.TotalSeconds}\"");
            } 
            else
            {
                retryAfter = responseMessage.Headers.GetRetryAfter();

                if (retryAfter.HasValue)
                {
                    logger.LogDebug($"Use timeout from \"{HttpResponseHeadersExtensions.HEADER_RETRY_AFTER}\" header. value=\"{retryAfter.Value.TotalSeconds}\"");
                }
            }
                

            if (retryAfter.HasValue)
            {
                if ((0 < retryAfter?.TotalSeconds) && (retryAfter?.TotalSeconds <= 60)) 
                { 
                    return ValueTask.FromResult(retryAfter); 
                } 
                else 
                {
                    logger.LogWarning($"retryAfter has value=\"{retryAfter?.TotalSeconds}\" that is more then 60 seconds. Use 60 by default.");
                    return new ValueTask<TimeSpan?>(TimeSpan.FromSeconds(60));
                }
            } 
            else
            {
                logger.LogWarning("retryAfter is EMPTY.");
            }

            // Also cap retry count to 1000 to avoid any potential overflows with `pow`
            var nbRetries = Math.Min(args.AttemptNumber, 1000);

            // Apply exponential backoff, but not more than the max.
            var sleepSeconds = Math.Min(INITIAL_RETRY_DELAY * Math.Pow(2.0, nbRetries), MAX_RETRY_DELAY);

            // Apply some jitter, plus - or - minus half a second.
            var jitter = 1.0 - 0.25 * Randomizer();
            var timeout = sleepSeconds * jitter;
            return timeout >= 0 ? new ValueTask<TimeSpan?>(TimeSpan.FromSeconds(timeout)) : default;
        }

        private ValueTask OnRetryHandler(OnRetryArguments<HttpResponseMessage> args)
        {
            //logger.LogDebug(args.O)
            return default;
        }

        public AnthropicRetryStrategyOptions(ILoggerFactory loggerFactory) : this(loggerFactory.CreateLogger<AnthropicRetryStrategyOptions>()) { }

        public AnthropicRetryStrategyOptions(ILogger<AnthropicRetryStrategyOptions> logger) {
            this.logger = logger;

            ShouldHandle = ShouldRetry;
            DelayGenerator = CalculateRetryTimeout;
            //OnRetry = OnRetryHandler;
        }
    }
}
