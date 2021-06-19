using System;
using System.Threading.Tasks;
using ChickenFarm.Shared;
using ChickenFarm.TaskService.Application;
using Dapr;
using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.TaskService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IMediator mediator, ILogger<TasksController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{farmId}")]
        public async Task<IActionResult> Get(int farmId)
        {
            try
            {
                var farm = await _mediator.Send(new NewDailyTasksQuery.Query(farmId)).ConfigureAwait(false);
                return new OkObjectResult(farm);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new BadRequestObjectResult(exception.Message);
            }
        }

        [Topic("pubsub", "tasks")]
        [HttpPost("NewTaskPublished")]
        public async Task<IActionResult> Add(TaskDto task, [FromServices] DaprClient daprClient)
        {
            _logger.LogInformation($"TASKAPI: NEW TASK!: {task.ShedId}");
            return new OkObjectResult(task.ShedId);
        }
    }
}