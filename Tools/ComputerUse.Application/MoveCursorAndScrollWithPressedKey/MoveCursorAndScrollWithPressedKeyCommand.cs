using MediatR;

namespace ComputerUse.Application.MoveCursorAndScrollWithPressedKey
{
    public class MoveCursorAndScrollWithPressedKeyCommand : IRequest<BaseImageCommandResponse>
    {
        public required ScrollDirectionCommand ScrollDirection { get; init; }

        public int ScrollAmount { get; init; }

        public string? Text { get; init; }

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}
