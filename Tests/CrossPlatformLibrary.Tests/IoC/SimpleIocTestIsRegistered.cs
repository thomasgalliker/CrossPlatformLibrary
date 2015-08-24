using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    public class SimpleIocTestIsRegistered
    {
        [Fact]
        public void TestIsClassRegistered()
        {
            SimpleIoc.Default.Reset();

            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>());
            SimpleIoc.Default.Register<TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());

            SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());

            SimpleIoc.Default.Unregister<TestClass1>();
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>());
        }

        [Fact]
        public void TestIsClassRegisteredWithFactory()
        {
            SimpleIoc.Default.Reset();

            var instance = new TestClass1();
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>());
            SimpleIoc.Default.Register(() => instance);
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());

            SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());

            SimpleIoc.Default.Unregister<TestClass1>();
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>());
        }

        [Fact]
        public void TestIsClassRegisteredWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "My key 1";
            const string key2 = "My key 2";

            var instance = new TestClass1();
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>(key2));

            SimpleIoc.Default.Register(() => instance, key1);
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>(key2));

            SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>(key1));

            SimpleIoc.Default.Unregister<TestClass1>(key1);
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>(key2));
        }

        [Fact]
        public void TestIsInterfaceRegistered()
        {
            SimpleIoc.Default.Reset();

            Assert.False(SimpleIoc.Default.IsRegistered<ITestClass>());
            SimpleIoc.Default.Register<ITestClass, TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<ITestClass>());

            SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.True(SimpleIoc.Default.IsRegistered<ITestClass>());

            SimpleIoc.Default.Unregister<ITestClass>();
            Assert.False(SimpleIoc.Default.IsRegistered<ITestClass>());
        }

        [Fact]
        public void TestTryGetInstanceIfIsRegistered()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.NotNull(SimpleIoc.Default.TryGetInstance<TestClass1>());
        }

        [Fact]
        public void TestTryGetInstanceIfIsNotRegistered()
        {
            SimpleIoc.Default.Reset();

            Assert.False(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.Null(SimpleIoc.Default.TryGetInstance<TestClass1>());
        }

        [Fact]
        public void TestIsRegisteredThenContinue()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            // Previous versions of SimpleIoc would throw an exception. Instead
            // new versions just ignore and continue. This fixes issues with Expression Blend,
            // for instance.
            SimpleIoc.Default.Register<ITestClass, TestClass1>();
        }
    }
}