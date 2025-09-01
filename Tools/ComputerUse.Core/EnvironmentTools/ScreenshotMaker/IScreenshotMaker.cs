namespace ComputerUse.Core.EnvironmentTools.ScreenshotMaker
{
    public interface IScreenshotMaker
    {
        public Task<Screenshot> TakeScreenshotAsync();
    }
}
