using ComputerUse.Contracts.PressKey;
using ComputerUse.Contracts.TypeText;
using ComputerUse.Host.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ComputerUse.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyboardController : ControllerBase
{
    private readonly ILogger<KeyboardController> logger;
    private readonly IMediator mediator;

    public KeyboardController(ILogger<KeyboardController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpPost("pressKey")]
    public async Task<IActionResult> PressKey(PressKeyRequest contract)
    {
        logger.LogCritical("Executing PressKey");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed PressKey");

        return Ok(commandResponse.ToContract());
    }

    [HttpPost("typeText")]
    public async Task<IActionResult> TypeText(TypeTextRequest contract)
    {
        logger.LogCritical("Executing TypeText");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed TypeText");

        return Ok(commandResponse.ToContract());
    }

}
