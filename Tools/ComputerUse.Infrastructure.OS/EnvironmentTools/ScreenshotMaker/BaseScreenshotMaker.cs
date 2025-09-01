using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using Microsoft.Extensions.Options;

namespace ComputerUse.Infrastructure.OS.EnvironmentTools.ScreenshotMaker
{
    public abstract class BaseScreenshotMaker : BaseEnvironmentTool, IScreenshotMaker
    {
        protected BaseScreenshotMaker(IOptions<ComputerUseInfrastructureOsOptions> options) : base(options.Value) {}

        protected string GetNewFilePath()
        {
            var outputDir = this.options.OutputDir;

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            return Path.Combine(outputDir, $"screenshot_{Guid.CreateVersion7()}.png");
        }

        protected string GetBase64File(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"Screenshot \"{filePath}\" does not exist");
            }

            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        protected abstract Task TakeScreenshotRawAsync(string filePath);

        public async Task<Screenshot> TakeScreenshotAsync()
        {
            var filePath = GetNewFilePath();

            await TakeScreenshotRawAsync(filePath);

            var base64File = GetBase64File(filePath);

            return new Screenshot { Base64File = base64File };
        }
    }
}
