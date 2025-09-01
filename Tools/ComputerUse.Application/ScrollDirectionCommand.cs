using ComputerUse.Core.EnvironmentTools.MouseAutomation;

namespace ComputerUse.Application
{
    public enum ScrollDirectionCommand
    {
        Up = 4,
        Down = 5,
        Left = 6,
        Right = 7
    }

    public static class ScrollDirectionCommandExtension
    {
        public static ScrollDirection ToEnvironmentScrollDirection(this ScrollDirectionCommand mouseClickOptions) => mouseClickOptions switch
        {
            ScrollDirectionCommand.Up => ScrollDirection.Up,
            ScrollDirectionCommand.Down => ScrollDirection.Down,
            ScrollDirectionCommand.Left => ScrollDirection.Left,
            ScrollDirectionCommand.Right => ScrollDirection.Right,
            _ => throw new NotImplementedException()
        };
    }
}
