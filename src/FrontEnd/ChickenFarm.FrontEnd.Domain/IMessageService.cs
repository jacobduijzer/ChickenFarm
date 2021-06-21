using System.Threading.Tasks;
using ChickenFarm.Shared;

namespace ChickenFarm.FrontEnd.Domain
{
    public interface IMessageService
    {
        Task SendMessage(MessageDto messageDto);
    }
}