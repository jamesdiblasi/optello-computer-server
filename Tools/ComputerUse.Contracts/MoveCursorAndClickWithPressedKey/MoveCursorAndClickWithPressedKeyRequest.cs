namespace ComputerUse.Contracts.MoveCursorAndClickWithPressedKey
{
    public record MoveCursorAndClickWithPressedKeyRequest
    {
        public required MouseClickOptionsContract MouseOptions { get; init; }

        public string? Text { get; init; }

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}
