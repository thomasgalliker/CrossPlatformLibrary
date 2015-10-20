﻿using System;
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
    }
}