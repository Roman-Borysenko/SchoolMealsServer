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
        TEntity Create(TEntity entity);
        Task<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, int skip, int take, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, params Expression<Func<TEntity, object>>[] properties);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(int id);
    }
}
