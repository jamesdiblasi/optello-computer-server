using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Api.Mappings
{
    public static class AIRoleTypesMappings
    {
        public static RoleTypeContract ToContract(this AIRoleType entity) => entity switch
        {
            AIRoleType.User => RoleTypeContract.User,
            AIRoleType.Assistant => RoleTypeContract.Assistant,
            AIRoleType.System => RoleTypeContract.System,
            _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Not expected AI model value: {entity}")
        };
    }
}
