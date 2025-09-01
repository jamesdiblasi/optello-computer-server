namespace ComputerUse.Contracts.GetCursorPosition
{
    public record GetCursorPositionResponse : BaseToolResponseContract
    {
        public required int X { get; init; }

        public required int Y { get; init; }
    }
}
