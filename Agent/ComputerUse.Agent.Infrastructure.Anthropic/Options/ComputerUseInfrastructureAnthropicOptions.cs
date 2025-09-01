namespace ComputerUse.Agent.Infrastructure.Anthropic.Options
{
    internal class ComputerUseInfrastructureAnthropicOptions
    {
        public required string ApiKey { get; init; }

        public required int MaxTokens { get; init; }

        public required ComputerUseDisplayOptions Display { get; init; }
    }
}
