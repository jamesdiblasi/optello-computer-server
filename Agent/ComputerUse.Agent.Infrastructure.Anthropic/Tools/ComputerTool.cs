using ComputerUse.Agent.Infrastructure.Anthropic.Options;
using Microsoft.Extensions.Options;
using CommonTool = Anthropic.SDK.Common.Tool;
using CommonFunction = Anthropic.SDK.Common.Function;
using Anthropic.SDK.Messaging;
using ComputerUse.Agent.Core.Tools;
using System.Text.Json.Nodes;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Tools
{
    internal class ComputerTool : IAnthropicTool
    {
        public const string TOOL_NAME = "computer";
        public string ToolName { get; private set; }
        public CommonTool ToolRegistration { get; private set; }

        public ComputerTool(IOptions<ComputerUseInfrastructureAnthropicOptions> optionsSlice)
        {
            ToolName = TOOL_NAME;

            var options = optionsSlice.Value;

        //SDK:4.7.1
            ToolRegistration = new CommonFunction(ToolName, "computer_20250124" /*"computer_20250124"*/ /*"computer_20241022"*/, new Dictionary<string, object>()
            {
                {"display_width_px", options.Display.Width /*1920*/ },
                {"display_height_px", options.Display.Height /*1080*/ },
                {"display_number", options.Display.DisplayNumber }
            });
        }

        public ToolExecutionContext? GetToolExecutionContext(ToolUseContent content, string identifier) 
        {
            int? x = null;
            int? y = null;
            int? duration = null;
            int? scrollAmount = null;
            MouseScrollDirectionContract? scrollDirection = null;

            //logger.LogDebug($"[Claudi] TOOL: {JsonSerializer.Serialize(tool)}");
            //logger.LogDebug($"[Claudi] TOOL.INPUT={tool?.Input?.ToJsonString()}");

            var action = content?.Input?["action"]?.ToString();

            //{"action":"scroll","coordinate":[512,400],"scroll_direction":"down","scroll_amount":3}
            if (action == "scroll")
            {
                if (content!.Input["scroll_amount"]?.ToString() is string strScrollAmount)
                {
                    scrollAmount = int.Parse(strScrollAmount);
                }

                if (content!.Input["scroll_direction"]?.ToString() is string strScrollDirection)
                {
                    scrollDirection = strScrollDirection.ToLower() switch
                    {
                        "down" => MouseScrollDirectionContract.Down,
                        "up" => MouseScrollDirectionContract.Up,
                        "left" => MouseScrollDirectionContract.Left,
                        "right" => MouseScrollDirectionContract.Right,
                        _ => null
                    };
                }
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                return null;
            }

            if (content!.Input["coordinate"] is JsonArray coordinate
                && coordinate.Count >= 2
                && coordinate[0]?.ToString() is string strX
                && coordinate[1]?.ToString() is string strY)
            {
                x = Convert.ToInt32(strX.ToString());
                y = Convert.ToInt32(strY.ToString());
            }

            if (content.Input["duration"]?.ToString() is string strDuration)
            {
                duration = int.Parse(strDuration);
            }

            MouseClickOptionsContract? clickOptions = action switch
            {
                "left_click" => MouseClickOptionsContract.LeftClick,
                "right_click" => MouseClickOptionsContract.RightClick,
                "middle_click" => MouseClickOptionsContract.MiddleClick,
                "double_click" => MouseClickOptionsContract.LeftDoubleClick,
                "triple_click" => MouseClickOptionsContract.LeftTrippleClick,
                "left_mouse_down" => MouseClickOptionsContract.LeftMouseDown,
                "left_mouse_up" => MouseClickOptionsContract.LeftMouseUp,
                _ => null
            };

            var context = new CommandContext
            {
                Identifier = identifier,
                Text = content.Input["text"]?.ToString(),
                ToolId = content.Id,
                X = x,
                Y = y,
                Duration = duration,
                ClickOptions = clickOptions,
                ScrollAmount = scrollAmount,
                ScrollDirection = scrollDirection
            };

            return new ToolExecutionContext { Action = action, Context = context };
        }
    }
}
