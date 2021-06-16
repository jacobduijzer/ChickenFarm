using ChickenFarm.Shared;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarm.FarmService.Infrastructure
{
    public class FarmContext : DbContext
    {
        public FarmContext(DbContextOptions<FarmContext> options)
            : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<Shed> Sheds { get; set; }
    }
}