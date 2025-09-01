using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ComputerUse.Agent.Contracts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AIModelsContract
    {
        [EnumMember(Value = "gpt4")]
        Gpt4,
        [EnumMember(Value = "anthropic")]
        Anthropic
    }
}
