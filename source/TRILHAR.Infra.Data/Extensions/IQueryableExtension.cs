using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace TRILHAR.Infra.Data.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> AddIncludes<TEntity, TProperty>(this IQueryable<TEntity> source, List<Expression<Func<TEntity, TProperty>>> expressionIncludes) where TEntity : class
        {
            if (expressionIncludes != null)
            {
                foreach (var include in expressionIncludes)
                {
                    source = source.Include(include);
                }
            }
            return source;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}