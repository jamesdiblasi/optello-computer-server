using ComputerUse.Agent.Core.Messages;

namespace ComputerUse.Agent.Core
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(AIMessage message);

        Task<IList<AIMessage>> GelAllMessages(string identifier);
    }
}
