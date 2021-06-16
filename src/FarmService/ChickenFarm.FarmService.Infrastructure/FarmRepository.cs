using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChickenFarm.Shared;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarm.FarmService.Infrastructure
{
    public class FarmRepository : IRepository<Farm>
    {
        private readonly FarmContext _farmContext;

        public FarmRepository(FarmContext farmContext) => _farmContext = farmContext;

        public async Task<IEnumerable<Farm>> Get(Expression<Func<Farm, bool>> expression) =>
            await _farmContext
                .Set<Farm>()
                .Where(expression)
                .ToListAsync()
                .ConfigureAwait(false);
    }
}