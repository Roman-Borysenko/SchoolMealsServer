using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolMeals.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DataBaseContext _context;
        protected DbSet<TEntity> _dbSet;
        protected ILogger _logger;
        public BaseRepository(ILogger<BaseRepository<TEntity>> logger, DataBaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

            _logger = logger;
        }
        public TEntity Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] properties)
        {
            return await _dbSet.MultiInclude(properties).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties)
        {
            IEnumerable<TEntity> data = null;

            try
            {
                data = await _dbSet.MultiInclude(properties).Order(order, orderType).AsQueryable().AsNoTracking().ToListAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return data;
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, params Expression<Func<TEntity, object>>[] properties)
        {
            IEnumerable<TEntity> data = null;

            try
            {
                data = await _dbSet.MultiInclude(properties).Where(predicate).Order(order, orderType).AsNoTracking().ToListAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return data;
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            IEnumerable<TEntity> data = null;

            try
            {
                data = await _dbSet.MultiInclude(properties).Where(predicate).AsNoTracking().ToListAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return data;
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, OrderType orderType, int skip, int take, params Expression<Func<TEntity, object>>[] properties)
        {
            IEnumerable<TEntity> data = null;

            try
            {
                data = await _dbSet.MultiInclude(properties).Where(predicate).Order(order, orderType).Skip(skip).Take(take).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return data;
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, params Expression<Func<TEntity, object>>[] properties)
        {
            IEnumerable<TEntity> data = null;

            try
            {
                data = await _dbSet.MultiInclude(properties).Where(predicate).Skip(skip).Take(take).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return data;
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
