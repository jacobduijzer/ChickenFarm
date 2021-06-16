using System.Linq;
using ChickenFarm.Shared;

namespace ChickenFarm.FarmService.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static void AddTestData(this FarmContext farmContext)
        {
            if (!farmContext.Farms.Any())
            {
                farmContext.Farms.Add(new Farm());
                farmContext.SaveChanges();
            }

            if (!farmContext.Sheds.Any())
            {

            }
        }

    }
}