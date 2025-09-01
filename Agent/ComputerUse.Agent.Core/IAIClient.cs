using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Core
{
    public interface IAIClient
    {
        IAsyncEnumerable<AIMessage> ExecuteAsync(TestRun testRun, CancellationToken token);
    }
}
