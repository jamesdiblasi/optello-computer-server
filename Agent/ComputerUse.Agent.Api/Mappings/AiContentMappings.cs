using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Api.Mappings
{
    public static class AiContentMappings
    {
        public static TextContentContract ToContract(this AITextContent entity) => new TextContentContract { Text = entity.Text };

        public static Base64ImageContentContract ToContract(this AIBase64ImageContent entity) => new Base64ImageContentContract 
        { 
            MediaType = entity.MediaType,
            Data = entity.Data,
        };

        public static IContentContract ToContract(this IAIContent entity) => entity switch
        {
            AITextContent textContent => textContent.ToContract(),
            AIBase64ImageContent base64ImageContent => base64ImageContent.ToContract(),
            _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Not expected AI content value")
        };

        public static IEnumerable<IContentContract> ToContracts(this IEnumerable<IAIContent> entities) => entities.Select(e => ToContract(e));
    }
}
