using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Core
{
    public interface IToolsOrchestrationManager
    {
        Task<ITools> AcquireTool();

        Task ReleaseTool(ITools tool);
    }
}
