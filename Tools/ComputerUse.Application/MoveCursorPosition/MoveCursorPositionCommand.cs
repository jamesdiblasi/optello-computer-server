using MediatR;

namespace ComputerUse.Application.MoveCursorPosition
{
    public class MoveCursorPositionCommand : IRequest<BaseImageCommandResponse>
    {
        public required int X { get; set; }

        public required int Y { get; set; }
    }
}
