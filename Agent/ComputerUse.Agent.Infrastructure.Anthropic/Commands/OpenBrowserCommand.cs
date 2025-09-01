using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.OpenBrowser;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class OpenBrowserCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public OpenBrowserCommand(ITools tools) : base(tools) {}

        protected override async Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            var response = await this.tools.OpenBrowserAsync(new OpenBrowserRequest 
                { 
                    Identifier = context.Identifier, 
                    Url = context.Text, 
                    WaitDuration = 15
                }
            );

            return response;
        }
    }
}
