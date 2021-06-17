using System.Collections.Generic;
using System.Threading.Tasks;
using ChickenFarm.Shared;

namespace ChickenFarm.FrontEnd.Domain
{
    public interface ITasksService
    {
        Task<IEnumerable<ShedDto>> GetTasks(int farmId);
    }
}