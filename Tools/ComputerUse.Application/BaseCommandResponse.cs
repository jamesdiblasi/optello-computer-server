namespace ComputerUse.Application
{
    public record BaseCommandResponse
    {
        public required string Output { get; init; }

        public required string Error { get; init; }
    }
}
