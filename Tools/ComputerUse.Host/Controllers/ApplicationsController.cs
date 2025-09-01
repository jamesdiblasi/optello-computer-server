using ComputerUse.Contracts.OpenBrowser;
using ComputerUse.Contracts.WaitAndScreenshot;
using ComputerUse.Host.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ComputerUse.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        //Applications/openBrowser
        private readonly ILogger<DisplayController> logger;
        private readonly IMediator mediator;

        public ApplicationsController(ILogger<DisplayController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("openBrowser")]
        public async Task<IActionResult> OpenBrowser(OpenBrowserRequest request)
        {
            logger.LogCritical("Executing openBrowser");

            var commandResponse = await mediator.Send(request.ToCommand());

            logger.LogTrace("Executed openBrowser");

            return Ok(commandResponse.ToContract());
        }
    }
}
