namespace ComputerUse.Contracts.WaitAndScreenshot
{
    public record WaitAndScreenshotRequest
    {
        public int? WaitDuration { get; init; }
    }
}
