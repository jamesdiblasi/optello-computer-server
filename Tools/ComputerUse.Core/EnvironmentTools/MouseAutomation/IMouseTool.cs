namespace ComputerUse.Core.EnvironmentTools.MouseAutomation
{
    public interface IMouseTool
    {
        public Task<CursorPosition> GetCursorPositionAsync();

        public Task ClickAsync(MouseClickOptions clickOption);

        public Task MoveAsync(CursorPosition cursorPosition);

        public Task Scroll(ScrollAction scrollAction);
    }
}
