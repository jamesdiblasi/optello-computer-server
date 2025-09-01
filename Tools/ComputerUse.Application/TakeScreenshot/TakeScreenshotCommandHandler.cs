using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.TakeScreenshot
{
    public class TakeScreenshotCommandHandler : IRequestHandler<TakeScreenshotCommand, BaseImageCommandResponse>
    {
        private readonly IScreenshotMaker screenshotMaker;

        public TakeScreenshotCommandHandler(IScreenshotMaker screenshotMaker)
        {
            this.screenshotMaker = screenshotMaker;
        }

        public async Task<BaseImageCommandResponse> Handle(TakeScreenshotCommand request, CancellationToken cancellationToken)
        {
            if (request.WaitDuration is int waitDuration)
            {
                await Task.Delay(TimeSpan.FromSeconds(waitDuration));
            }

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse {Base64File = screenshot.Base64File, Error = "", Output = "" };
        }
    }
}
