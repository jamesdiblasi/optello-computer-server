using ComputerUse.Agent.Core.Tools.DragAndDropItem;
using ComputerUse.Agent.Core.Tools.GetCursorPosition;
using ComputerUse.Agent.Core.Tools.MoveCursorAndClickWithPressedKey;
using ComputerUse.Agent.Core.Tools.MoveCursorAndScrollWithPressedKey;
using ComputerUse.Agent.Core.Tools.MoveCursorPosition;
using ComputerUse.Agent.Core.Tools.OpenBrowser;
using ComputerUse.Agent.Core.Tools.PressKey;
using ComputerUse.Agent.Core.Tools.Screenshot;
using ComputerUse.Agent.Core.Tools.TypeText;

namespace ComputerUse.Agent.Core.Tools
{
    public enum ToolStatus { Running, Stopped };

    public interface ITools
    {
        string Id { get; }

        Task<ToolStatus> GetStatus();

        Task Start();

        Task Stop();

        Task<BaseImageToolResponse> DragAndDropItemAsync(DragAndDropItemRequest request);

        Task<GetCursorPositionResponse> GetCursorPositionAsync(BaseToolRequest request);

        Task<BaseImageToolResponse> MoveCursorPositionAsync(MoveCursorPositionRequest request);

        Task<BaseImageToolResponse> MoveCursorAndClickWithPressedKeyAsync(MoveCursorAndClickWithPressedKeyRequest request);

        Task<BaseImageToolResponse> MoveCursorAndScrollWithPressedKeyAsync(MoveCursorAndScrollWithPressedKeyRequest request);

        Task<BaseImageToolResponse> PressKeyAsync(PressKeyRequest request);

        Task<BaseImageToolResponse> TypeTextAsync(TypeTextRequest request);

        Task<BaseImageToolResponse> TakeScreenshotAsync(WaitAndScreenshotRequest request);

        Task<BaseImageToolResponse> OpenBrowserAsync(OpenBrowserRequest request);
    }
}
