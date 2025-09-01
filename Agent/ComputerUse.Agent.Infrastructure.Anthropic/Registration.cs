using Anthropic.SDK;
using ComputerUse.Agent.Core;
using ComputerUse.Agent.Infrastructure.Anthropic.Http;
using ComputerUse.Agent.Infrastructure.Anthropic.Options;
using ComputerUse.Agent.Infrastructure.Anthropic.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    public static class Registration
    {
        public static IServiceCollection AddComputerUseAgentInfrastructureAnthropic(this IServiceCollection services)
        {
            services.AddOptions<ComputerUseInfrastructureAnthropicOptions>().BindConfiguration(nameof(ComputerUseInfrastructureAnthropicOptions));
            services.AddSingleton<CommandFactory>();
            services.AddSingleton<IAIClient, AnthropicAIClient>();
            services.AddTransient(provider =>
            {
                var options = provider.GetRequiredService<IOptions<ComputerUseInfrastructureAnthropicOptions>>().Value;
                return new APIAuthentication(options.ApiKey);
            });

            services.AddHttpClient<AnthropicClient>(cl =>
            {
                cl.Timeout = TimeSpan.FromMinutes(5);
            })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new SocketsHttpHandler()
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(15.0),
                        ConnectTimeout = TimeSpan.FromMinutes(5)
                    };
                })
                .AddAnthropicResilienceHandler();

            services.AddKeyedSingleton<IAnthropicTool, ComputerTool>(ComputerTool.TOOL_NAME);
            services.AddKeyedSingleton<IAnthropicTool, OpenBrowserTool>(OpenBrowserTool.TOOL_NAME);

            return services;
        }

        private static IHttpResiliencePipelineBuilder AddAnthropicResilienceHandler(this IHttpClientBuilder builder) {
            return builder.AddResilienceHandler("AnthropicPipeline", static (builder, context) =>
            {
                var loggerFactory = context.ServiceProvider.GetRequiredService<ILoggerFactory>();

                builder.AddRetry(new AnthropicRetryStrategyOptions(loggerFactory)
                {
                    MaxRetryAttempts = 4
                });
            });
        }
    }
}
