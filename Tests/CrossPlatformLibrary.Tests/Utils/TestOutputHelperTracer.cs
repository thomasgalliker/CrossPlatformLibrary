using System;
using CrossPlatformLibrary.Internals;
using Xunit.Abstractions;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Tests.Utils
{
    public class TestOutputHelperTracer : ITracer
    {
        private readonly ITestOutputHelper testOutputHelper;


        public TestOutputHelperTracer(ITestOutputHelper testOutputHelper, object target)
          : this(testOutputHelper, target.GetType())
        {
        }

        public TestOutputHelperTracer(ITestOutputHelper testOutputHelper, Type targetType)
            : this(testOutputHelper, targetType.GetFormattedName())
        {
        }

        public TestOutputHelperTracer(ITestOutputHelper testOutputHelper, string targetName)
        {
            this.testOutputHelper = testOutputHelper;
            this.Name = targetName;
        }

        public string Name { get; }

        public void Write(Category category, string message)
        {
            this.Write(category, null, message);
        }

        public void Write(Category category, Exception exception, string message)
        {
            try
            {
                this.testOutputHelper.WriteLine(exception == null
                    ? $"{DateTime.UtcNow} - {category} - {this.Name} - {message} [EOL]"
                    : $"{DateTime.UtcNow} - {category} - {this.Name} - {message} - Exception: {exception} [EOL]");
            }
            catch (InvalidOperationException)
            {
                // TestOutputHelperTracer throws an InvalidOperationException
                // if it is no longer associated with a test case.
            }
        }
    }
}
