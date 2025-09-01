using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.TypeText;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class TypeTextCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public TypeTextCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Text))
            {
                throw new Exception("text is required for \"type\"");
            }

            return this.tools.TypeTextAsync(new TypeTextRequest {Identifier = context.Identifier, Text = context.Text});
        }
    }
}
