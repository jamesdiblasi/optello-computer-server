using ComputerUse.Agent.Infrastructure.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure
{
    internal class ComputerUseInfrastructureOptions
    {
        public HttpProxyToolsClientOptions? ToolsClient { get; init; }

        public List<AzureAppHttpToolsClientOptions>? AzureAppHttpToolsClientOptions { get; init; }
    }
}
