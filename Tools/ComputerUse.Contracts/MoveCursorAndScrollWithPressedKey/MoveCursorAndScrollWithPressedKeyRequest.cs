namespace ComputerUse.Contracts.MoveCursorAndScrollWithPressedKey
{
    public record MoveCursorAndScrollWithPressedKeyRequest
    {
        public required MouseScrollDirectionContract ScrollDirection { get; init; }

        public int ScrollAmount { get; init; }

        public string? Text { get; init; }

        public int? X { get; init; }

        public int? Y { get; init; }
    }
}
