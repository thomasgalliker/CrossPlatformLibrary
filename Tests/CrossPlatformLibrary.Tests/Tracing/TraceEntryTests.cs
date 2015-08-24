using System;

using CrossPlatformLibrary.Tracing;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tracing
{
    [Collection("Tracing")]
    public class TraceEntryTests
    {
        [Fact]
        public void NullOrEmptyMessageThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TraceEntry(Category.Debug, null));
            Assert.Throws<ArgumentException>(() => new TraceEntry(Category.Debug, string.Empty));

            Assert.Throws<ArgumentNullException>(() => new TraceEntry(Category.Information, null));
            Assert.Throws<ArgumentException>(() => new TraceEntry(Category.Information, string.Empty));

            Assert.Throws<ArgumentNullException>(() => new TraceEntry(Category.Warning, null));
            Assert.Throws<ArgumentException>(() => new TraceEntry(Category.Warning, string.Empty));

            Assert.Throws<ArgumentNullException>(() => new TraceEntry(Category.Error, null));
            Assert.Throws<ArgumentException>(() => new TraceEntry(Category.Error, string.Empty));
        }

        [Fact]
        public void InvalidMessageForFormattingThrows()
        {
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Debug, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Information, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Warning, "message{-1}", "arg0"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Error, "message{-1}", "arg0"));
        }

        [Fact]
        public void LessArgumentsSuppliedAsInMessageDefiniedThrows()
        {
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => new TraceEntry(Category.Debug, "message{0}"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Debug, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Debug, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => new TraceEntry(Category.Information, "message{0}"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Information, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Information, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => new TraceEntry(Category.Warning, "message{0}"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Warning, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Warning, "message{1}", "arg1"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<FormatException>(() => new TraceEntry(Category.Error, "message{0}"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Error, "message{0}{1}", "arg1"));
            Assert.Throws<FormatException>(() => new TraceEntry(Category.Error, "message{1}", "arg1"));
        }

        [Fact]
        public void MoreArgumentsSuppliedAsInMessageDefinedThrows()
        {
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Debug, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Debug, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Debug, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Debug, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Information, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Information, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Information, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Information, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Warning, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Warning, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Warning, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Warning, "message{1}", "arg1", "arg2"));

            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Error, "message", "arg1"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Error, "message", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Error, "message{0}", "arg1", "arg2"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Error, "message{1}", "arg1", "arg2"));
        }

        [Fact]
        public void NullExceptionNotThrows()
        {
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Debug, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Information, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Warning, null, "message"));
            // TODO GATH: Remove? // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => new TraceEntry(Category.Error, null, "message"));
        }

        [Fact]
        public void PropertiesAreInitializedCorrectly()
        {
            Exception exception = new Exception();

            TraceEntry entry = new TraceEntry(Category.Debug, "message");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Debug, "{0}", "arg1");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Debug, exception, "message");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Debug, exception, "{0}", "arg1");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Information, "message");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Information, "{0}", "arg1");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Information, exception, "message");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Information, exception, "{0}", "arg1");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Warning, "message");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Warning, "{0}", "arg1");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Warning, exception, "message");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Warning, exception, "{0}", "arg1");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Error, "message");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Error, "{0}", "arg1");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Error, exception, "message");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Error, exception, "{0}", "arg1");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);
        }
    }
}