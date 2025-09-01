using ComputerUse.Core.EnvironmentTools.Keyboard;
using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.Shell;
using Microsoft.Extensions.Options;

namespace ComputerUse.Infrastructure.OS.EnvironmentTools
{
    internal class Xdotool : BaseEnvironmentTool, IMouseTool, IKeyboardTool
    {
        private const string XDOTOOL_FILENAME = "xdotool";

        private readonly IShell shell;

        public Xdotool(IShell shell, IOptions<ComputerUseInfrastructureOsOptions> options) : base(options.Value) 
        {
            this.shell = shell;
        }

        public async Task<CursorPosition> GetCursorPositionAsync()
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = "getmouselocation --shell",
            };

            var shellResponse = await shell.RunAsync(shellRequest);

            var xStr = shellResponse.StandardOutput.Split("X=")[1].Split("\n")[0];
            var yStr = shellResponse.StandardOutput.Split("Y=")[1].Split("\n")[0];

            return new CursorPosition
            {
                X = int.Parse(xStr),
                Y = int.Parse(yStr)
            };
        }

        public async Task MoveAsync(CursorPosition cursorPosition)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"mousemove --sync {cursorPosition.X} {cursorPosition.Y}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task ClickAsync(MouseClickOptions clickOption)
        {
            var clickAction = clickOption switch
            {
                MouseClickOptions.LeftClick => "click 1",
                MouseClickOptions.MiddleClick => "click 2",
                MouseClickOptions.RightClick => "click 3",
                MouseClickOptions.LeftDoubleClick => "click --repeat 2 --delay 10 1",
                MouseClickOptions.LeftTrippleClick => "click --repeat 3 --delay 10 1",
                MouseClickOptions.LeftMouseDown => "mousedown 1",
                MouseClickOptions.LeftMouseUp => "mouseup 1",
                _ => throw new NotImplementedException()
            };

            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = clickAction,
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task PressKeyAsync(Key key)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"key -- {key.Text}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task KeyDownAsync(Key key)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"keydown {key.Text}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task KeyUpAsync(Key key)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"keyup {key.Text}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task HoldKeyAsync(HoldenKey key)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"keydown {key.Text} sleep {key.Duration} keyup {key.Text}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task TypeTextAsync(TypedText typedText)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"type --delay {typedText.TypingDelay} -- {typedText.Text}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }

        public async Task Scroll(ScrollAction scrollAction)
        {
            var shellRequest = new RunRequest
            {
                FileName = XDOTOOL_FILENAME,
                Arguments = $"click --repeat {scrollAction.ScrollAmount} {(int) scrollAction.ScrollDirection}",
                //EnvironmentDisplayNum = options.DisplayNum
            };

            await shell.RunAsync(shellRequest);
        }
    }
}
