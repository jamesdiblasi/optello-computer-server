using MediatR;

namespace ComputerUse.Application.MoveCursorAndClickWithPressedKey
{
    public class MoveCursorAndClickWithPressedKeyCommand : IRequest<BaseImageCommandResponse>
    {
        public required MouseClickOptionsCommand MouseOptions { get; init; }

        public string? Text { get; init; }

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}
