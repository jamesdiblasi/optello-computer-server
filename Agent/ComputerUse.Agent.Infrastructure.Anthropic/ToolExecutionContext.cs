namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    internal record ToolExecutionContext
    {
        public required string Action { get; init; }

        public required CommandContext Context { get; init; }
    }
}
