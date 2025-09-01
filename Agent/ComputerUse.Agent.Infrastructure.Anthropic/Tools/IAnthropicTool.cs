using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTool = Anthropic.SDK.Common.Tool;
using CommonFunction = Anthropic.SDK.Common.Function;
using Anthropic.SDK.Messaging;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Tools
{
    internal interface IAnthropicTool
    {
        string ToolName { get; }
        CommonTool ToolRegistration { get; }

        ToolExecutionContext? GetToolExecutionContext(ToolUseContent content, string identifier);
    }
}
