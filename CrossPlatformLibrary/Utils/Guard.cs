using System;
using System.Linq.Expressions;

namespace CrossPlatformLibrary.Utils
{
    public static class Guard
    {
        /// <summary>
        ///     Only pass single parameters through to this call via the expression e.g. Guard.ArgumentNotNull(() => param);
        /// </summary>
        /// <typeparam name="T">The type of the parameter, inferred from the Expression</typeparam>
        /// <param name="expression">An expression containing a single parameter e.g. () => param</param>
        public static void ArgumentNotNull<T>(Expression<Func<T>> expression) where T : class
        {
            ArgumentNotNull(expression, "expression");

            // As seen here: http://jonfuller.codingtomusic.com/2008/12/11/static-reflection-method-guards/
            if (expression.Compile()().Equals(default(T)))
            {
                throw new ArgumentNullException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        public static void ArgumentNotNull<T>(T value, string name)
        {
            if (Equals(value, null))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NotNull<T>(T value, string message)
        {
            if (Equals(value, null))
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        ///     Only pass single parameters through to this call via the expression, e.g. Guard.ArgumentNotNull(() => stringParam)
        /// </summary>
        /// <param name="expression">An expression containing a single string parameter e.g. () => stringParam</param>
        public static void ArgumentNotNullOrEmpty(Expression<Func<string>> expression)
        {
            if (string.IsNullOrEmpty(expression.Compile()()))
            {
                throw new ArgumentNullException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        public static void ArgumentNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void True(Func<bool> func, Action action)
        {
            if (!func())
            {
                action();
            }
        }
    }
}