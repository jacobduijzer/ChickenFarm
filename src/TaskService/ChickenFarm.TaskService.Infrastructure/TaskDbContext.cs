using ChickenFarm.TaskService.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarm.TaskService.Infrastructure
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<Shed> Sheds { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}