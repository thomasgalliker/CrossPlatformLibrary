using System.ComponentModel;
using System.Globalization;

using CrossPlatformLibrary.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossPlatformLibrary.Tests.UnitTesting
{
    public static class AssertHelper
    {
        /// <summary>
        ///     Builds and returns a new <see cref="AssertFailedException" /> with the correctly formatted specified
        ///     <paramref name="message" />.
        /// </summary>
        /// <param name="message">The message to use for the <see cref="AssertFailedException" />.</param>
        /// <param name="args">The arguments to format the <paramref name="message" />.</param>
        /// <returns>A new instance of a <see cref="AssertFailedException" />.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="message" /> parameter is <c>null</c>.</exception>
        public static AssertFailedException BuildException([Localizable(false)] string message, params object[] args)
        {
            Guard.ArgumentNotNull(message, "message");

            // Format message if required
            if ((args != null) && (args.Length > 0))
            {
                message = string.Format(CultureInfo.InvariantCulture, message, args);
            }

            // Create and return new exception
            return new AssertFailedException(message);
        }
    }
}