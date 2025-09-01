using Anthropic.SDK.Messaging;
using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal abstract class BaseCommand<TToolResponse> : ICommand where TToolResponse : BaseToolResponse
    {
        protected readonly ITools tools;

        public BaseCommand(ITools tools)
        {
            this.tools = tools;
        }

        protected abstract Task<TToolResponse> ExecuteToolAsync(CommandContext context);

        public async Task<ToolResultContent> ExecuteAsync(CommandContext context)
        {
            var toolResponse = await ExecuteToolAsync(context);
            var (content, isError) = ConvertToolResponseToContent(toolResponse);

            return new ToolResultContent()
            {
                ToolUseId = context.ToolId,
                IsError = isError,
                Content = content
            };
        }

        protected string CombineTextContentWithSystem(string textContent, string? systemText) => !string.IsNullOrWhiteSpace(systemText) ? $"<system>{systemText}</system>\n{textContent}" : textContent;

        protected virtual (List<ContentBase>,bool) ConvertToolResponseToContent(TToolResponse toolResponse)
        {
            var content = new List<ContentBase>();
            var isError = false;

            if (!string.IsNullOrWhiteSpace(toolResponse.Error))
            {
                content.Add(new TextContent
                {
                    Text = CombineTextContentWithSystem(toolResponse.Error, toolResponse.System)
                });
                isError = true;
            } 
            else
            {
                if (!string.IsNullOrWhiteSpace(toolResponse.Output))
                {
                    content.Add(new TextContent
                    {
                        Text = CombineTextContentWithSystem(toolResponse.Output, toolResponse.System)
                    });
                }
            }

            return (content, isError);
        }
    }
}
