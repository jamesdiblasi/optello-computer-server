using ComputerUse.Core.EnvironmentTools.Shell;
using Microsoft.Extensions.Options;

namespace ComputerUse.Infrastructure.OS.EnvironmentTools.ScreenshotMaker
{
    internal class ScrotScreenshotMaker : BaseScreenshotMaker
    {
        private readonly IShell shell;

        public ScrotScreenshotMaker(IShell shell, IOptions<ComputerUseInfrastructureOsOptions> optionsSnapshot): base(optionsSnapshot)
        {
            this.shell = shell;
        }

        protected override async Task TakeScreenshotRawAsync(string filePath)
        {
            var runRequest = new RunRequest
            {
                FileName = "scrot",
                Arguments = $"-p {filePath}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(runRequest);
        }
    }
}