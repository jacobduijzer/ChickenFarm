using System.ComponentModel.DataAnnotations;

namespace ChickenFarm.TaskService.Domain
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}