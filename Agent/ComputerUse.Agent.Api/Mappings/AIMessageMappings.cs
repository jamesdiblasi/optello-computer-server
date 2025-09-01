using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Api.Mappings
{
    public static class AIMessageMappings
    {
        public static MessageContract ToContract(this AIMessage entity) => new MessageContract 
        {  
            Id = entity.Id,
            Identifier = entity.Identifier,
            Role = entity.Role.ToContract(),
            Content = entity.Content.ToContracts()
        };
    }
}
