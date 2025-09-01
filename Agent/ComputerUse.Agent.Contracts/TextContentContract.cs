namespace ComputerUse.Agent.Contracts
{
    public record TextContentContract: IContentContract
    {
        public required string Text { get; init; }
    }
}
