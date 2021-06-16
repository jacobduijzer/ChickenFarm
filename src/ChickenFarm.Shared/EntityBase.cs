using System.ComponentModel.DataAnnotations;

namespace ChickenFarm.Shared
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}