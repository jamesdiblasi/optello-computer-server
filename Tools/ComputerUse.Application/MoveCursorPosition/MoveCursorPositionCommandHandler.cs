using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.MoveCursorPosition
{
    public class MoveCursorPositionCommandHandler : IRequestHandler<MoveCursorPositionCommand, BaseImageCommandResponse>
    {
        private readonly IMouseTool mouseTool;
        private readonly IScreenshotMaker screenshotMaker;

        public MoveCursorPositionCommandHandler(IMouseTool mouseTool, IScreenshotMaker screenshotMaker)
        {
            this.mouseTool = mouseTool;
            this.screenshotMaker = screenshotMaker;
        }

        public async Task<BaseImageCommandResponse> Handle(MoveCursorPositionCommand request, CancellationToken cancellationToken)
        {
            await mouseTool.MoveAsync(new CursorPosition { X = request.X, Y = request.Y});

            // delay to let things settle before taking a screenshot 
            await Task.Delay(TimeSpan.FromSeconds(2));

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse {Base64File = screenshot.Base64File, Output = "", Error = "" };
        }
    }
}
