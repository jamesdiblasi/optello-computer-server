namespace ComputerUse.Agent.Core.Tools.GetCursorPosition
{
    public record GetCursorPositionResponse : BaseToolResponse
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }
}
