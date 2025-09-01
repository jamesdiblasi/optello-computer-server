using MediatR;

namespace ComputerUse.Application.TypeText
{
    public class TypeTextCommand : IRequest<BaseImageCommandResponse>
    {
        public required string Text { get; init; }
    }
}
