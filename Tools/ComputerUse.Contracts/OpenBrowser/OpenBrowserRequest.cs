namespace ComputerUse.Contracts.OpenBrowser
{
    public record OpenBrowserRequest
    {
        public required string Url { get; init; }

        public int? WaitDuration { get; init; }
    }
}
