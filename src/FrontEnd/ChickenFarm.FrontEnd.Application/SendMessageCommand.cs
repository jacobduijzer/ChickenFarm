using System.Threading;
using System.Threading.Tasks;
using ChickenFarm.FrontEnd.Domain;
using ChickenFarm.Shared;
using MediatR;

namespace ChickenFarm.FrontEnd.Application
{
    public class SendMessageCommand
    {
        public record Command(string Message) : INotification;

        public class Handler : INotificationHandler<Command>
        {
            private readonly IMessageService _messageService;

            public Handler(IMessageService messageService)
            {
                _messageService = messageService;
            }

            public async Task Handle(Command notification, CancellationToken cancellationToken) =>
                await _messageService.SendMessage(new MessageDto { Message = notification.Message }).ConfigureAwait(false);
        }
    }
}