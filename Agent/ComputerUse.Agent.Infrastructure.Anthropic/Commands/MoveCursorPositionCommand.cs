using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class MoveCursorPositionCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public MoveCursorPositionCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (context.X is int x && context.Y is int y)
            {
                return this.tools.MoveCursorPositionAsync(new Core.Tools.MoveCursorPosition.MoveCursorPositionRequest { Identifier = context.Identifier, X = x, Y = y });
            }
            else
            {
                throw new Exception("coordinate is required for \"mouse_move\"");
            }
        }
    }
}
