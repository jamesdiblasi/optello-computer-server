namespace ComputerUse.Application.GetCursorPosition
{
    public record GetCursorPositionCommandResponse : BaseCommandResponse
    {
        public required int X { get; init; }

        public required int Y { get; init; }
    }
}
