using System.Text.Json.Serialization;

namespace ComputerUse.Agent.Core.Messages
{
    public record AIMessage
    {
        public int? Id { get; init; }

        public required string Identifier { get; init; }

        public required AIRoleType Role { get; init; }

        public required IList<IAIContent> Content { get; init; }

        public string? DebugInfo { get; set; }
    }
}
