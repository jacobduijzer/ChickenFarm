using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenFarm.TaskService.Domain
{
    public class Task : EntityBase
    {
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("ShedId")]
        public int ShedId { get; set; }
        public Shed Shed { get; set; }
    }
}