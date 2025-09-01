using MediatR;

namespace ComputerUse.Application.PressKey
{
    public class PressKeyCommand : IRequest<BaseImageCommandResponse>
    {
        public required string Text { get; init; }

        public int? Duration { get; init; }
    }
}
