namespace ComputerUse.Agent.Contracts
{
    public record MessageContract
    {
        public int? Id { get; init; }

        public required string Identifier { get; init; }

        public required RoleTypeContract Role { get; init; }

        public required IEnumerable<IContentContract> Content { get; init; }
    }
}
