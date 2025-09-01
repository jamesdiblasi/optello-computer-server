using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using ComputerUse.Core.EnvironmentTools.Shell;
using MediatR;

namespace ComputerUse.Application.TakeScreenshot
{
    public class OpenBrowserCommandHandler : IRequestHandler<OpenBrowserCommand, BaseImageCommandResponse>
    {
        private const string FIREFOX_BROWSER_FILENAME = "firefox-esr";

        private readonly IScreenshotMaker screenshotMaker;
        private readonly IShell shell;

        public OpenBrowserCommandHandler(IScreenshotMaker screenshotMaker, IShell shell)
        {
            this.screenshotMaker = screenshotMaker;
            this.shell = shell;
        }

        public async Task<BaseImageCommandResponse> Handle(OpenBrowserCommand request, CancellationToken cancellationToken)
        {
            var shellRequest = new RunRequest
            {
                FileName = FIREFOX_BROWSER_FILENAME,
                Arguments = request.Url,
                IsWaitForExit = false,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
            };

            await this.shell.RunAsync(shellRequest);

            if (request.WaitDuration is int waitDuration)
            {
                await Task.Delay(TimeSpan.FromSeconds(waitDuration));
            }

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse {Base64File = screenshot.Base64File, Error = "", Output = "" };
        }
    }
}
