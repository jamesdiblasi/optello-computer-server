using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.AppContainers;
using Azure.ResourceManager;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ComputerUse.Agent.Core.Tools;
using Azure.ResourceManager.AppContainers.Models;

namespace ComputerUse.Agent.Infrastructure.Azure
{
    public record AzureAppHttpToolsClientData
    {
        public required ResourceIdentifier resourceIdentifier { get; init; }

        public required string Url { get; init; }

        public ToolStatus Status { get; init; }
    }

    internal class AzureAppHttpToolsClient : HttpToolsClient
    {
        private ResourceIdentifier resourceIdentifier;

        private static ContainerAppResource GetContainerAppResource(ResourceIdentifier resourceIdentifier)
        {
            TokenCredential cred = new DefaultAzureCredential();

            ArmClient client = new ArmClient(cred);

            ContainerAppResource containerApp = client.GetContainerAppResource(resourceIdentifier);

            return containerApp;
        }

        public static async Task<string> GetAzureAppUrl(ResourceIdentifier resourceIdentifier)
        {
            var containerApp = GetContainerAppResource(resourceIdentifier);

            Response<ContainerAppResource> lro = await containerApp.GetAsync();
            ContainerAppResource result = lro.Value;
            ContainerAppData resourceData = result.Data;

            return "https://" + resourceData.Configuration.Ingress.Fqdn;
        }

        public static async Task<AzureAppHttpToolsClientData> GetAzureAppData(ResourceIdentifier resourceIdentifier)
        {
            var containerApp = GetContainerAppResource(resourceIdentifier);

            Response<ContainerAppResource> lro = await containerApp.GetAsync();
            ContainerAppResource result = lro.Value;
            ContainerAppData resourceData = result.Data;

            return new AzureAppHttpToolsClientData
            {
                resourceIdentifier = resourceIdentifier,
                Url = "https://" + resourceData.Configuration.Ingress.Fqdn,
                Status = resourceData.RunningStatus == ContainerAppRunningStatus.Running ? ToolStatus.Running : ToolStatus.Stopped
            };
        }

        /*private ToolStatus _status;

        public override async Task<ToolStatus> GetStatus()
        {
            return _status;
        }*/

        public AzureAppHttpToolsClient(HttpClient httpClient, AzureAppHttpToolsClientData data) : base(httpClient)
        {
            this.Id = data.resourceIdentifier.ToString();
            this.resourceIdentifier = data.resourceIdentifier;
            //this._status = data.Status;
        }

        public override async Task Start()
        {
            ContainerAppResource containerApp = GetContainerAppResource(resourceIdentifier);

            await containerApp.StartAsync(WaitUntil.Completed);

            //_status = ToolStatus.Running;
            return;
        }

        public override async Task Stop()
        {
            ContainerAppResource containerApp = GetContainerAppResource(resourceIdentifier);

            await containerApp.StopAsync(WaitUntil.Completed);

            //_status = ToolStatus.Stopped;
            return;
        }
    }
}
