namespace ComputerUse.Agent.Core.Tools.PressKey
{
    public record PressKeyRequest : BaseToolRequest
    {
        public required string Text { get; init; }

        public int? Duration { get; init; }
    }
}
