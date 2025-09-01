using ComputerUse.Agent.Core.Tools;
using ComputerUse.Agent.Infrastructure.Anthropic.Commands;

namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    internal class CommandFactory
    {
        public ICommand CreateCommand(string action, ITools tools) => action switch
        {
            /*
             "key", //+
    "type", //+
    "mouse_move", //+
    "left_click", //+
    "left_click_drag", //+
    "right_click", //+
    "middle_click", //+
    "double_click", //+
    "screenshot", //+
    "cursor_position" //+

            "left_mouse_down", //+
        "left_mouse_up", //+
        "scroll", //+
        "hold_key", //+
        "wait", //+
        "triple_click" //+
             */
            "key" => new PressKeyCommand(tools),
            "hold_key" => new PressKeyCommand(tools),
            "type" => new TypeTextCommand(tools),
            "screenshot" => new TakeScreenshotCommand(tools),
            "wait" => new TakeScreenshotCommand(tools),
            "mouse_move" => new MoveCursorPositionCommand(tools),
            "left_click" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "right_click" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "middle_click" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "double_click" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "triple_click" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "left_mouse_down" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "left_mouse_up" => new MoveCursorAndClickWithPressedKeyCommand(tools),
            "cursor_position" => new GetCursorPositionCommand(tools),
            "open_browser" => new OpenBrowserCommand(tools),
            "left_click_drag" => new DragAndDropItemCommand(tools),
            "scroll" => new MoveCursorAndScrollWithPressedKeyCommand(tools),
            _ => throw new NotImplementedException($"The {action} action is not implemented.")
        };
    }
}
