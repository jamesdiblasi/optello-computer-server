using ComputerUse.Application.TakeScreenshot;
using ComputerUse.Contracts.OpenBrowser;

namespace ComputerUse.Host.Mappings
{
    public static class OpenBrowserMappings
    {
        public static OpenBrowserCommand ToCommand(this OpenBrowserRequest contract)
        {
            return new OpenBrowserCommand 
            {
                Url = contract.Url,
                WaitDuration = contract.WaitDuration
            };
        }
    }
}
