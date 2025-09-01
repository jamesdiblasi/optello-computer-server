using Anthropic.SDK.Messaging;

namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    internal interface ICommand
    {
        Task<ToolResultContent> ExecuteAsync(CommandContext context);
    }
}
