using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Core.Tools.DragAndDropItem;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class DragAndDropItemCommand : BaseImageCommand<BaseImageToolResponse>
    {
        public DragAndDropItemCommand(ITools tools) : base(tools) {}

        protected override Task<BaseImageToolResponse> ExecuteToolAsync(CommandContext context)
        {
            if (context.X is int x && context.Y is int y)
            {
                return this.tools.DragAndDropItemAsync(new DragAndDropItemRequest { Identifier = context.Identifier, X = x, Y = y });
            }
            else
            {
                throw new Exception("coordinate is required for \"mouse_move\"");
            }
        }
    }
}
