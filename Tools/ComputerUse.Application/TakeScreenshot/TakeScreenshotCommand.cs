using MediatR;

namespace ComputerUse.Application.TakeScreenshot
{
    public class TakeScreenshotCommand : IRequest<BaseImageCommandResponse>
    {
        public int? WaitDuration { get; init; }
    }
}
