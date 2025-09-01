namespace ComputerUse.Core.EnvironmentTools.Shell
{
    public interface IShell
    {
        public RunResponse Run(RunRequest request);
        public Task<RunResponse> RunAsync(RunRequest request);
    }
}
