namespace ComputerUse.Agent.Core.Tools.Screenshot
{
    public record WaitAndScreenshotRequest : BaseToolRequest
    {
        public int? WaitDuration { get; init; }
    }
}
