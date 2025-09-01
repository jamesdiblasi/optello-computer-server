namespace ComputerUse.Contracts
{
    public record BaseToolResponseContract
    {
        public required string Output { get; init; }

        public required string Error { get; init; }
    }
}
