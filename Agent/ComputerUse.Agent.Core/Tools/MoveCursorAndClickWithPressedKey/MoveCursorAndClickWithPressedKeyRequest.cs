namespace ComputerUse.Agent.Core.Tools.MoveCursorAndClickWithPressedKey
{
    public record MoveCursorAndClickWithPressedKeyRequest : BaseToolRequest
    {
        public required MouseClickOptionsContract MouseOptions { get; init; }

        public string? Text { get; init; }

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}
