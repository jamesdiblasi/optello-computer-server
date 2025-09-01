namespace ComputerUse.Core.EnvironmentTools.Keyboard
{
    public record TypedText
    {
        public const int TYPING_DELAY_MS = 12;

        public required string Text { get; init; }

        public int TypingDelay { get; init; } = TYPING_DELAY_MS;
    }
}
