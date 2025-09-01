using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.PressKey;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class PressKeyCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public PressKeyCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Text))
            {
                throw new Exception("text is required for \"key\"");
            }

            return this.tools.PressKeyAsync(new PressKeyRequest { Identifier = context.Identifier, Text = context.Text, Duration = context.Duration});
        }
    }
}
