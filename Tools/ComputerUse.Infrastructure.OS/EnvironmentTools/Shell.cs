using ComputerUse.Core.EnvironmentTools.Shell;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ComputerUse.Infrastructure.OS.EnvironmentTools
{
    public class Shell : IShell
    {
        private const string ENV_DISPLAY = "DISPLAY";

        private readonly ILogger<Shell> logger;
        private readonly ComputerUseInfrastructureOsOptions options;

        public Shell(ILogger<Shell> logger, IOptions<ComputerUseInfrastructureOsOptions> optionsSlice) {
            this.logger = logger;
            this.options = optionsSlice.Value;
        }

        public RunResponse Run(RunRequest request)
        {
            return this.RunAsync(request).Result;
        }

        public async Task<RunResponse> RunAsync(RunRequest request)
        {
            logger.LogTrace($"Run the following cmd: {request.FileName} {request.Arguments}");

            var startInfo = new ProcessStartInfo(request.FileName)
            {
                Arguments = request.Arguments,
                UseShellExecute = request.UseShellExecute,
                RedirectStandardError = request.RedirectStandardError,
                RedirectStandardOutput = request.RedirectStandardOutput
            };

            startInfo.Environment[ENV_DISPLAY] = $":{this.options.DisplayNum}";

            using (var process = Process.Start(startInfo) ?? throw new Exception("No process resource was started"))
            {
                try
                {
                    if (request.IsWaitForExit)
                    {
                        await process.WaitForExitAsync().WaitAsync(TimeSpan.FromSeconds(request.Timeout));

                        var standardError = await process.StandardError.ReadToEndAsync();
                        if (!string.IsNullOrWhiteSpace(standardError))
                        {
                            throw new Exception($"Error occurs during execution the following command:\"{request}\". ExitCode:{process.ExitCode}. Error:{standardError}");
                        }

                        var standardOutput = await process.StandardOutput.ReadToEndAsync();

                        logger.LogDebug($"Exit code:{process.ExitCode}. Output: {standardOutput}");
                        logger.LogTrace($"Cmd \"{request.FileName} {request.Arguments}\" successfully finished");

                        return new RunResponse
                        {
                            StandardOutput = standardOutput,
                            ExitCode = process.ExitCode
                        };
                    }

                    return new RunResponse
                    {
                        StandardOutput = string.Empty,
                        ExitCode = 0
                    };
                }
                catch (OperationCanceledException)
                {
                    //timeout
                    process.Kill();

                    throw new TimeoutException($"Command \"{request.FileName}\" timed out after {request.Timeout} seconds");
                }
            }
        }
    }
}
