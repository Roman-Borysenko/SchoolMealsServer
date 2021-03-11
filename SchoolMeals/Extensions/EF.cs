using Microsoft.EntityFrameworkCore;
using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolMeals.Extensions
{
    public static class EF
    {
        public static IQueryable<TEntity> MultiInclude<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            return properties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public static IOrderedQueryable<TEntity> Order<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> order, OrderType orderType) where TEntity : class
        {
            if(orderType == OrderType.Desc)
            {
                return query.OrderByDescending(order);
            }

            return query.OrderBy(order);
        }
        //public static IOrderedEnumerable<TEntity> Order<TEntity, TKey>(this IEnumerable<TEntity> query, Func<TEntity, TKey> order, OrderType orderType) where TEntity : class
        //{
        //    if (orderType == OrderType.Desc)
        //    {
        //        return query.OrderByDescending(order);
        //    }

        //    return query.OrderBy(order);
        //}
    }
}
