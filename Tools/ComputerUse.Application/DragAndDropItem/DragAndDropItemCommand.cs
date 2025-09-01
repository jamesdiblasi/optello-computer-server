using MediatR;

namespace ComputerUse.Application.DragAndDropItem
{
    public class DragAndDropItemCommand : IRequest<BaseImageCommandResponse>
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}
