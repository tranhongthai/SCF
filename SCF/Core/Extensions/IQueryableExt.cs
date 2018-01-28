using System.Collections.Generic;
using System.Linq.Expressions;
using Peyton.Core.Repository;

// ReSharper disable once CheckNamespace

namespace System.Linq
{
// ReSharper disable once InconsistentNaming
    public static class IQueryableExt
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, bool value)
        {
            if (value)
                return query.Where(expression);
            return query;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, bool? value)
        {
            return query.Where(expression, value.HasValue);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, long? value)
        {
            return query.Where(expression, value.HasValue && value != 0);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, long value)
        {
            return query.Where(expression, value != 0);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression,
            string value)
        {
            return query.Where(expression, !string.IsNullOrWhiteSpace(value));
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, Guid value)
        {
            return query.Where(expression, value != Guid.Empty);
        }

        public static List<M> ToModel<M, T>(this IQueryable<T> query)
        {
            var result = query.ToList();
            if (typeof (T).GetInterfaces().Any(i => i.Name == "IOrder"))
                return
                    result.OrderBy(i => (i as ISequence).Order)
                        .Select(i => (M) Activator.CreateInstance(typeof (M), i))
                        .ToList();

            return result.Select(i => (M) Activator.CreateInstance(typeof (M), i)).ToList();
        }

        public static List<T> ToListExt<T>(this IQueryable<T> query) where T : ISequence
        {
            return query.OrderBy(i => i.Order).ToList();
        }
    }
}