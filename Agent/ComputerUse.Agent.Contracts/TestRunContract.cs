namespace ComputerUse.Agent.Contracts
{
    public record TestRunContract
    {
        public required string TargetUrl { get; init; }

        public required string Name { get; init; }

        public required AIModelsContract AiModel { get; init; }

        public required IList<string> Steps { get; init; }
    }
}
