using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace CrossPlatformLibrary.Utils
{
    public static class Guard
    {
        /// <summary>
        ///     Only pass single parameters through to this call via the expression e.g. Guard.ArgumentNotNull(() => param);
        /// </summary>
        /// <typeparam name="T">The type of the parameter, inferred from the Expression</typeparam>
        /// <param name="expression">An expression containing a single parameter e.g. () => param</param>
        [DebuggerStepThrough] 
        public static void ArgumentNotNull<T>(Expression<Func<T>> expression) where T : class
        {
            ArgumentNotNull(expression, "expression");

            // As seen here: http://jonfuller.codingtomusic.com/2008/12/11/static-reflection-method-guards/
            if (expression.Compile()().Equals(default(T)))
            {
                throw new ArgumentNullException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        [DebuggerStepThrough] 
        public static void ArgumentNotNull<T>(T value, string name)
        {
            if (Equals(value, null))
            {
                throw new ArgumentNullException(name);
            }
        }

        [DebuggerStepThrough] 
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
        [DebuggerStepThrough] 
        public static void ArgumentNotNullOrEmpty(Expression<Func<string>> expression)
        {
            if (string.IsNullOrEmpty(expression.Compile()()))
            {
                throw new ArgumentNullException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        [DebuggerStepThrough] 
        public static void ArgumentNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        [DebuggerStepThrough] 
        public static void True(Func<bool> func, Action action)
        {
            if (!func())
            {
                action();
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