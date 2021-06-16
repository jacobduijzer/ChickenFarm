using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenFarm.Shared
{
    public class Shed : EntityBase
    {
        [ForeignKey("FarmId")]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
    }
}