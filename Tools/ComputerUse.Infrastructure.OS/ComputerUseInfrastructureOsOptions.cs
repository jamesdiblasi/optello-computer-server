namespace ComputerUse.Infrastructure.OS
{
    public class ComputerUseInfrastructureOsOptions
    {
        public const string ConfigurationSectionName = nameof(ComputerUseInfrastructureOsOptions);


        public int DisplayNum { get; set; } = -1;

        public string? OutputDir { get; set; } = null;
    }
}
