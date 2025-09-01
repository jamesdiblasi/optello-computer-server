using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using ComputerUse.Agent.Core;
using System.Text.Json.Nodes;

using CommonTools = Anthropic.SDK.Common.Tool;
using CommonFunction = Anthropic.SDK.Common.Function;
using ComputerUse.Agent.Core.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ComputerUse.Agent.Infrastructure.Anthropic.Options;
using ComputerUse.Agent.Core.Messages;
using ComputerUse.Agent.Infrastructure.Anthropic.Extensions;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ComputerUse.Agent.Infrastructure.Anthropic.Tools;
using System;

namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    internal class AnthropicAIClient : IAIClient
    {
        private readonly string SYSTEM_PROMPT = "<SYSTEM_CAPABILITY>"
            + $" You are utilising an Ubuntu virtual machine using {(Environment.Is64BitOperatingSystem ? 64 : 32) + "-bit"} architecture with internet access."
            //+ " You can feel free to install Ubuntu applications with your bash tool. Use curl instead of wget."
            //+ " To open firefox, please just click on the firefox icon.  Note, firefox-esr is what is installed on your system. Once the firefox shows, try to open new tab and do the requested actions there."
            + " To open firefox, please run the following command \"open_browser\" with url as parameter."
            + " After the first page loads, look at the page and accept all the privacy preferences pop-ups. "
            //+ " Using bash tool you can start GUI applications, but you need to set export DISPLAY=:1 and use a subshell. For example \"(DISPLAY=:1 xterm &)\". "
            + " GUI apps run with bash tool will appear within your desktop environment, but they may take some time to appear. Take a screenshot to confirm it did."
            //+ " When using your bash tool with commands that are expected to output very large quantities of text, redirect into a tmp file and use str_replace_editor or `grep -n -B <lines before> -A <lines after> <query> <filename>` to confirm output."
            + " When viewing a page it can be helpful to zoom out so that you can see everything on the page. Either that, or make sure you scroll down to see everything before deciding something isn't available."
            + " When using your computer function calls, they take a while to run and send back to you.  Where possible/feasible, try to chain multiple of these calls all into one function calls request."
            + $" The current date is {DateTime.Today.ToShortDateString()}."
        + "</ SYSTEM_CAPABILITY >"
        + "<IMPORTANT>"
            + " When using Firefox, if a startup wizard appears, IGNORE IT.  Do not even click \"skip this step\".  Instead, click on the address bar where it says \"Search or enter address\", and enter the appropriate search term or URL there."
        //+ " If the item you are looking at is a pdf, if after taking a single screenshot of the pdf it seems that you want to read the entire document instead of trying to continue to read the pdf from your screenshots + navigation, determine the URL, use curl to download the pdf, install and use pdftotext to convert it to a text file, and then read that text file directly with your StrReplaceEditTool."
        + "</IMPORTANT>";

        private readonly ILogger<AnthropicAIClient> logger;
        private readonly CommandFactory commandFactory;
        private readonly ComputerUseInfrastructureAnthropicOptions options;
        private readonly IServiceProvider serviceProvider;
        private readonly IToolsOrchestrationManager toolsOrchestrationManager;

        public AnthropicAIClient(CommandFactory commandFactory, ILogger<AnthropicAIClient> logger, 
            IOptions<ComputerUseInfrastructureAnthropicOptions> optionsSlice, IServiceProvider serviceProvider, 
            IToolsOrchestrationManager toolsOrchestrationManager)
        {
            this.commandFactory = commandFactory;
            this.logger = logger;
            this.options = optionsSlice.Value;
            this.serviceProvider = serviceProvider;
            this.toolsOrchestrationManager = toolsOrchestrationManager;
        }

        /*
         def _inject_prompt_caching(
    messages: list[BetaMessageParam],
):
    """
    Set cache breakpoints for the 3 most recent turns
    one cache breakpoint is left for tools/system prompt, to be shared across sessions
    """

    breakpoints_remaining = 3
    for message in reversed(messages):
        if message["role"] == "user" and isinstance(
            content := message["content"], list
        ):
            if breakpoints_remaining:
                breakpoints_remaining -= 1
                # Use type ignore to bypass TypedDict check until SDK types are updated
                content[-1]["cache_control"] = BetaCacheControlEphemeralParam(  # type: ignore
                    {"type": "ephemeral"}
                )
            else:
                content[-1].pop("cache_control", None)
                # we'll only every have one extra turn per loop
                break
         */
        /*
         _inject_prompt_caching(messages)
            # Because cached reads are 10% of the price, we don't think it's
            # ever sensible to break the cache by truncating images
            only_n_most_recent_images = 0
            # Use type ignore to bypass TypedDict check until SDK types are updated
            system["cache_control"] = {"type": "ephemeral"}  # type: ignore
         */
        private void SetCacheControls(MessageParameters parameters, int numberItemsToCache = 3)
        {
            /*
                Set cache breakpoints for the 3 most recent turns
                one cache breakpoint is left for tools/system prompt, to be shared across sessions
            */

            var currentCachedItems = 0;

            foreach (var message in parameters.Messages.AsEnumerable().Reverse())
            {
                if (currentCachedItems < numberItemsToCache 
                    && message.Role == RoleType.User 
                    && message.Content?.Count == 0)
                {
                    currentCachedItems++;

                    foreach (var contentItem in message.Content)
                    {
                        contentItem.CacheControl = new CacheControl { Type = CacheControlType.ephemeral };
                    }
                } 
                else
                {
                    foreach (var contentItem in message.Content ?? [])
                    {
                        contentItem.CacheControl = new CacheControl { Type = CacheControlType.ephemeral };
                    }
                }
            }

            // set cache for SYSTEM messages
            foreach (var systemMessage in parameters.System ?? [])
            {
                systemMessage.CacheControl = new CacheControl { Type = CacheControlType.ephemeral };
            }
        }

        private AIMessage GetSystemMessage(string identifier, string text) => new AIMessage
        {
            Identifier = identifier,
            Role = AIRoleType.System,
            Content = new List<IAIContent>() { new AITextContent { Text = text } }
        };

        public async IAsyncEnumerable<AIMessage> ExecuteAsync(TestRun testRun, [EnumeratorCancellation] CancellationToken token)
        {
            var identifier = Guid.CreateVersion7().ToString();
            //var keys = new APIAuthentication(options.ApiKey);

            //using (var client = new global::Anthropic.SDK.AnthropicClient(keys))
            using (var client = serviceProvider.GetRequiredService<AnthropicClient>())
            {
                client.AnthropicBetaVersion += ",computer-use-2025-01-24";
                var initialContent = testRun.Steps.Select(step => new TextContent()
                {
                    Text = step
                });

                var initialUserMessage = new Message()
                {
                    Role = RoleType.User,
                    Content = [new TextContent { Text = $"Open the following url:{testRun.TargetUrl} and do the steps on it." }, .. initialContent]
                };

                token.ThrowIfCancellationRequested();

                yield return initialUserMessage.ToAIMessage(identifier);

                var messages = new List<Message>
                {
                    initialUserMessage
                };

                var tools = serviceProvider.GetKeyedServices<IAnthropicTool>(KeyedService.AnyKey).Select(t => t.ToolRegistration).ToList();

                var parameters = new MessageParameters()
                {
                    Messages = messages,
                    MaxTokens = this.options.MaxTokens,
                    Model = AnthropicModels.Claude4Sonnet, //Claude35Sonnet - 3.5 (computer_20241022) OR Claude4Sonnet - 4.0  Sonnet with Thinking (computer_20250124) [https://docs.anthropic.com/en/docs/agents-and-tools/tool-use/computer-use-tool#claude-4-models]
                    Stream = false,
                    Temperature = 0m,
                    Tools = tools,
                    System = new List<SystemMessage>()
                    {
                        new SystemMessage(SYSTEM_PROMPT)
                    },
                    PromptCaching = PromptCacheType.FineGrained
                };

                ITools? tool = null;

                try
                {
                    yield return GetSystemMessage(identifier, "Trying to acquire the free tools. Please wait...");

                    tool = await toolsOrchestrationManager.AcquireTool();

                    yield return GetSystemMessage(identifier, "Check the status of the acquired tools. Please wait...");

                    if (await tool.GetStatus() != ToolStatus.Running) {
                        yield return GetSystemMessage(identifier, "Starting the tools");

                        await tool.Start();

                        yield return GetSystemMessage(identifier, "The tools was successfully started.");
                    }
                    else
                    {
                        yield return GetSystemMessage(identifier, "The tools was initialized.");
                    }

                    var stillRunning = true;

                    while (stillRunning)
                    {
                        //logger.LogDebug($"[Claudi] request: {JsonSerializer.Serialize(parameters)}");
                        //logger.LogDebug($"[Claudi] BEFORE_MESSAGES: {JsonSerializer.Serialize(parameters.Messages)}");

                        MessageResponse res;

                        try
                        {
                            if (parameters.PromptCaching == PromptCacheType.FineGrained)
                            {
                                SetCacheControls(parameters);
                            }

                            res = await client.Messages.GetClaudeMessageAsync(parameters);
                            messages.Add(res.Message);
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogError(ex, "Exception occurs during make a request to AI client.");
                            throw;
                        }

                        token.ThrowIfCancellationRequested();

                        yield return res.Message.ToAIMessage(identifier);

                        //logger.LogDebug($"[Claudi] AFTER_MESSAGES: {JsonSerializer.Serialize(parameters.Messages)}");
                        //logger.LogDebug($"[Claudi] response: {JsonSerializer.Serialize(res)}");

                        var toolUse = res.Content.OfType<ToolUseContent>().ToList();

                        if (toolUse.Count == 0)
                        {
                            stillRunning = false;
                            break;
                        }

                        var toolResultContentList = new List<ContentBase>();

                        foreach (var toolUseContent in toolUse)
                        {
                            yield return new AIMessage
                            {
                                Identifier = identifier,
                                Role = AIRoleType.System,
                                Content = new List<IAIContent>() { new AITextContent { Text = $"Tool use: {toolUseContent.Name}; Input: {toolUseContent.Input.ToJsonString()}" } }
                            };

                            var anthropicTool = this.serviceProvider.GetKeyedService<IAnthropicTool>(toolUseContent.Name);

                            if (anthropicTool is null)
                            {
                                var exceptionText = $"Tool with the name:\"{toolUseContent.Name}\" did not registry. Action: \"{(toolUseContent?.Input?["action"]?.ToString() ?? "<EMPTY>")}\"";

                                logger.LogError(exceptionText);

                                throw new Exception(exceptionText);
                            }

                            var executionContext = anthropicTool.GetToolExecutionContext(toolUseContent, identifier);

                            if (executionContext is null)
                            {
                                continue;
                            }

                            token.ThrowIfCancellationRequested();

                            try
                            {
                                var command = commandFactory.CreateCommand(executionContext.Action, tool);

                                var toolResultContent = await command.ExecuteAsync(executionContext.Context);

                                toolResultContentList.Add(toolResultContent);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Exception occurs during execution \"{executionContext.Action}\" action with the following context: {executionContext.Context.ToString()}");

                                throw;
                            }
                        }

                        var userMessage = new Message
                        {
                            Role = RoleType.User,
                            Content = toolResultContentList
                        };

                        // Add mesage with tool results
                        messages.Add(userMessage);

                        token.ThrowIfCancellationRequested();

                        yield return userMessage.ToAIMessage(identifier);
                    }

                    yield return new AIMessage
                    {
                        Identifier = identifier,
                        Role = AIRoleType.System,
                        Content = new List<IAIContent>() { new AITextContent { Text = "Restarting the tools" } }
                    };
                }
                finally
                {
                    if (tool is not null)
                    {
                        //try to stop tool
                        try
                        {
                            logger.LogInformation($"Stoping tool: {tool.Id}");
                            await tool.Stop();
                            logger.LogInformation($"The tool {tool.Id} successfully stoped.");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Failed to stop tool: {tool}");
                        }

                        //try to start tool
                        try
                        {
                            if (await tool.GetStatus() == ToolStatus.Stopped)
                            {
                                logger.LogInformation($"Starting tool: {tool.Id}");
                                await tool.Start();
                                logger.LogInformation($"The tool {tool.Id} successfully started.");
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Failed to start tool: {tool}");
                        }

                        // release tool after stop
                        await toolsOrchestrationManager.ReleaseTool(tool);
                    }
                }
            }
        }
    }
}
