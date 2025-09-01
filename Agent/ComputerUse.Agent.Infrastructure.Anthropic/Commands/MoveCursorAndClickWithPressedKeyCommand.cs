using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.MoveCursorAndClickWithPressedKey;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class MoveCursorAndClickWithPressedKeyCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public MoveCursorAndClickWithPressedKeyCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (!(context.ClickOptions is MouseClickOptionsContract clickOptions))
            {
                throw new Exception("clickOptions is required for \"mouse_move\" with click");
            }

            return this.tools.MoveCursorAndClickWithPressedKeyAsync(new MoveCursorAndClickWithPressedKeyRequest
            {
                Identifier = context.Identifier,
                X = context.X,
                Y = context.Y,
                MouseOptions = clickOptions,
                Text = context.Text
            });
        }
    }
}
