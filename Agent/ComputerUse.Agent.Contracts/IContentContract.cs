using System.Text.Json.Serialization;

namespace ComputerUse.Agent.Contracts
{
    [JsonDerivedType(typeof(Base64ImageContentContract), typeDiscriminator: "base64Image")]
    [JsonDerivedType(typeof(TextContentContract), typeDiscriminator: "text")]
    public interface IContentContract
    {
    }
}
