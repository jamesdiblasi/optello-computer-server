using ComputerUse.Application.TakeScreenshot;
using ComputerUse.Contracts.WaitAndScreenshot;

namespace ComputerUse.Host.Mappings
{
    public static class DisplayActionMappings
    {
        public static TakeScreenshotCommand ToCommand(this WaitAndScreenshotRequest contract)
        {
            return new TakeScreenshotCommand { WaitDuration = contract.WaitDuration };
        }
    }
}
