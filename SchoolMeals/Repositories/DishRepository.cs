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
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        private Expression<Func<Dish, Dish>> select = d => new Dish
        {
            Id = d.Id,
            Name = d.Name,
            Slug = d.Slug,
            Description = d.Description,
            IsRecommend = d.IsRecommend,
            Image = d.Image.ImageUrl(SectionSite.Dishes, ImageSize.Min),
            LanguageId = d.LanguageId,
            CategoryId = d.CategoryId,
            CreateAt = d.CreateAt,
            UpdateAt = d.UpdateAt,
            Ingredients = d.DishIngredients.Select(di => di.Ingredient).ToList(),
            Tags = d.DishTags.Select(dt => dt.Tag).ToList()
        };

        public DishRepository(ILogger<BaseRepository<Dish>> logger, DataBaseContext context) : base(logger, context)
        {
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType)
        {
            return await _dbSet.Include(d => d.Category)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Where(predicate)
                    .Select(select)
                    .Order(order, orderType).ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType, int skip, int take)
        {
            return await _dbSet.Include(d => d.Category)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Where(predicate)
                    .Select(select)
                    .Order(order, orderType).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate)
        {
            return await _dbSet.Include(d => d.Category)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Where(predicate)
                    .Select(select)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate, int skip, int take)
        {
            return await _dbSet.Include(d => d.Category)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Where(predicate)
                    .Select(select)
                    .Skip(skip).Take(take).ToListAsync();
        }
    }
}
