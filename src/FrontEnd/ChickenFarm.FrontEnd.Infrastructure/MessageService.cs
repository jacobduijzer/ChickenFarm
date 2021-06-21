using System.Threading.Tasks;
using ChickenFarm.FrontEnd.Domain;
using ChickenFarm.Shared;
using Dapr.Client;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.FrontEnd.Infrastructure
{
    public class MessageService : IMessageService
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<MessageService> _tasksService;

        public MessageService(DaprClient daprClient, ILogger<MessageService> tasksService)
        {
            _daprClient = daprClient;
            _tasksService = tasksService;
        }

        public async Task SendMessage(MessageDto messageDto) =>
            await _daprClient.PublishEventAsync<MessageDto>("pubsub", "messages", messageDto).ConfigureAwait(false);
    }
}