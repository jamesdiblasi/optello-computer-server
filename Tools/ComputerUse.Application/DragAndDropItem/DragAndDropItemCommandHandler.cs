using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.DragAndDropItem
{
    public class DragAndDropItemCommandHandler : IRequestHandler<DragAndDropItemCommand, BaseImageCommandResponse>
    {
        private readonly IMouseTool mouseAutomationTool;
        private readonly IScreenshotMaker screenshotMaker;

        public DragAndDropItemCommandHandler(IMouseTool mouseAutomationTool, IScreenshotMaker screenshotMaker)
        {
            this.mouseAutomationTool = mouseAutomationTool;
            this.screenshotMaker = screenshotMaker;
        }

        public async Task<BaseImageCommandResponse> Handle(DragAndDropItemCommand request, CancellationToken cancellationToken)
        {
            await this.mouseAutomationTool.ClickAsync(MouseClickOptions.LeftMouseDown);
            await this.mouseAutomationTool.MoveAsync(new CursorPosition { X = request.X, Y = request.Y});
            await this.mouseAutomationTool.ClickAsync(MouseClickOptions.LeftMouseUp);

            // delay to let things settle before taking a screenshot 
            await Task.Delay(TimeSpan.FromSeconds(2));

            var screenshot = await this.screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse { Base64File = screenshot.Base64File, Output = "", Error = "" };
        }
    }
}
