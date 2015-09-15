

using System;

using CrossPlatformLibrary.Utils;

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    public class GuardTests
    {
        [Fact]
        public void ArgumentNotNullThrowsIfNullableArgumentIsNull()
        {
            // Arrange
            bool? testProp = null;

            // Act
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(() => testProp));

            // Assert
            Assert.Equal("testProp", ex.ParamName);
        }

        [Fact]
        public void ArgumentNotNullThrowsIfArgumentIsNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((string)null, "argument"));
            Assert.Equal("argument", ex.ParamName);
        }

        [Fact]
        public void ArgumentNotNullNotThrowsIfArgumentNameIsNullOrEmpty()
        {
            ArgumentNullException ex1 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((object)null, (string)null));
            Assert.Null(ex1.ParamName);

            ArgumentNullException ex2 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((object)null, string.Empty));
            Assert.Equal(string.Empty, ex2.ParamName);
        }

        [Fact]
        public void ArgumentMustNotExceedThrowsArgumentExceptionTooLow()
        {
            // Arrange
            const int MaxLength = 3;
            string inputTest = "1234";

            // Act
            ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentMustNotExceed(() => inputTest, MaxLength));

            // Assert
            Assert.Equal("inputTest", ex.ParamName);
        }

        [Fact]
        public void ArgumentNotNullThrowsWithProvidedArgumentName()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((object)null, "argument"));
            Assert.Equal("argument", ex.ParamName);
        }

        [Fact]
        public void ArgumentNotNullOrEmptyThrowsIfArgumentIsNullOrEmpty()
        {
            ArgumentNullException ex1 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, "argument"));
            Assert.Equal("argument", ex1.ParamName);

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(string.Empty, "argument"));
            Assert.Equal("argument", ex2.Message);

            ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty("", "argument"));
            Assert.Equal("argument", ex3.Message);
        }

        [Fact]
        public void ArgumentNotNullOrEmptyNotThrowsIfArgumentNameIsNullOrEmpty()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNullOrEmpty("string", (string)null));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNullOrEmpty("string", string.Empty));

            ArgumentNullException ex1 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, (string)null));
            Assert.Null(ex1.ParamName);

            ArgumentNullException ex2 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, string.Empty));
            Assert.Equal(string.Empty, ex2.ParamName);

            ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(string.Empty, (string)null));
            Assert.Null(ex3.ParamName);

            ArgumentException ex4 = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(string.Empty, string.Empty));
            Assert.Equal(string.Empty, ex4.Message);
        }

        [Fact]
        public void ArgumentNotNullOrEmptyThrowsWithProvidedArgumentName()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, "argument"));
            Assert.Equal("argument", ex.ParamName);

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(string.Empty, "argument"));
            Assert.Equal("argument", ex.ParamName);
        }

        ////[Fact]
        ////public void ArgumentIsNotNegativeThrowsIfArgumentIsNegative()
        ////{
        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(-1, "argument"));
        ////    Assert.Equal("argument", ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(int.MinValue, "argument"));
        ////    Assert.Equal("argument", ex2.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsNotNegativeNotThrowsIfArgumentNameIsNullOrEmpty()
        ////{
        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(-1, null));
        ////    Assert.Null(ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(-1, string.Empty));
        ////    Assert.Equal(string.Empty, ex2.ParamName);

        ////    ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(int.MinValue, null));
        ////    Assert.Null(ex3.ParamName);

        ////    ArgumentException ex4 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsNotNegative(int.MinValue, string.Empty));
        ////    Assert.Equal(string.Empty, ex4.ParamName);

        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(1, null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(1, string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(int.MaxValue, null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(int.MaxValue, string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(0, null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsNotNegative(0, string.Empty));
        ////}

        ////[Fact]
        ////public void ArgumentIsNotNegativeThrowsWithProvidedArgumentName()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsNotNegative(-1, "argument"));
        ////    Assert.Equal("argument", ex.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsNotNegativeThrowsWithProvidedArgumentValue()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsNotNegative(-1, "argument"));
        ////    //Not set because silverlight doesnt have this constructor
        ////    Assert.Equal(null, ex.ActualValue);
        ////}

        ////[Fact]
        ////public void ArgumentIsAssignableThrowsIfArgumentIsNotAsssignable()
        ////{
        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new NotImplementedClass(), "argument"));
        ////    Assert.Equal("argument", ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new ImplementedInheritedClass(), "argument"));
        ////    Assert.Equal("argument", ex2.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsAssignableNotThrowsIfArgumentNameIsNullOrEmpty()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedClass(), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new InheritedClass(), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedInheritedClass(), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInheritedInterface), new ImplementedInheritedClass(), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new InheritedClass(), null));

        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedClass(), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new InheritedClass(), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new ImplementedInheritedClass(), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(IInheritedInterface), new ImplementedInheritedClass(), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new InheritedClass(), string.Empty));

        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new NotImplementedClass(), null));
        ////    Assert.Null(ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new ImplementedInheritedClass(), null));
        ////    Assert.Null(ex2.ParamName);

        ////    ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new NotImplementedClass(), string.Empty));
        ////    Assert.Equal(string.Empty, ex3.ParamName);

        ////    ArgumentException ex4 = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(ImplementedClass), new ImplementedInheritedClass(), string.Empty));
        ////    Assert.Equal(string.Empty, ex4.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsAssignableThrowsWithProvidedArgumentName()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsAssignable(typeof(IInterface), new NotImplementedClass(), "argument"));
        ////    Assert.Equal("argument", ex.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentTypeIsAssignableWithTypeThrowsIfTypeIsNotAsssignable()
        ////{
        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(NotImplementedClass), "argument"));
        ////    Assert.Equal("argument", ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IOtherInterface), "argument"));
        ////    Assert.Equal("argument", ex2.ParamName);

        ////    ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(IInterface), "argument"));
        ////    Assert.Equal("argument", ex3.ParamName);

        ////    ArgumentException ex4 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(IInterface), "argument"));
        ////    Assert.Equal("argument", ex4.ParamName);

        ////    ArgumentException ex5 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(ImplementedInheritedClass), "argument"));
        ////    Assert.Equal("argument", ex5.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentTypeIsAssignableWithTypeNotThrowsIfArgumentNameIsNullOrEmpty()
        ////{
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedClass), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IInheritedInterface), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(InheritedClass), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedInheritedClass), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(ImplementedInheritedClass), null));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(InheritedClass), null));

        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedClass), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IInheritedInterface), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(InheritedClass), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(ImplementedInheritedClass), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(ImplementedInheritedClass), string.Empty));
        ////    // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(InheritedClass), string.Empty));

        ////    ArgumentException ex1 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(NotImplementedClass), null));
        ////    Assert.Null(ex1.ParamName);

        ////    ArgumentException ex2 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IOtherInterface), null));
        ////    Assert.Null(ex2.ParamName);

        ////    ArgumentException ex3 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(IInterface), null));
        ////    Assert.Null(ex3.ParamName);

        ////    ArgumentException ex4 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(IInterface), null));
        ////    Assert.Null(ex4.ParamName);

        ////    ArgumentException ex5 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(ImplementedInheritedClass), null));
        ////    Assert.Null(ex5.ParamName);

        ////    ArgumentException ex6 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(NotImplementedClass), string.Empty));
        ////    Assert.Equal(string.Empty, ex6.ParamName);

        ////    ArgumentException ex7 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(IOtherInterface), string.Empty));
        ////    Assert.Equal(string.Empty, ex7.ParamName);

        ////    ArgumentException ex8 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(IInterface), string.Empty));
        ////    Assert.Equal(string.Empty, ex8.ParamName);

        ////    ArgumentException ex9 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInheritedInterface), typeof(IInterface), string.Empty));
        ////    Assert.Equal(string.Empty, ex9.ParamName);

        ////    ArgumentException ex10 = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(ImplementedClass), typeof(ImplementedInheritedClass), string.Empty));
        ////    Assert.Equal(string.Empty, ex10.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentTypeIsAssignableWithTypeThrowsWithProvidedArgumentName()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentTypeIsAssignable(typeof(IInterface), typeof(NotImplementedClass), "argument"));
        ////    Assert.Equal("argument", ex.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeThrowsWithProvidedArgumentName()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "message"));
        ////    Assert.Equal("argument", ex.ParamName);
        ////}

        ////[Fact]
        ////public void ArgumentIsInRangeThrowsWithProvidedArgumentValue()
        ////{
        ////    ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.ArgumentIsInRange(false, "value", "argument", "message"));
        ////    //Not set because silverlight doesnt have this constructor
        ////    Assert.Equal(null, ex.ActualValue);
        ////}

        ////[Fact]
        ////public void ArgumentIsMatchingThrowsWithProvidedArgumentName()
        ////{
        ////    ArgumentException ex = Assert.Throws<ArgumentException>(() => Guard.ArgumentIsMatching(false, "value", "argument", "message"));
        ////    Assert.Equal("argument", ex.ParamName);
        ////}
        
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