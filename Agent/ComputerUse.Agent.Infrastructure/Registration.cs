using Azure.Core;
using Azure.ResourceManager.AppContainers;
using ComputerUse.Agent.Core;
using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Infrastructure.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ComputerUse.Agent.Infrastructure
{
    public static class Registration
    {
        public static IServiceCollection AddComputerUseAgentInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ComputerUseInfrastructureOptions>().BindConfiguration(nameof(ComputerUseInfrastructureOptions));

            services.AddMemoryCache();

            services.AddTransient<HttpToolsJwtAuthHandler>();
            services.AddSingleton<IHttpToolsJwtAuthService, AzureHttpToolsJwtAuthService>();

            //Azure
            services.AddAzureAppHttpTools();

            //Local
            //services.AddLocalHttpTools();

            services.AddSingleton<IToolsOrchestrationManager, BaseHttpToolsOrchestrationManager>();

            // add DB
            /*string connection = configuration.GetConnectionString("MessagesConnection")!;

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IMessageRepository, MsSqlMessageRepository>();*/

            return services;
        }

        public static IServiceCollection AddLocalHttpTools(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var toolsOptions = provider.GetRequiredService<IOptions<ComputerUseInfrastructureOptions>>().Value.ToolsClient;

            if (toolsOptions is null)
            {
                throw new Exception($"{nameof(ComputerUseInfrastructureOptions.ToolsClient)} was not configured");
            }

            var resourceIdentifierId = Guid.CreateVersion7().ToString();

            services.AddHttpClient();
            services.AddHttpClient(resourceIdentifierId, (client) =>
            {
                client.BaseAddress = new Uri(toolsOptions.BaseUrl);
            });

            services.AddSingleton<ITools, HttpToolsClient>((provider) =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(resourceIdentifierId);
                return new HttpToolsClient(httpClient);
            });

            return services;
        }

        public static IServiceCollection AddAzureAppHttpTools(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var toolsOptions = provider.GetRequiredService<IOptions<ComputerUseInfrastructureOptions>>().Value.AzureAppHttpToolsClientOptions;

            if (!(toolsOptions?.Count > 0))
            {
                throw new Exception($"{nameof(ComputerUseInfrastructureOptions.AzureAppHttpToolsClientOptions)} was not configured");
            }

            foreach (AzureAppHttpToolsClientOptions toolOptions in toolsOptions)
            {
                ResourceIdentifier resourceIdentifier = ContainerAppResource.CreateResourceIdentifier(toolOptions.SubscriptionId, toolOptions.ResourceGroupName, toolOptions.ContainerAppName);

                var data = AzureAppHttpToolsClient.GetAzureAppData(resourceIdentifier).Result;

                var resourceIdentifierId = resourceIdentifier.ToString();
                services.AddHttpClient();
                services.AddHttpClient(resourceIdentifierId, (client) =>
                {
                    client.BaseAddress = new Uri(data.Url);
                });

                services.AddSingleton<ITools, AzureAppHttpToolsClient>((provider) =>
                {
                    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient(resourceIdentifierId);
                    return new AzureAppHttpToolsClient(httpClient, data);
                });
            }

            return services;
        }
    }
}
