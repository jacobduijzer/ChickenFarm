using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChickenFarm.TaskService.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarm.TaskService.Infrastructure
{
    public class FarmRepository : IRepository<Farm>
    {
        private readonly TaskDbContext _dbContext;

        public FarmRepository(TaskDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<Farm>> Get(Expression<Func<Farm, bool>> expression) =>
            await _dbContext.Farms
                .Where(expression)
                .Include(x => x.Sheds)
                .ThenInclude(x => x.Tasks)
                .ToListAsync()
                .ConfigureAwait(false);
    }
}