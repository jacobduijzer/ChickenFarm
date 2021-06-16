using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenFarm.TaskService.Domain
{
    public class Shed : EntityBase
    {
        [ForeignKey("FarmId")]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}