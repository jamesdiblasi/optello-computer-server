namespace ComputerUse.Infrastructure.OS.EnvironmentTools
{
    public abstract class BaseEnvironmentTool
    {
        protected ComputerUseInfrastructureOsOptions options;

        protected BaseEnvironmentTool(ComputerUseInfrastructureOsOptions options)
        {
            this.options = options;
        }

    }
}
