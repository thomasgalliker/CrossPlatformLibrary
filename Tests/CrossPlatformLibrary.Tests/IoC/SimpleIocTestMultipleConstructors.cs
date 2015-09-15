using System.Diagnostics;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    [Collection("IoC")]
    public class SimpleIocTestMultipleOrPrivateConstructors
    {
        [Fact]
        public void TestBuildInstanceWithMultipleConstructorsNotMarkedWithAttribute()
        {
            var property = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass6(property));

            var instance1 = new TestClass6();
            Assert.NotNull(instance1);
            Assert.Null(instance1.MyProperty);

            var instance2 = SimpleIoc.Default.GetInstance<TestClass6>();
            Assert.NotNull(instance2);
            Assert.NotNull(instance2.MyProperty);
            Assert.Same(property, instance2.MyProperty);
        }

        [Fact]
        public void TestBuildWithMultipleConstructors()
        {
            var property = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1>(() => property);
            SimpleIoc.Default.Register<TestClass5>();

            var instance1 = new TestClass5();
            Assert.NotNull(instance1);
            Assert.Null(instance1.MyProperty);

            var instance2 = SimpleIoc.Default.GetInstance<TestClass5>();
            Assert.NotNull(instance2);
            Assert.NotNull(instance2.MyProperty);
            Assert.Same(property, instance2.MyProperty);
        }

        [Fact]
        public void TestBuildWithMultipleConstructorsNotMarkedWithAttribute()
        {
            var property = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1>(() => property);

            try
            {
                SimpleIoc.Default.Register<TestClass6>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void TestBuildWithPrivateConstructor()
        {
            SimpleIoc.Default.Reset();

            try
            {
                SimpleIoc.Default.Register<TestClass7>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        
        [Fact]
        public void TestBuildWithStaticConstructor()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass8>();
        }

        [Fact]
        public void TestPublicAndInternalConstructor()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass9>();
        }
    }
}