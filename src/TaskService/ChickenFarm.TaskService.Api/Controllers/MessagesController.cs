using System.Threading.Tasks;
using ChickenFarm.Shared;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.TaskService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        [Topic("pubsub", "messages")]
        [HttpPost("NewTaskPublished")]
        public async Task<IActionResult> Add(MessageDto messageDto, [FromServices] DaprClient daprClient)
        {
            _logger.LogInformation($"NEW MESSAGE: {messageDto.Message}");
            return Ok();
        }
    }
}