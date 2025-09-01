using ComputerUse.Core.EnvironmentTools.Keyboard;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.PressKey
{
    public class PressKeyCommandHandler : IRequestHandler<PressKeyCommand, BaseImageCommandResponse>
    {
        public const int TYPING_GROUP_SIZE = 50;

        private readonly IKeyboardTool keyboardTool;
        private readonly IScreenshotMaker screenshotMaker;

        public PressKeyCommandHandler(IKeyboardTool keyboardTool, IScreenshotMaker screenshotMaker)
        {
            this.keyboardTool = keyboardTool;
            this.screenshotMaker = screenshotMaker;
        }

        public async Task<BaseImageCommandResponse> Handle(PressKeyCommand request, CancellationToken cancellationToken)
        {
            if (request.Duration is int duration && (duration > 0 ))
            {
                await keyboardTool.HoldKeyAsync(new HoldenKey { Text = request.Text, Duration = duration });
            } 
            else
            {
                await keyboardTool.PressKeyAsync(new Key { Text = request.Text });
            }

            // delay to let things settle before taking a screenshot 
            await Task.Delay(TimeSpan.FromSeconds(2));

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse { Base64File = screenshot.Base64File, Error = "", Output = "" };
        }
    }
}
