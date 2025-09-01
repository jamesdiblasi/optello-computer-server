namespace ComputerUse.Contracts.PressKey
{
    public record PressKeyRequest
    {
        public required string Text { get; init; }

        public int? Duration { get; init; }
    }
}
