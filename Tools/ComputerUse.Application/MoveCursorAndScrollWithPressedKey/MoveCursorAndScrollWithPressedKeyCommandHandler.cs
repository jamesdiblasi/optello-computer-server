using ComputerUse.Core.EnvironmentTools.Keyboard;
using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.MoveCursorAndScrollWithPressedKey
{
    public class MoveCursorAndScrollWithPressedKeyCommandHandler : IRequestHandler<MoveCursorAndScrollWithPressedKeyCommand, BaseImageCommandResponse>
    {
        private readonly IMouseTool mouseTool;
        private readonly IScreenshotMaker screenshotMaker;
        private readonly IKeyboardTool keyboardTool;

        public MoveCursorAndScrollWithPressedKeyCommandHandler(IMouseTool mouseTool, IKeyboardTool keyboardTool, IScreenshotMaker screenshotMaker)
        {
            this.mouseTool = mouseTool;
            this.screenshotMaker = screenshotMaker;
            this.keyboardTool = keyboardTool;
        }

        public async Task<BaseImageCommandResponse> Handle(MoveCursorAndScrollWithPressedKeyCommand request, CancellationToken cancellationToken)
        {
            if (request.X is int x && request.Y is int y)
            {
                await mouseTool.MoveAsync(new CursorPosition { X = x, Y = y });
            }

            if (!string.IsNullOrEmpty(request.Text))
            {
                await this.keyboardTool.KeyDownAsync(new Key { Text = request.Text });
            }

            var envScrollDirection = request.ScrollDirection.ToEnvironmentScrollDirection();

            await this.mouseTool.Scroll(new ScrollAction { ScrollAmount = 1, ScrollDirection = envScrollDirection });

            if (!string.IsNullOrEmpty(request.Text))
            {
                await this.keyboardTool.KeyUpAsync(new Key { Text = request.Text });
            }

            // delay to let things settle before taking a screenshot 
            await Task.Delay(TimeSpan.FromSeconds(2));

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse {Base64File = screenshot.Base64File, Output = "", Error = "" };
        }
    }
}
