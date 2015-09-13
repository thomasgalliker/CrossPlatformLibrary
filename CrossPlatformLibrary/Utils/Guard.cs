using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace CrossPlatformLibrary.Utils
{
    [DebuggerStepThrough] 
    public static class Guard
    {
        /// <summary>
        ///     Only pass single parameters through to this call via the expression e.g. Guard.ArgumentNotNull(() => param);
        /// </summary>
        /// <typeparam name="T">The type of the parameter, inferred from the Expression</typeparam>
        /// <param name="expression">An expression containing a single parameter e.g. () => param</param>
        public static void ArgumentNotNull<T>(Expression<Func<T>> expression)
        {
            ArgumentNotNull(expression, "expression");

            // As seen here: http://jonfuller.codingtomusic.com/2008/12/11/static-reflection-method-guards/
            //var areEqual = EqualityComparer<T>.Default.Equals(expression.Compile()(), default(T));
            var propertyValue = expression.Compile()();
            if (propertyValue == null)
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

        public static void ArgumentIsTrue(Expression<Func<bool>> expression)
        {
            Guard.ArgumentNotNull(() => expression);

            if (expression.Compile().Invoke() == false)
            {
                throw new ArgumentException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        /// <summary>
        ///     Only pass single parameters through to this call via the expression, e.g. Guard.ArgumentNotNull(() => stringParam)
        /// </summary>
        /// <param name="expression">An expression containing a single string parameter e.g. () => stringParam</param>
        public static void ArgumentNotNullOrEmpty(Expression<Func<string>> expression)
        {
            var compiledExpression = expression.Compile()();
            var memberName = ((MemberExpression)expression.Body).Member.Name;

            ArgumentNotNullOrEmpty(compiledExpression, memberName);
        }

        public static void ArgumentNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (value == null)
                {
                    throw new ArgumentNullException(name);
                }
                throw new ArgumentException(name);
            }
        }

        public static void True(Func<bool> func, Action action)
        {
            if (!func())
            {
                action();
            }
        }

        public static void ArgumentMustNotExceed(Expression<Func<string>> expression, int maxLength = int.MaxValue)
        {
            var stringValue = expression.Compile()();
            int length = stringValue.Length;
            if (length > maxLength)
            {
                var memberName = ((MemberExpression)expression.Body).Member.Name;
                throw new ArgumentException("Length must not exceed " + maxLength + " number of characters", memberName);
            }
        }

        public static void ArgumentMustBeInterface(Type classType)
        {
            CheckIfTypeIsInterface(classType, false, "Type must be an interface.");
        }

        public static void ArgumentMustNotBeInterface(Type classType)
        {
            CheckIfTypeIsInterface(classType, true, "Type must not be an interface.");
        }

        private static void CheckIfTypeIsInterface(Type classType, bool throwIfItIsAnInterface, string exceptionMessage)
        {
#if NETFX_CORE
            if (classType.GetTypeInfo().IsInterface == throwIfItIsAnInterface)
#else
            if (classType.IsInterface == throwIfItIsAnInterface)
#endif
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}