using System;

using CrossPlatformLibrary.Tracing;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tracing
{
    [Collection("Tracing")]
    public class EmptyTracerTests
    {
        [Fact]
        public void WriteThrowsIfMessageIsNullOrEmpty()
        {
            EmptyTracer tracer = new EmptyTracer();

            Assert.Throws<ArgumentNullException>(() => tracer.Write(Category.Debug, null));
            Assert.Throws<ArgumentException>(() => tracer.Write(Category.Debug, string.Empty));

            Assert.Throws<ArgumentNullException>(() => tracer.Write(Category.Information, null));
            Assert.Throws<ArgumentException>(() => tracer.Write(Category.Information, string.Empty));

            Assert.Throws<ArgumentNullException>(() => tracer.Write(Category.Warning, null));
            Assert.Throws<ArgumentException>(() => tracer.Write(Category.Warning, string.Empty));

            Assert.Throws<ArgumentNullException>(() => tracer.Write(Category.Error, null));
            Assert.Throws<ArgumentException>(() => tracer.Write(Category.Error, string.Empty));
        }

        [Fact]
        public void WriteNotThrowsIfExceptionIsNull()
        {
            EmptyTracer tracer = new EmptyTracer();

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, null, "message"));
        }

        [Fact]
        public void WriteThrowsIfEntryIsNull()
        {
            EmptyTracer tracer = new EmptyTracer();
            Assert.Throws<ArgumentNullException>(() => tracer.Write(null));
        }

        [Fact]
        public void IsCategoryEnabledAlwaysReturnsFalse()
        {
            EmptyTracer tracer = new EmptyTracer();
            bool isEnabled = tracer.IsCategoryEnabled(Category.Debug);
            Assert.False(isEnabled);

            isEnabled = tracer.IsCategoryEnabled(Category.Information);
            Assert.False(isEnabled);

            isEnabled = tracer.IsCategoryEnabled(Category.Warning);
            Assert.False(isEnabled);

            isEnabled = tracer.IsCategoryEnabled(Category.Error);
            Assert.False(isEnabled);
        }

        [Fact]
        public void WriteCoreNotThrowsIfEntryIsNull()
        {
            EmptyTracerMock tracer = new EmptyTracerMock();
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<ArgumentException>(() => tracer.WriteCoreMethod(null));
        }

        public class EmptyTracerMock : EmptyTracer
        {
            public void WriteCoreMethod(TraceEntry entry)
            {
                this.WriteCore(entry);
            }
        }
    }
}