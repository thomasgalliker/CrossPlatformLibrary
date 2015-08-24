

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    public partial class GuardTests
    {
        [Fact]
        public void ArgumentNotNullNotThrowsIfArgumentIsValueType()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNull(10, "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNull("string", "argument"));
        }

        [Fact]
        public void ArgumentNotNullOrEmptyNotThrowsIfArgumentNotNullOrEmpty()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNullOrEmpty("value", "argument"));
        }

        [Fact]
        public void ArgumentIsNotNegativeNotThrowsIfArgumentIsPositive()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(1, "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(int.MaxValue, "argument"));
        }

        [Fact]
        public void ArgumentIsNotNegativeNotThrowsIfArgumentIsZero()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(0, "argument"));
        }

        [Fact]
        public void ArgumentIsAssignableNotThrowsIfArgumentIsAsssignable()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedClass(), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new InheritedClass(), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedInheritedClass(), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInheritedInterface), new ImplementedInheritedClass(), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new InheritedClass(), "argument"));
        }

        [Fact]
        public void ArgumentTypeIsAssignableWithTypeNotThrowsIfTypeIsAsssignable()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedClass), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IInheritedInterface), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(InheritedClass), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedInheritedClass), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(ImplementedInheritedClass), "argument"));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(InheritedClass), "argument"));
        }

        ////[Fact]
        ////public void ArgumentIsInRangeThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.ArgumentIsInRange(true, string.Empty, "argument", null));
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsInRange(true, string.Empty, "argument", string.Empty));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeNotThrowsIfArgumentValueIsNull()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsInRange(true, (string)null, "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeNotThrowsIfArgumentNameIsNullOrEmpty()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsInRange(true, string.Empty, null, "message"));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsInRange(true, string.Empty, string.Empty, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsInRange(true, "value", "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeThrowsWithProvidedMessage()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "message"));
        ////    StringAssert.StartsWith(ex.Message, "message");
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeFormatMessageUsingParameters()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "{0}"));
        ////    StringAssert.StartsWith(ex.Message, "value");

        ////    ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "{1}"));
        ////    StringAssert.StartsWith(ex.Message, "argument");
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeWithoutParameterThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.ArgumentIsInRange(true, null));
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsInRange(true, string.Empty));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeWithoutParameterNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsInRange(true, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeWithoutParameterThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeWithoutParameterThrowsWithProvidedMessage()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "message"));
        ////    Assert.Equal(ex.Message, "message");
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.ArgumentIsMatching(true, string.Empty, "argument", null));
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(true, string.Empty, "argument", string.Empty));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingNotThrowsIfArgumentValueIsNull()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsMatching<string>(true, null, "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingNotThrowsIfArgumentNameIsNullOrEmpty()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsMatching(true, string.Empty, null, "message"));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsMatching(true, string.Empty, string.Empty, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsMatching(true, "value", "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "value", "argument", "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingThrowsWithProvidedMessage()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "value", "argument", "message"));
        ////    StringAssert.StartsWith(ex.Message, "message");
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingFormatMessageUsingParameters()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "value", "argument", "{0}"));
        ////    StringAssert.StartsWith(ex.Message, "value");

        ////    ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "value", "argument", "{1}"));
        ////    StringAssert.StartsWith(ex.Message, "argument");
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingWithoutParameterThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.ArgumentIsMatching(true, null));
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(true, string.Empty));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingWithoutParameterNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsMatching(true, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingWithoutParameterThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "message"));
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingWithoutParameterThrowsWithProvidedMessage()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "message"));
        ////    StringAssert.StartsWith(ex.Message, "message");
        ////}

        ////[Fact]
        ////public void OperationIsValidThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.OperationIsValid(true, null));
        ////    Assert.Throws<ArgumentException>(() => Guard.OperationIsValid(true, string.Empty));
        ////}

        ////[Fact]
        ////public void OperationIsValidNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.OperationIsValid(true, "message"));
        ////}

        ////[Fact]
        ////public void OperationIsValidThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<InvalidOperationException>(() => Guard.OperationIsValid(false, "message"));
        ////}

        ////[Fact]
        ////public void OperationIsValidThrowsWithProvidedMessage()
        ////{
        ////    InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsValid(false, "message"));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsValidFormatsProvidedMessageWithProvidedArguments()
        ////{
        ////    InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsValid(false, "{0};{1}", "argument1", "argument2"));
        ////    Assert.Equal("argument1;argument2", ex.Message);

        ////    ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsValid(false, "message", new object[0]));
        ////    Assert.Equal("message", ex.Message);

        ////    ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsValid(false, "message", null));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsNotValidThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.OperationIsNotValid(null));
        ////    Assert.Throws<ArgumentException>(() => Guard.OperationIsNotValid(string.Empty));
        ////}

        ////[Fact]
        ////public void OperationIsNotValidThrowsAllways()
        ////{
        ////    Assert.Throws<InvalidOperationException>(() => Guard.OperationIsNotValid("message"));
        ////}

        ////[Fact]
        ////public void OperationIsNotValidThrowsWithProvidedMessage()
        ////{
        ////    InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsNotValid("message"));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsNotValidFormatsProvidedMessageWithProvidedArguments()
        ////{
        ////    InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsNotValid("{0};{1}", "argument1", "argument2"));
        ////    Assert.Equal("argument1;argument2", ex.Message);

        ////    ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsNotValid("message", new object[0]));
        ////    Assert.Equal("message", ex.Message);

        ////    ex = Assert.Throws<InvalidOperationException>(() => Guard.OperationIsNotValid("message", null));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsSupportedThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.OperationIsSupported(true, null));
        ////    Assert.Throws<ArgumentException>(() => Guard.OperationIsSupported(true, string.Empty));
        ////}

        ////[Fact]
        ////public void OperationIsSupportedNotThrowsIfMatch()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.OperationIsSupported(true, "message"));
        ////}

        ////[Fact]
        ////public void OperationIsSupportedThrowsIfNotMatch()
        ////{
        ////    Assert.Throws<NotSupportedException>(() => Guard.OperationIsSupported(false, "message"));
        ////}

        ////[Fact]
        ////public void OperationIsSupportedThrowsWithProvidedMessage()
        ////{
        ////    NotSupportedException ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsSupported(false, "message"));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsSupportedFormatsProvidedMessageWithProvidedArguments()
        ////{
        ////    NotSupportedException ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsSupported(false, "{0};{1}", "argument1", "argument2"));
        ////    Assert.Equal("argument1;argument2", ex.Message);

        ////    ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsSupported(false, "message", new object[0]));
        ////    Assert.Equal("message", ex.Message);

        ////    ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsSupported(false, "message", null));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsNotSupportedThrowsIfMessageIsNullOrEmpty()
        ////{
        ////    Assert.Throws<ArgumentNullException>(() => Guard.OperationIsNotSupported(null));
        ////    Assert.Throws<ArgumentException>(() => Guard.OperationIsNotSupported(string.Empty));
        ////}

        ////[Fact]
        ////public void OperationIsNotSupportedThrowsAllways()
        ////{
        ////    Assert.Throws<NotSupportedException>(() => Guard.OperationIsNotSupported("message"));
        ////}

        ////[Fact]
        ////public void OperationIsNotSupportedThrowsWithProvidedMessage()
        ////{
        ////    NotSupportedException ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsNotSupported("message"));
        ////    Assert.Equal("message", ex.Message);
        ////}

        ////[Fact]
        ////public void OperationIsNotSupportedFormatsProvidedMessageWithProvidedArguments()
        ////{
        ////    NotSupportedException ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsNotSupported("{0};{1}", "argument1", "argument2"));
        ////    Assert.Equal("argument1;argument2", ex.Message);

        ////    ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsNotSupported("message", new object[0]));
        ////    Assert.Equal("message", ex.Message);

        ////    ex = Assert.Throws<NotSupportedException>(() => Guard.OperationIsNotSupported("message", null));
        ////    Assert.Equal("message", ex.Message);
        ////}

        public interface IInterface
        {
        }

        public interface IInheritedInterface : IInterface
        {
        }

        public interface IOtherInterface
        {
        }

        public class ImplementedClass : IInterface
        {
        }

        public class NotImplementedClass
        {
        }

        public class InheritedClass : ImplementedClass
        {
        }

        public class ImplementedInheritedClass : IInheritedInterface
        {
        }
    }
}