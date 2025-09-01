namespace ComputerUse.Agent.Core.Tools.MoveCursorPosition
{
    public record MoveCursorPositionRequest : BaseToolRequest
    {
        public int X { get; init; }
        public int Y { get; init; }
    }
}
