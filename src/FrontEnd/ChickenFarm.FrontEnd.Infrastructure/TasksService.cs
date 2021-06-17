using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ChickenFarm.FrontEnd.Domain;
using ChickenFarm.Shared;
using ChickenFarm.TaskService.Domain;
using Dapr.Client;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.FrontEnd.Infrastructure
{
    public class TasksService : ITasksService
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<TasksService> _tasksService;

        public TasksService(DaprClient daprClient, ILogger<TasksService> tasksService)
        {
            _daprClient = daprClient;
            _tasksService = tasksService;
        }

        public async Task<IEnumerable<ShedDto>> GetTasks(int farmId) =>
            await _daprClient.InvokeMethodAsync<IEnumerable<ShedDto>>(HttpMethod.Get, "chickenfarm-taskservice-api", $"Tasks/{farmId}")
                .ConfigureAwait(false);
    }
}