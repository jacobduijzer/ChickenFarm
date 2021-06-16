using System.Collections.Generic;

namespace ChickenFarm.TaskService.Domain
{
    public class Farm : EntityBase
    {
        public ICollection<Shed> Sheds { get; set; }
    }
}