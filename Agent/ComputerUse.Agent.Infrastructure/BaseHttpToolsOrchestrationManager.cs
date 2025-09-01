using ComputerUse.Agent.Core;
using ComputerUse.Agent.Core.Tools;
using System.Collections.Concurrent;

namespace ComputerUse.Agent.Infrastructure
{
    internal class BaseHttpToolsOrchestrationManager : IToolsOrchestrationManager
    {
        private SemaphoreSlim semaphoreSlim;
        private ConcurrentQueue<ITools> queue;

        public BaseHttpToolsOrchestrationManager(IEnumerable<ITools> tools)
        {
            semaphoreSlim = new SemaphoreSlim(tools.Count());
            queue = new ConcurrentQueue<ITools>(tools);
        }

        public async Task<ITools> AcquireTool()
        {
            await semaphoreSlim.WaitAsync();

            if(!queue.TryDequeue(out var tool))
            {
                throw new Exception("Could not get tool.");
            }

            return tool;
        }

        public Task ReleaseTool(ITools tool)
        {
            queue.Enqueue(tool);

            semaphoreSlim.Release();

            return Task.CompletedTask;
        }
    }
}
