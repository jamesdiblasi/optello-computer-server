using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.Screenshot;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class TakeScreenshotCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public TakeScreenshotCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            return this.tools.TakeScreenshotAsync(new WaitAndScreenshotRequest { Identifier = context.Identifier, WaitDuration = context.Duration });
        }
    }
}
