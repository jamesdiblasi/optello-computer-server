using MediatR;

namespace ComputerUse.Application.TakeScreenshot
{
    public class OpenBrowserCommand : IRequest<BaseImageCommandResponse>
    {
        public required string Url { get; init; }

        public int? WaitDuration { get; init; }
    }
}
