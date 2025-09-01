using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ComputerUse.Agent.Contracts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleTypeContract
    {
        [EnumMember(Value = "user")]
        User,
        [EnumMember(Value = "assistant")]
        Assistant,
        [EnumMember(Value = "system")]
        System
    }
}
