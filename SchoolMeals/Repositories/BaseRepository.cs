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
        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            int count = 0;

            try
            {
                count = await _dbSet.MultiInclude(properties).Where(predicate).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return count;
        }
        public async Task<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            TEntity entity = null;

            try
            {
                entity = await _dbSet.MultiInclude(properties).SingleOrDefaultAsync(predicate);
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return entity;
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

        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> CreateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
                return null;
            }
        }

        public async Task Remove(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                _dbSet.RemoveRange(await _dbSet.Where(predicate).ToArrayAsync());
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }
        }
    }
}
