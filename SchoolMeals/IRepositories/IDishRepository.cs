using SchoolMeals.Enums;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolMeals.IRepositories
{
    public interface IDishRepository : IBaseRepository<Dish>
    {
        Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType);
        Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType, int skip, int take);
        Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate);
        Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate, int skip, int take);
    }
}
