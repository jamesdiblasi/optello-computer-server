namespace ComputerUse.Agent.Core.Tools.TypeText
{
    public record TypeTextRequest : BaseToolRequest
    {
        public required string Text { get; init; }
    }
}
