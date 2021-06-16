using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChickenFarm.Shared
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
    }
}