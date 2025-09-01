using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core;

namespace ComputerUse.Agent.Api.Mappings
{
    public static class AIModelsMappings
    {
        public static AIModels ToDomainEntity(this AIModelsContract dto) => dto switch
        {
            AIModelsContract.Anthropic => AIModels.Anthropic,
            AIModelsContract.Gpt4 => AIModels.Gpt4,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Not expected AI model value: {dto}")
        };
    }
}
