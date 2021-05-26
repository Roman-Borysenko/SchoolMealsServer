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
        private Expression<Func<Dish, Dish>> SelectDish(params ImageSize[] imageSize)
        {
            return d => new Dish
            {
                Id = d.Id,
                Name = d.Name,
                Slug = d.Slug,
                Description = d.Description,
                IsRecommend = d.IsRecommend,
                Image = d.Image.ImageUrl(SectionSite.Dishes, imageSize),
                LanguageId = d.LanguageId,
                CategoryId = d.CategoryId,
                Category = new Category
                {
                    Id = d.Category.Id,
                    Name = d.Category.Name,
                    Slug = d.Category.Slug,
                    CategoryId = d.Category.CategoryId,
                    LanguageId = d.Category.LanguageId,
                    ParentCategory = d.Category.ParentCategory != null ? new Category
                    {
                        Id = d.Category.ParentCategory.Id,
                        Name = d.Category.ParentCategory.Name,
                        Slug = d.Category.ParentCategory.Slug,
                        CategoryId = d.Category.ParentCategory.CategoryId,
                        LanguageId = d.Category.ParentCategory.LanguageId
                    } : null
                },
                CreateAt = d.CreateAt,
                UpdateAt = d.UpdateAt,

                DishUrlSections = d.DishUrl(),
                Ingredients = d.DishIngredients.Select(di => new Ingredient
                {
                    Id = di.Ingredient.Id,
                    Name = di.Ingredient.Name,
                    Slug = di.Ingredient.Slug
                }).ToList(),
                Tags = d.DishTags.Select(dt => new Tag
                {
                    Id = dt.Tag.Id,
                    Name = dt.Tag.Name,
                    Slug = dt.Tag.Slug
                }).ToList(),
                DiseaseDishes = d.DiseaseDishes.Select(dd => new DiseaseDish { 
                    Id = dd.Id,
                    DishId = dd.DishId,
                    DiseaseId = dd.DiseaseId
                }).ToList()
            };
        }

        public DishRepository(ILogger<BaseRepository<Dish>> logger, DataBaseContext context) : base(logger, context)
        {
        
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType)
        {
            IEnumerable<Dish> dishes = null;

            try
            {
                dishes = await _dbSet.Include(d => d.Category).ThenInclude(c => c.ParentCategory)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Include(d => d.DiseaseDishes)
                    .Where(predicate)
                    .Select(SelectDish(ImageSize.Min))
                    .Order(order, orderType).ToListAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync<TKey>(Expression<Func<Dish, bool>> predicate, Expression<Func<Dish, TKey>> order, OrderType orderType, int skip, int take)
        {
            IEnumerable<Dish> dishes = null;

            try
            {
                dishes = await _dbSet.Include(d => d.Category).ThenInclude(c => c.ParentCategory)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Include(d => d.DiseaseDishes)
                    .Where(predicate)
                    .Select(SelectDish(ImageSize.Min))
                    .Order(order, orderType).Skip(skip).Take(take).ToListAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate)
        {
            IEnumerable<Dish> dishes = null;

            try
            {
                dishes = await _dbSet.Include(d => d.Category).ThenInclude(c => c.ParentCategory)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Include(d => d.DiseaseDishes)
                    .Where(predicate)
                    .Select(SelectDish(ImageSize.Min))
                    .ToListAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync(Expression<Func<Dish, bool>> predicate, int skip, int take)
        {
            IEnumerable<Dish> dishes = null;

            try
            {
                dishes = await _dbSet.Include(d => d.Category).ThenInclude(c => c.ParentCategory)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Include(d => d.DiseaseDishes)
                    .Where(predicate)
                    .Select(SelectDish(ImageSize.Min))
                    .Skip(skip).Take(take).ToListAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return dishes;
        }

        public async Task<Dish> GetDishAsync(Expression<Func<Dish, bool>> predicate)
        {
            Dish dish = null;

            try
            {
                dish = await _dbSet.Include(d => d.Category).ThenInclude(c => c.ParentCategory)
                    .Include(d => d.Language)
                    .Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                    .Include(d => d.DishTags).ThenInclude(dt => dt.Tag)
                    .Include(d => d.DiseaseDishes)
                    .Where(predicate).Select(SelectDish(ImageSize.Midd, ImageSize.Max)).SingleOrDefaultAsync();
            } catch(Exception ex)
            {
                _logger.LogError(ex.ShowError());
            }

            return dish;
        }
    }
}
