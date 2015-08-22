using System;
using System.Diagnostics.CodeAnalysis;

using CrossPlatformLibrary.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossPlatformLibrary.Tests.UnitTesting
{
    public static class ExceptionAssert
    {
        /// <summary>
        ///     Invokes the specified delegate <paramref name="method" /> and checks if the specified type of
        ///     <see cref="Exception" /> is thrown.  If the specified type of <typeparamref name="TException" />
        ///     (or any other <see cref="Exception" /> type inheriting from it) is not thrown or any other <see cref="Exception" />
        ///     is thrown, an appropriate <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <typeparam name="TException">The type of the expected exception.</typeparam>
        /// <param name="method">The delegate that is called by the assertion and that should throw the exception.</param>
        /// <returns>The instance of the exception that was thrown.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="method" /> parameter is <c>null</c>.</exception>
        /// <exception cref="AssertFailedException">
        ///     The <typeparamref name="TException" /> or any inherited class of it has not been thrown by the specified
        ///     <paramref name="method" />.
        ///     - or - Any other not expected <see cref="Exception" /> has been thrown by the specified <paramref name="method" />.
        ///     - or - No <see cref="Exception" /> has been thrown by the specified <paramref name="method" />.
        /// </exception>
        /// <example>
        ///     The following code snippet example demonstrates the usage of this assertion.
        ///     <code>ExceptionAssert.IsThrown{Exception}(() => obj.MethodThatThrowsException());</code>.
        /// </example>
        public static TException IsThrown<TException>(Action method) where TException : Exception
        {
            return (TException)IsThrown(typeof(TException), method);
        }

        /// <summary>
        ///     Invokes the specified delegate <paramref name="method" /> and checks if the specified type of
        ///     <see cref="Exception" /> is thrown.  If the specified type of <paramref name="expectedException" />
        ///     (or any other <see cref="Exception" /> type inheriting from it) is not thrown or any other <see cref="Exception" />
        ///     is thrown, an appropriate <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expectedException">The type of the expected exception.</param>
        /// <param name="method">The delegate that is called by the assertion and that should throw the exception.</param>
        /// <returns>The instance of the exception that was thrown.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="expectedException" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="method" /> parameter is <c>null</c>.</exception>
        /// <exception cref="AssertFailedException">
        ///     The <paramref name="expectedException" /> or any inherited class of it has not been thrown by the specified
        ///     <paramref name="method" />.
        ///     - or - Any other not expected <see cref="Exception" /> has been thrown by the specified <paramref name="method" />.
        ///     - or - No <see cref="Exception" /> has been thrown by the specified <paramref name="method" />.
        /// </exception>
        /// <example>
        ///     The following code snippet example demonstrates the usage of this assertion.
        ///     <code>ExceptionAssert.IsThrown(typeof(Exception), () => obj.MethodThatThrowsException());</code>.
        /// </example>
        public static Exception IsThrown(Type expectedException, Action method)
        {
            Guard.ArgumentNotNull(expectedException, "expectedException");
            Guard.ArgumentNotNull(method, "method");

            Exception thrownException = null;

            try
            {
                // Invoke the specified delegate
                method();
            }
            catch (Exception ex)
            {
                // Set return value
                thrownException = ex;

                // Check if exception is instance of the specified expected exception type
                if (!expectedException.IsInstanceOfType(thrownException))
                {
                    // Fail test
                    throw AssertHelper.BuildException(ExceptionMessages.ExceptionAssertWrongException, ex.GetType(), expectedException);
                }
            }

            if (thrownException == null)
            {
                // Fail test
                throw AssertHelper.BuildException(ExceptionMessages.ExceptionAssertExceptionNotThrown, expectedException);
            }

            return thrownException;
        }

        /// <summary>
        ///     Invokes the specified delegate <paramref name="method" /> and checks if the specified type of
        ///     <see cref="Exception" /> is not thrown. If the specified type of <typeparamref name="TException" />
        ///     (or any other <see cref="Exception" /> type inheriting from it) is thrown, an appropriate
        ///     <see cref="AssertFailedException" /> is thrown.
        ///     If another type of <see cref="Exception" /> than the specified is thrown, the exception would be rethrown.
        /// </summary>
        /// <typeparam name="TException">The type of the expected exception.</typeparam>
        /// <param name="method">The delegate that is called by the assertion and that should not throw the exception.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="method" /> parameter is <c>null</c>.</exception>
        /// <exception cref="AssertFailedException">
        ///     The <typeparamref name="TException" /> or any inherited class of it is thrown
        ///     by the specified <paramref name="method" />.
        /// </exception>
        /// <example>
        ///     The following code snippet example demonstrates the usage of this assertion.
        ///     <code>ExceptionAssert.IsNotThrown{Exception}(() => obj.MethodThatNotThrowsTheExpectedException());</code>.
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type parameter is used to determine the type the actual call is for.")]
        public static void IsNotThrown<TException>(Action method) where TException : Exception
        {
            IsNotThrown(typeof(TException), method);
        }

        /// <summary>
        ///     Invokes the specified delegate <paramref name="method" /> and checks if the specified type of
        ///     <see cref="Exception" /> is not thrown. If the specified type of <paramref name="expectedException" />
        ///     (or any other <see cref="Exception" /> type inheriting from it) is thrown, an appropriate
        ///     <see cref="AssertFailedException" /> is thrown.
        ///     If another type of <see cref="Exception" /> than the specified is thrown, the exception would be rethrown.
        /// </summary>
        /// <param name="expectedException">The type of the expected exception.</param>
        /// <param name="method">The delegate that is called by the assertion and that should not throw the exception.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="expectedException" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="method" /> parameter is <c>null</c>.</exception>
        /// <exception cref="AssertFailedException">
        ///     The <paramref name="expectedException" /> or any inherited class of it is
        ///     thrown by the specified <paramref name="method" />.
        /// </exception>
        /// <example>
        ///     The following code snippet example demonstrates the usage of this assertion.
        ///     <code>ExceptionAssert.IsNotThrown(typeof(Exception), () => obj.MethodThatNotThrowsTheExpectedException());</code>.
        /// </example>
        public static void IsNotThrown(Type expectedException, Action method)
        {
            Guard.ArgumentNotNull(expectedException, "expectedException");
            Guard.ArgumentNotNull(method, "method");

            try
            {
                // Invoke the specified delegate
                method();
            }
            catch (Exception ex)
            {
                // Check if exception is instance of the spcified expected exception type
                if (expectedException.IsInstanceOfType(ex))
                {
                    // Fail test
                    throw AssertHelper.BuildException(ExceptionMessages.ExceptionAssertExceptionThrown, ex.GetType());
                }
                else
                {
                    // Rethrow caught exception
                    throw;
                }
            }
        }
    }
}