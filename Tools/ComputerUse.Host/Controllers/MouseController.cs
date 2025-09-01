using ComputerUse.Application.GetCursorPosition;
using ComputerUse.Contracts.DragAndDropItem;
using ComputerUse.Contracts.MoveCursorAndClickWithPressedKey;
using ComputerUse.Contracts.MoveCursorAndScrollWithPressedKey;
using ComputerUse.Contracts.MoveCursorPosition;
using ComputerUse.Host.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ComputerUse.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class MouseController : ControllerBase
{
    private readonly ILogger<DisplayController> logger;
    private readonly IMediator mediator;

    public MouseController(ILogger<DisplayController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("position")]
    public async Task<IActionResult> Position()
    {
        logger.LogTrace("Executing position");

        var commandResponse = await mediator.Send(new GetCursorPositionCommand());

        logger.LogTrace("Executed dragItem");

        return Ok(commandResponse.ToContract());
    }

    [HttpPost("position")]
    public async Task<IActionResult> Move(MoveCursorPositionRequest contract)
    {
        logger.LogCritical("Executing dragItem");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed dragItem");

        return Ok(commandResponse.ToContract());
    }

    [HttpPost("dragAndDropItem")]
    public async Task<IActionResult> DragAndDropItem(DragAndDropItemRequest contract)
    {
        logger.LogCritical("Executing dragItem");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed dragItem");

        return Ok(commandResponse.ToContract());
    }

    [HttpPost("moveCursorAndClickWithPressedKey")]
    public async Task<IActionResult> Click(MoveCursorAndClickWithPressedKeyRequest contract)
    {
        logger.LogCritical("Executing moveCursorAndClickWithPressedKey");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed moveCursorAndClickWithPressedKey");

        return Ok(commandResponse.ToContract());
    }

    [HttpPost("moveCursorAndScrollWithPressedKey")]
    public async Task<IActionResult> MoveCursorAndScrollWithPressedKey(MoveCursorAndScrollWithPressedKeyRequest contract)
    {
        logger.LogCritical("Executing MoveCursorAndScrollWithPressedKey");

        var commandResponse = await mediator.Send(contract.ToCommand());

        logger.LogTrace("Executed MoveCursorAndScrollWithPressedKey");

        return Ok(commandResponse.ToContract());
    }
}
