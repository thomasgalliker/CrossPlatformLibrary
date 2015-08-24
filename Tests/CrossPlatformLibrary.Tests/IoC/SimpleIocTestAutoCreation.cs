using CrossPlatformLibrary.Tests.Stubs;

using System.Linq;

using CrossPlatformLibrary.IoC;

using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    public class SimpleIocTestAutoCreation
    {
        [Fact]
        public void TestAutoCreationWithDefaultClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>(true);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestAutoCreationWithFactory()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(
                () => new TestClass1(),
                true);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestAutoCreationWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "key1";
            SimpleIoc.Default.Register(
                () => new TestClass1(),
                key1,
                true);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestAutoCreationWithFactoryForInterface()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass>(
                () => new TestClass1(),
                true);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestAutoCreationWithFactoryForInterfaceAndKey()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "key1";
            SimpleIoc.Default.Register<ITestClass>(
                () => new TestClass1(),
                key1,
                true);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestAutoCreationWithInterface()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>(true);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Equal(1, instances.Count());

            var defaultInstance = SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.Same(defaultInstance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithDefaultClass()
        {
            SimpleIoc.Default.Reset();

            TestClass1.Reset();

            SimpleIoc.Default.Register<TestClass1>();
            Assert.Equal(0, TestClass1.InstancesCount);

            SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();

            var instance = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithFactory()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            Assert.Equal(0, TestClass1.InstancesCount);

            SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();

            var instance = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();

            const string key1 = "key1";
            SimpleIoc.Default.Register(
                () => new TestClass1(),
                key1);

            Assert.Equal(0, TestClass1.InstancesCount);

            var instance = SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Same(instance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithFactoryForInterface()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1());

            Assert.Equal(0, TestClass1.InstancesCount);

            var instance = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Same(instance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithFactoryForInterfaceAndKey()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();

            const string key1 = "key1";
            SimpleIoc.Default.Register<ITestClass>(
                () => new TestClass1(),
                key1);

            Assert.Equal(0, TestClass1.InstancesCount);

            var instance = SimpleIoc.Default.GetInstance<ITestClass>(key1);

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Same(instance, instances.ElementAt(0));
        }

        [Fact]
        public void TestDelayedCreationWithInterface()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            Assert.Equal(0, TestClass1.InstancesCount);

            var instance = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.Equal(1, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.Same(instance, instances.ElementAt(0));
        }
    }
}