using ComputerUse.Application;
using ComputerUse.Application.DragAndDropItem;
using ComputerUse.Application.GetCursorPosition;
using ComputerUse.Application.MoveCursorAndClickWithPressedKey;
using ComputerUse.Application.MoveCursorAndScrollWithPressedKey;
using ComputerUse.Application.MoveCursorPosition;
using ComputerUse.Contracts;
using ComputerUse.Contracts.DragAndDropItem;
using ComputerUse.Contracts.GetCursorPosition;
using ComputerUse.Contracts.MoveCursorAndClickWithPressedKey;
using ComputerUse.Contracts.MoveCursorAndScrollWithPressedKey;
using ComputerUse.Contracts.MoveCursorPosition;

namespace ComputerUse.Host.Mappings
{
    public static class MouseActionMappings
    {
        public static DragAndDropItemCommand ToCommand(this DragAndDropItemRequest contract)
        {
            return new DragAndDropItemCommand { X = contract.X, Y = contract.Y };
        }

        public static GetCursorPositionResponse ToContract(this GetCursorPositionCommandResponse command)
        {
            return new GetCursorPositionResponse { X = command.X, Y = command.Y, Error = command.Error, Output = command.Output };
        }

        public static MoveCursorPositionCommand ToCommand(this MoveCursorPositionRequest contract)
        {
            return new MoveCursorPositionCommand { X = contract.X, Y = contract.Y };
        }

        public static MoveCursorAndClickWithPressedKeyCommand ToCommand(this MoveCursorAndClickWithPressedKeyRequest contract)
        {
            return new MoveCursorAndClickWithPressedKeyCommand { X = contract.X, Y = contract.Y, Text = contract.Text, MouseOptions = contract.MouseOptions.ToMouseClickOptionsCommand() };
        }

        public static MouseClickOptionsCommand ToMouseClickOptionsCommand(this MouseClickOptionsContract contract) => 
            contract switch 
            { 
                MouseClickOptionsContract.LeftClick => MouseClickOptionsCommand.LeftClick, 
                MouseClickOptionsContract.MiddleClick => MouseClickOptionsCommand.MiddleClick, 
                MouseClickOptionsContract.RightClick => MouseClickOptionsCommand.RightClick, 
                MouseClickOptionsContract.LeftDoubleClick => MouseClickOptionsCommand.LeftDoubleClick, 
                MouseClickOptionsContract.LeftTrippleClick => MouseClickOptionsCommand.LeftTrippleClick,
                MouseClickOptionsContract.LeftMouseDown => MouseClickOptionsCommand.LeftMouseDown,
                MouseClickOptionsContract.LeftMouseUp => MouseClickOptionsCommand.LeftMouseUp,
                _ => throw new NotImplementedException() 
            };

        public static MoveCursorAndScrollWithPressedKeyCommand ToCommand(this MoveCursorAndScrollWithPressedKeyRequest contract)
        {
            return new MoveCursorAndScrollWithPressedKeyCommand 
            { 
                X = contract.X, 
                Y = contract.Y, 
                Text = contract.Text, 
                ScrollAmount = contract.ScrollAmount,
                ScrollDirection = contract.ScrollDirection.ToScrollDirectionCommand() 
            };
        }
        public static ScrollDirectionCommand ToScrollDirectionCommand(this MouseScrollDirectionContract mouseClickOptions) => mouseClickOptions switch
        {
            MouseScrollDirectionContract.Up => ScrollDirectionCommand.Up,
            MouseScrollDirectionContract.Down => ScrollDirectionCommand.Down,
            MouseScrollDirectionContract.Left => ScrollDirectionCommand.Left,
            MouseScrollDirectionContract.Right => ScrollDirectionCommand.Right,
            _ => throw new NotImplementedException()
        };
        
    }
}
