using ComputerUse.Contracts.WaitAndScreenshot;
using ComputerUse.Host.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ComputerUse.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class DisplayController : ControllerBase
{
    private readonly ILogger<DisplayController> logger;
    private readonly IMediator mediator;

    public DisplayController(ILogger<DisplayController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpPost("screenshot")]
    public async Task<IActionResult> Screenshot(WaitAndScreenshotRequest request)
    {
        logger.LogCritical("Executing screenshot");

        var commandResponse = await mediator.Send(request.ToCommand());

        logger.LogTrace("Executed screenshot");

        return Ok(commandResponse.ToContract());
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        logger.LogCritical("Executing ping");

        logger.LogTrace("Executed ping");

        return Ok($"(TOOLs) PONG {DateTime.Now.ToString()}");
    }
}
