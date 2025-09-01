using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal class GetCursorPositionCommand : BaseCommand<BaseToolResponse>
    {
        public GetCursorPositionCommand(ITools tools) : base(tools) {}

        protected override async Task<BaseToolResponse> ExecuteToolAsync(CommandContext context)
        {
            var cursorPositionResponse = await this.tools.GetCursorPositionAsync(new BaseToolRequest { Identifier = context.Identifier});

            var output = string.IsNullOrWhiteSpace(cursorPositionResponse.Error) ? $"X={cursorPositionResponse.X},Y={cursorPositionResponse.Y}" : string.Empty;

            return new BaseToolResponse 
            { 
                Error = cursorPositionResponse.Error, 
                System = cursorPositionResponse.System, 
                Output = output 
            };
        }
    }
}
