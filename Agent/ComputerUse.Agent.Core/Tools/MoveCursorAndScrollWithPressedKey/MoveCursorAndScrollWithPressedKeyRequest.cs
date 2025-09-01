namespace ComputerUse.Agent.Core.Tools.MoveCursorAndScrollWithPressedKey
{
    public record MoveCursorAndScrollWithPressedKeyRequest : BaseToolRequest
    {
        public required MouseScrollDirectionContract ScrollDirection { get; init; }

        public int ScrollAmount { get; init; }

        public string? Text { get; init; }

        public int? X { get; init; }

        public int? Y { get; init; }
    }
}
