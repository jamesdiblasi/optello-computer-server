using ComputerUse.Core.EnvironmentTools.Keyboard;
using ComputerUse.Core.EnvironmentTools.ScreenshotMaker;
using MediatR;

namespace ComputerUse.Application.TypeText
{
    public class TypeTextCommandHandler : IRequestHandler<TypeTextCommand, BaseImageCommandResponse>
    {
        public const int TYPING_GROUP_SIZE = 50;

        private readonly IKeyboardTool keyboardTool;
        private readonly IScreenshotMaker screenshotMaker;

        public TypeTextCommandHandler(IKeyboardTool keyboardTool, IScreenshotMaker screenshotMaker)
        {
            this.keyboardTool = keyboardTool;
            this.screenshotMaker = screenshotMaker;
        }

        private List<string> SplitStringIntoChuncks(string str, int chunkSize)
        {
            int maxRange = Convert.ToInt32(Math.Ceiling((double)str.Length / chunkSize));

            return Enumerable.Range(0, maxRange)
                .Select(i => {
                    var startIndex = i * chunkSize;
                    var length = startIndex + chunkSize > str.Length ? str.Length - startIndex : chunkSize;
                    var result = str.Substring(startIndex, length);

                    return result;
                }).ToList();
        }

        public async Task<BaseImageCommandResponse> Handle(TypeTextCommand request, CancellationToken cancellationToken)
        {
            var chunks = SplitStringIntoChuncks(request.Text, TYPING_GROUP_SIZE);

            foreach(var chunk in chunks)
            {
                await keyboardTool.TypeTextAsync(new TypedText { Text = request.Text });
            }

            // delay to let things settle before taking a screenshot 
            await Task.Delay(TimeSpan.FromSeconds(2));

            var screenshot = await screenshotMaker.TakeScreenshotAsync();

            return new BaseImageCommandResponse {Base64File = screenshot.Base64File, Error = "", Output = "" };
        }
    }
}
