using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.MoveCursorAndScrollWithPressedKey;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class MoveCursorAndScrollWithPressedKeyCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public MoveCursorAndScrollWithPressedKeyCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (context.ScrollDirection is not MouseScrollDirectionContract scrollDirection)
            {
                throw new Exception("ScrollDirection is required for \"scroll\"");
            }

            if (context.ScrollAmount is not int scrollAmount || (scrollAmount < 0))
            {
                throw new Exception("ScrollAmount is required for \"scroll\" and must be a non-negative int");
            }

            return this.tools.MoveCursorAndScrollWithPressedKeyAsync(new MoveCursorAndScrollWithPressedKeyRequest
            {
                Identifier = context.Identifier,
                X = context.X,
                Y = context.Y,
                ScrollDirection = scrollDirection,
                ScrollAmount = scrollAmount,
                Text = context.Text
            });
        }
    }
}
