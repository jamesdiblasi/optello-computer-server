using ComputerUse.Core.EnvironmentTools.MouseAutomation;

namespace ComputerUse.Application
{
    public enum MouseClickOptionsCommand
    {
        LeftClick,
        MiddleClick,
        RightClick,
        LeftDoubleClick,
        LeftTrippleClick,
        LeftMouseDown,
        LeftMouseUp,
    }

    public static class MouseClickOptionsCommandExtension
    {
        public static MouseClickOptions ToEnvironmentMouseClickOptions(this MouseClickOptionsCommand mouseClickOptions) => mouseClickOptions switch
        {
            MouseClickOptionsCommand.LeftClick => MouseClickOptions.LeftClick,
            MouseClickOptionsCommand.MiddleClick => MouseClickOptions.MiddleClick,
            MouseClickOptionsCommand.RightClick => MouseClickOptions.RightClick,
            MouseClickOptionsCommand.LeftDoubleClick => MouseClickOptions.LeftDoubleClick,
            MouseClickOptionsCommand.LeftTrippleClick => MouseClickOptions.LeftTrippleClick,
            MouseClickOptionsCommand.LeftMouseDown => MouseClickOptions.LeftMouseDown,
            MouseClickOptionsCommand.LeftMouseUp => MouseClickOptions.LeftMouseUp,
            _ => throw new NotImplementedException()
        };
    }
}
