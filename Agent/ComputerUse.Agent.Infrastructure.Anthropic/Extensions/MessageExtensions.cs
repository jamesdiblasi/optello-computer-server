using Anthropic.SDK.Messaging;
using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Extensions
{
    public static class MessageExtensions
    {
        public static AIMessage ToAIMessage(this Message that, string identifier)
        {
            var role = that.Role switch
            {
                RoleType.Assistant => AIRoleType.Assistant,
                _ => AIRoleType.User
            };

            List<IAIContent> content = new List<IAIContent>();

            foreach (var contentItem in that.Content)
            {
                if (contentItem is TextContent textContentItem)
                {
                    content.Add(new AITextContent
                    {
                        Text = textContentItem.Text
                    });
                }

                if (contentItem is ImageContent imageContent)
                {
                    content.Add(new AIBase64ImageContent
                    {
                        MediaType = imageContent.Source.MediaType,
                        Data = imageContent.Source.Data
                    });
                }

                if (contentItem is ToolResultContent toolResultContent)
                {
                    foreach (var toolResultContentItem in toolResultContent.Content)
                    {
                        if (toolResultContentItem is ImageContent imageContent2)
                        {
                            content.Add(new AIBase64ImageContent
                            {
                                MediaType = imageContent2.Source.MediaType,
                                Data = imageContent2.Source.Data
                            });
                        }

                        if (toolResultContentItem is TextContent textContentItem2)
                        {
                            content.Add(new AITextContent
                            {
                                Text = textContentItem2.Text
                            });
                        }
                    }
                }
            }

            return new AIMessage { Identifier = identifier, Role = role, Content = content };
        }
    }
}
