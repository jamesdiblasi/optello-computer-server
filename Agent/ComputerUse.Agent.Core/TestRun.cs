namespace ComputerUse.Agent.Core
{
    public record TestRun
    {
        public required string TargetUrl { get; init; }

        public required string Name { get; init; }

        public required AIModels AiModel { get; init; }

        public required IList<string> Steps { get; init; }
    }
}
