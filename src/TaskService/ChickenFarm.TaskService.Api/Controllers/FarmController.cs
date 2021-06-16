using System;
using System.Net;
using System.Threading.Tasks;
using ChickenFarm.TaskService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.TaskService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        [HttpGet("{farmId}")]
        public async Task<IActionResult> Get(
            int farmId,
            [FromServices] IMediator mediator,
            [FromServices] ILogger<FarmController> logger)
        {
            try
            {
                var farm = await mediator.Send(new FarmsQuery.Query(farmId)).ConfigureAwait(false);
                return new OkObjectResult(farm);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                return new BadRequestObjectResult(exception.Message);
            }
        }
    }
}