using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CrossPlatformLibrary.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression ToLower(this Expression expression)
        {
            return Expression.Call(expression, typeof(string).GetRuntimeMethod("ToLower", new Type[] { }));
        }

        public static Expression Contains(this Expression expression, Expression containsExpression)
        {
            return Expression.Call(expression, typeof(string).GetRuntimeMethod("Contains", new Type[] { }), containsExpression);
        }
    }
}