using ComputerUse.Core.EnvironmentTools.MouseAutomation;
using MediatR;

namespace ComputerUse.Application.GetCursorPosition
{
    public class GetCursorPositionCommandHandler : IRequestHandler<GetCursorPositionCommand, GetCursorPositionCommandResponse>
    {
        private readonly IMouseTool mouseTool;

        public GetCursorPositionCommandHandler(IMouseTool mouseTool)
        {
            this.mouseTool = mouseTool;
        }

        public async Task<GetCursorPositionCommandResponse> Handle(GetCursorPositionCommand request, CancellationToken cancellationToken)
        {
            var cursorPosition = await mouseTool.GetCursorPositionAsync();

            return new GetCursorPositionCommandResponse { X = cursorPosition.X, Y = cursorPosition.Y, Output = "", Error = "" };
        }
    }
}
