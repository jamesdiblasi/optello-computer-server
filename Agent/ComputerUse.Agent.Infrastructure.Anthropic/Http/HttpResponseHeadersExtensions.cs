using System;
using System.Net.Http.Headers;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Http
{
    internal static class HttpResponseHeadersExtensions
    {
        internal const string HEADER_X_SHOULD_RETRY = "x-should-retry";
        internal const string HEADER_RETRY_AFTER_MS = "retry-after-ms";
        internal const string HEADER_RETRY_AFTER = "retry-after";

        public static bool? GetXShouldRetry(this HttpResponseHeaders source)
        {
            if (!source.TryGetValues(HEADER_X_SHOULD_RETRY, out var shouldRetryHeaderStrs))
            {
                return null;
            }

            var firstShouldRetryHeaderStr = shouldRetryHeaderStrs.First();

            return bool.TryParse(firstShouldRetryHeaderStr, out var shouldRetryHeader) ? shouldRetryHeader : null;
        }

        public static TimeSpan? GetRetryAfterMs(this HttpResponseHeaders source)
        {
            if (!source.TryGetValues(HEADER_RETRY_AFTER_MS, out var retryAfterMsStrs))
            {
                return null;
            }

            var firstRetryAfterMsStr = retryAfterMsStrs.First();

            return int.TryParse(firstRetryAfterMsStr, out var retryAfterMs) ? TimeSpan.FromMilliseconds(retryAfterMs) : null;
        }

        public static TimeSpan? GetRetryAfter(this HttpResponseHeaders source)
        {
            if (source.RetryAfter is not RetryConditionHeaderValue retryAfterHeaderValue)
            {
                return null;
            }

            TimeSpan? retryAfter;

            if (retryAfterHeaderValue.Date is DateTimeOffset date)
            {
                retryAfter = date - TimeProvider.System.GetUtcNow();
            }
            else
            {
                retryAfter = retryAfterHeaderValue.Delta;
            }

            if (retryAfter.HasValue && retryAfter < TimeSpan.Zero)
            {
                retryAfter = TimeSpan.Zero;
            }

            return retryAfter;
        }
    }
}
