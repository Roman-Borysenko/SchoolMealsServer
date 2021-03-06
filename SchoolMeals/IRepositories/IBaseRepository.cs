using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolMeals.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<int> Count(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
        Task<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, int skip, int take, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, params Expression<Func<TEntity, object>>[] properties);
        Task<TEntity> Create(TEntity entity);
        Task<IEnumerable<TEntity>> CreateRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task Remove(Expression<Func<TEntity, bool>> predicate);
    }
}
