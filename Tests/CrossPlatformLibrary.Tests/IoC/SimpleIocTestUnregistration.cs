using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    [Collection("IoC")]
    public class SimpleIocTestUnregistration
    {
        [Fact]
        [Trait("Category", "SystemTest")]
        public void TestUnregisterClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.NotNull(instance1);

            SimpleIoc.Default.Unregister<TestClass1>();

            try
            {
                SimpleIoc.Default.GetInstance<TestClass1>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [Fact]
        public void TestUnregisterInstance()
        {
            var instanceOriginal1 = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => instanceOriginal1);

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instanceOriginal1, instance1);

            SimpleIoc.Default.Unregister(instanceOriginal1);

            try
            {
                var instance2 = SimpleIoc.Default.GetInstance<TestClass1>();
                Assert.Same(instance1, instance2);
            }
            catch (ActivationException)
            {
                Assert.True(false, "ActivationException was thrown");
            }
        }

        [Fact]
        public void TestUnregisterInstanceWithKey()
        {
            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();
            const string key1 = "My key 1";
            const string key2 = "My key 2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register(() => instanceOriginal2, key2);

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.Same(instanceOriginal1, instance1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key2);
            Assert.Same(instanceOriginal2, instance2);

            SimpleIoc.Default.Unregister<TestClass1>(key1);

            try
            {
                SimpleIoc.Default.GetInstance<TestClass1>(key1);
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [Fact]
        public void TestUnregisterAndImmediateRegisterWithInterface()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass1, TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<ITestClass1>());

            SimpleIoc.Default.Unregister<ITestClass1>();
            Assert.False(SimpleIoc.Default.IsRegistered<ITestClass1>());

            SimpleIoc.Default.Register<ITestClass1, TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<ITestClass1>());
        }

        public void TestUnregisterInstanceAndGetNewInstance()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            SimpleIoc.Default.Unregister(instance1);

            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.NotEqual(instance1.Identifier, instance2.Identifier);
        }

        [Fact]
        public void TestUnregisterFactoryInstance()
        {
            SimpleIoc.Default.Reset();

            var instance0 = new TestClass1();

            SimpleIoc.Default.Register(() => instance0);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance0, instance1);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance0, instance2);

            SimpleIoc.Default.Unregister(instance0);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance0, instance3);
        }

        [Fact]
        public void TestUnregisterDefaultInstance()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance1, instance2);

            SimpleIoc.Default.Unregister(instance1);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestUnregisterKeyedInstance()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            const string key = "key1";
            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key);
            Assert.Same(instance1, instance2);

            SimpleIoc.Default.Unregister(instance1);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key);
            Assert.NotSame(instance1, instance3);
        }
    }
}