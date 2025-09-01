namespace ComputerUse.Agent.Core.Tools.DragAndDropItem
{
    public record DragAndDropItemRequest : BaseToolRequest
    {
        public int X { get; init; }
        public int Y { get; init; }
    }
}
