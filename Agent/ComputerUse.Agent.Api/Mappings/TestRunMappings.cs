using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core;

namespace ComputerUse.Agent.Api.Mappings
{
    public static class TestRunMappings
    {
        public static TestRun ToDomainEntity(this TestRunContract dto) => new TestRun 
        {
            Name = dto.Name,
            AiModel = dto.AiModel.ToDomainEntity(),
            TargetUrl = dto.TargetUrl,
            Steps = dto.Steps 
        };
    }
}
