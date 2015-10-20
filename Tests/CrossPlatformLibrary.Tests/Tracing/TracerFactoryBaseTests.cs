using System;
using System.Diagnostics;
using System.Globalization;

using CrossPlatformLibrary.Tracing;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tracing
{
    [Collection("Tracing")]
    public partial class TracerFactoryBaseTests
    {
        [Fact]
        public void CreateThrowsIfTypeIsNull()
        {
            TracerFactoryMock factory = new TracerFactoryMock();
            Assert.Throws<ArgumentNullException>(() => factory.Create((Type)null));
        }

        [Fact]
        public void CreatePassesTypeFullName()
        {
            TracerFactoryMock factory = new TracerFactoryMock();
            string passedName = null;
            factory.CreateTracerAction = name =>
                {
                    passedName = name;
                    return new TracerMock();
                };
            ITracer tracer = factory.Create(typeof(ITracer));

            Assert.Equal(NameAndVersion(typeof(ITracer)), passedName);
        }

        [Fact]
        public void GenericCreatePassesTypeFullName()
        {
            TracerFactoryMock factory = new TracerFactoryMock();
            string passedName = null;
            factory.CreateTracerAction = name =>
                {
                    passedName = name;
                    return new TracerMock();
                };
            ITracer tracer = factory.Create<ITracer>();

            Assert.Equal(NameAndVersion(typeof(ITracer)), passedName);
        }

        public class TracerFactoryMock : TracerFactoryBase
        {
            public TracerFactoryMock()
            {
                this.CreateTracerAction = name => new TracerMock();
            }

            public Func<string, ITracer> CreateTracerAction { get; set; }

            public override ITracer Create(string name)
            {
                return this.CreateTracerAction(name);
            }
        }

        private static string NameAndVersion(Type type)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", type.FullName, FileVersionInfo.GetVersionInfo(type.Assembly.Location).FileVersion);
        }

        public class TracerMock : ITracer
        {
            public void Write(Category category, string message, params object[] arguments)
            {
            }

            public void Write(Category category, Exception exception, string message, params object[] arguments)
            {
            }

            public void Write(TraceEntry entry)
            {
            }

            public bool IsCategoryEnabled(Category category)
            {
                return true;
            }

            public void Exception(Exception exception, string p)
            {
                throw new NotImplementedException();
            }
        }
    }
}