using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using CrossPlatformLibrary.Collection.Generic;

namespace CrossPlatformLibrary.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression ToLower(this Expression expression)
        {
            var methodInfo = typeof(string).GetRuntimeMethod("ToLower", new Type[] { });
            return Expression.Call(expression, methodInfo);
        }

        public static Expression Contains(this Expression expression, Expression containsExpression)
        {
            var methodInfo = typeof(string).GetRuntimeMethod("Contains", new[] { typeof(string) });
            return Expression.Call(expression, methodInfo, containsExpression);
        }

        public static IEnumerable<T> PerformOrdering<T>(IEnumerable<T> enumerable, IEnumerable<OrderSpecification<T>> orderSpecifications)
        {
            lock (orderSpecifications)
            {
                IQueryable<T> query = enumerable.AsQueryable();

                OrderSpecification<T> firstSpecification = orderSpecifications.First();
                IOrderedEnumerable<T> orderedQuery;
                if (firstSpecification.OrderDirection == OrderDirection.Ascending)
                {
                    orderedQuery = query.OrderBy(firstSpecification.KeySelector);
                }
                else
                {
                    orderedQuery = query.OrderByDescending(firstSpecification.KeySelector);
                }

                foreach (var orderSpecification in orderSpecifications.Skip(1))
                {
                    if (orderSpecification.OrderDirection == OrderDirection.Ascending)
                    {
                        orderedQuery = orderedQuery.ThenBy(orderSpecification.KeySelector);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(orderSpecification.KeySelector);
                    }
                }

                return orderedQuery.ToList();
            }
        }
    }
}