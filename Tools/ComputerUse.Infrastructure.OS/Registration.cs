using ComputerUse.Core.EnvironmentTools.Keyboard;
using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using ComputerUse.Core.EnvironmentTools.Shell;
using ComputerUse.Infrastructure.OS.EnvironmentTools;
using ComputerUse.Infrastructure.OS.EnvironmentTools.ScreenshotMaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerUse.Infrastructure.OS
{
    public static class Registration
    {
        private const int DEFAULT_DISPLAY_NUM = 1;
        private const string ENVIRONMENT_VARIABLE_DISPLAY_NUM = "DISPLAY_NUM";

        public static IServiceCollection AddComputerUseInfrastructureOs(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<ComputerUseInfrastructureOsOptions>(configurationSection)
                .PostConfigure<ComputerUseInfrastructureOsOptions>(options =>
                {
                    var envDisplayNum = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_DISPLAY_NUM);

                    options.DisplayNum = options.DisplayNum < 0 ? (int.TryParse(envDisplayNum, out var displayNum) ? displayNum : DEFAULT_DISPLAY_NUM) : options.DisplayNum;
                    options.OutputDir = string.IsNullOrWhiteSpace(options.OutputDir) ? Path.GetTempPath() : options.OutputDir;
                });

            services.AddSingleton<IShell, Shell>();
            services.AddSingleton<IScreenshotMaker, ScrotScreenshotMaker>();

            services.AddSingleton<Xdotool>();
            services.AddSingleton<IMouseTool>(x => x.GetRequiredService<Xdotool>());
            services.AddSingleton<IKeyboardTool>(x => x.GetRequiredService<Xdotool>());

            return services;
        }

    }
}
