using Anthropic.SDK.Messaging;
using CommonTool = Anthropic.SDK.Common.Tool;
using CommonFunction = Anthropic.SDK.Common.Function;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace ComputerUse.Agent.Infrastructure.Anthropic.Tools
{
    internal class OpenBrowserTool : IAnthropicTool
    {
        public const string TOOL_NAME = "open_browser";
        public const string TOOL_URL_PROP_NAME = "url";

        public string ToolName { get; private set; }

        public CommonTool ToolRegistration { get; private set; }

        public OpenBrowserTool() 
        {
            ToolName = TOOL_NAME;

            var inputschema = new InputSchema()
            {
                Type = "object",
                Properties = new Dictionary<string, Property>()
                {
                    { TOOL_URL_PROP_NAME, new Property() { Type = "string", Description = "The url of the website." } }
                },
                Required = new List<string>() { TOOL_URL_PROP_NAME }
            };

            JsonSerializerOptions jsonSerializationOptions = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() },
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };

            string jsonString = JsonSerializer.Serialize(inputschema, jsonSerializationOptions);

            ToolRegistration = new CommonFunction(ToolName, "This function open browser with passed url.", JsonNode.Parse(jsonString));
        }

        public ToolExecutionContext? GetToolExecutionContext(ToolUseContent content, string identifier)
        {
            var context = new CommandContext
            {
                Identifier = identifier,
                Text = content.Input[TOOL_URL_PROP_NAME]?.ToString(),
                ToolId = content.Id
            };

            return new ToolExecutionContext { Action = TOOL_NAME, Context = context };
        }
    }
}
