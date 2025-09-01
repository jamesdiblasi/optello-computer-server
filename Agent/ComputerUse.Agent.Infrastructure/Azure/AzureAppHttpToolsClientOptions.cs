using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure.Azure
{
    internal record AzureAppHttpToolsClientOptions
    {
        public required string SubscriptionId { get; init; }

        public required string ResourceGroupName { get; init; }

        public required string ContainerAppName { get; init; }
    }
}
