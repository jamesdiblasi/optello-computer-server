using Anthropic.SDK.Messaging;
using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Commands
{
    internal abstract class BaseImageCommand<TToolResponse> : BaseCommand<TToolResponse> where TToolResponse : BaseImageToolResponse
    {
        public BaseImageCommand(ITools tools) : base(tools) {}

        protected override (List<ContentBase>,bool) ConvertToolResponseToContent(TToolResponse toolResponse)
        {
            var (content, isError) = base.ConvertToolResponseToContent(toolResponse);

            if (isError)
            {
                return (content, isError);
            }

            if (!string.IsNullOrWhiteSpace(toolResponse.Base64File))
            {
                content.Add(new ImageContent()
                {
                    Source = new ImageSource()
                    {
                        Data = toolResponse.Base64File,
                        MediaType = "image/png" //TODO: Add to Tools API
                    }
                });
            }

            return (content, isError);
        }
    }
}
