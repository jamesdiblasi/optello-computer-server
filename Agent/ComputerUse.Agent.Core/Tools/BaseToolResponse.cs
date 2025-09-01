namespace ComputerUse.Agent.Core.Tools
{
    public record BaseToolResponse
    {
        public required string Output { get; init; }

        public required string Error { get; init; }

        public string? System { get; init; } = null;
    }
}
