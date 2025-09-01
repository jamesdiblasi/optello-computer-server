using ComputerUse.Agent.Contracts;
using ComputerUse.Agent.Core;
using ComputerUse.Agent.Core.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ComputerUse.Agent.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AgentController : ControllerBase
{
    private readonly ILogger<AgentController> logger;
    private readonly IAIClient client;

    public AgentController(ILogger<AgentController> logger, IAIClient client)
    {
        this.logger = logger;
        this.client = client;
    }

    [HttpPost("TestRuns")]
    public async Task<IActionResult> StartNewTestRun(TestRunContract testRun)
    {
        //await testClient.ExecuteAsync(new TestRun { Steps = testRun.Steps }, CancellationToken.None);

        return Ok();
    }

    [HttpGet("Ping")]
    public IActionResult Ping()
    {
        return Ok($"(AGENT) PONG - {DateTime.Now}");
    }
}
