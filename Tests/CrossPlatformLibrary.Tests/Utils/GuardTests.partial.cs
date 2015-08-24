using System;

using CrossPlatformLibrary.Utils;

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    public partial class GuardTests
    {
        [Fact]
        public void ArgumentNotNullThrowsIfArgumentIsNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((string)null, "argument"));
            Assert.Equal("argument", ex.ParamName);
        }

        [Fact]
        public void ArgumentNotNullNotThrowsIfArgumentNameIsNullOrEmpty()
        {
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNull(new object(), (string)null));
            // TODO GATH: Remove?? Assert.IsNotThrown<Exception>(() => Guard.ArgumentNotNull(new object(), string.Empty));

            ArgumentNullException ex1 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((object)null, (string)null));
            Assert.Null(ex1.ParamName);

            ArgumentNullException ex2 = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull((object)null, string.Empty));
            Assert.Equal(string.Empty, ex2.ParamName);
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
    }
}