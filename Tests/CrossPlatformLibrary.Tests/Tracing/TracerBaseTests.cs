using System;

using CrossPlatformLibrary.Tracing;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tracing
{
    [Collection("Tracing")]
    public class TracerBaseTests
    {
        [Fact]
        public void WriteThrowsIfMessageIsNullOrEmpty()
        {
            TracerMock tracer = new TracerMock();

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
        public void WriteThrowsIfMessageIsInvalidForFormatting()
        {
            TracerMock tracer = new TracerMock();

            Assert.Throws<FormatException>(() => tracer.Write(Category.Debug, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Information, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Warning, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Error, "message{-1}", "arg0"));
        }

        [Fact]
        public void WriteThrowsIfLessArgumentsSuppliedAsInMessageDefined()
        {
            TracerMock tracer = new TracerMock();

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => tracer.Write(Category.Debug, "message{0}"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Debug, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Debug, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => tracer.Write(Category.Information, "message{0}"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Information, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Information, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => tracer.Write(Category.Warning, "message{0}"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Warning, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Warning, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => tracer.Write(Category.Error, "message{0}"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Error, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => tracer.Write(Category.Error, "message{1}", "arg1"));
        }

        [Fact]
        public void WriteNotThrowsIfMoreArgumentsSuppliedAsInMessageDefined()
        {
            TracerMock tracer = new TracerMock();

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, "message{1}", "arg1", "arg2"));
        }

        [Fact]
        public void WriteNotThrowsIfExceptionIsNull()
        {
            TracerMock tracer = new TracerMock();

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Debug, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Information, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Warning, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => tracer.Write(Category.Error, null, "message"));
        }

        [Fact]
        public void WriteCreatesCorrectLogEntry()
        {
            TraceEntry passedEntry = null;
            Exception exception = new Exception();

            TracerMock tracer = new TracerMock();
            tracer.WriteAction = entry => passedEntry = entry;

            tracer.Write(Category.Debug, "message");
            Assert.Equal(Category.Debug, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);

            tracer.Write(Category.Debug, "{0}", "arg1");
            Assert.Equal(Category.Debug, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);

            tracer.Write(Category.Debug, exception, "message");
            Assert.Equal(Category.Debug, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Debug, exception, "{0}", "arg1");
            Assert.Equal(Category.Debug, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Information, "message");
            Assert.Equal(Category.Information, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);

            tracer.Write(Category.Information, "{0}", "arg1");
            Assert.Equal(Category.Information, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);

            tracer.Write(Category.Information, exception, "message");
            Assert.Equal(Category.Information, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Information, exception, "{0}", "arg1");
            Assert.Equal(Category.Information, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Warning, "message");
            Assert.Equal(Category.Warning, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);

            tracer.Write(Category.Warning, "{0}", "arg1");
            Assert.Equal(Category.Warning, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);

            tracer.Write(Category.Warning, exception, "message");
            Assert.Equal(Category.Warning, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Warning, exception, "{0}", "arg1");
            Assert.Equal(Category.Warning, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Error, "message");
            Assert.Equal(Category.Error, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);

            tracer.Write(Category.Error, "{0}", "arg1");
            Assert.Equal(Category.Error, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);

            tracer.Write(Category.Error, exception, "message");
            Assert.Equal(Category.Error, passedEntry.Category);
            Assert.Equal("message", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);

            tracer.Write(Category.Error, exception, "{0}", "arg1");
            Assert.Equal(Category.Error, passedEntry.Category);
            Assert.Equal("arg1", passedEntry.Message);
            Assert.Same(exception, passedEntry.Exception);
        }

        public class TracerMock : TracerBase
        {
            public TracerMock()
            {
                this.WriteAction = entry => { };
                this.IsCategoryEnabledAction = category => true;
            }

            public Action<TraceEntry> WriteAction { get; set; }

            public Func<Category, bool> IsCategoryEnabledAction { get; set; }

            protected override void WriteCore(TraceEntry entry)
            {
                this.WriteAction(entry);
            }

            public override bool IsCategoryEnabled(Category category)
            {
                return this.IsCategoryEnabledAction(category);
            }
        }
    }
}