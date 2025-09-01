namespace ComputerUse.Agent.Core.Tools.OpenBrowser
{
    public record OpenBrowserRequest : BaseToolRequest
    {
        public required string Url { get; init; }

        public int? WaitDuration { get; init; }
    }
}
