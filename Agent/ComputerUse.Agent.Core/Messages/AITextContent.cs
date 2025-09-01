namespace ComputerUse.Agent.Core.Messages
{
    public record AITextContent : IAIContent
    {
        public required string Text { get; init; }
    }
}
