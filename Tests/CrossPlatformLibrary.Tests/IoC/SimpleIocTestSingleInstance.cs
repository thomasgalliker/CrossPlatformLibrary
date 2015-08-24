using System;

using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;

using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    [Collection("IoC Test Collection")]
    public class SimpleIocTestSingleInstance
    {
        [Fact]
        public void TestConstructWithProperty()
        {
            var property = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass6 { MyProperty = property });

            var instance1 = new TestClass6();
            Assert.NotNull(instance1);
            Assert.Null(instance1.MyProperty);

            var instance2 = SimpleIoc.Default.GetInstance<TestClass6>();
            Assert.NotNull(instance2);
            Assert.NotNull(instance2.MyProperty);
            Assert.Same(property, instance2.MyProperty);
        }

        [Fact]
        public void TestDefaultClassCreation()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<TestClass1>();
            SimpleIoc.Default.Register<TestClass2>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            var instance2 = SimpleIoc.Default.GetInstance<TestClass2>();

            Assert.IsType<TestClass1>(instance1);
            Assert.NotNull(instance1);
            Assert.IsType<TestClass2>(instance2);
            Assert.NotNull(instance2);
        }

        [Fact]
        public void TestGetInstanceWithGenericInterface()
        {
            SimpleIoc.Default.Reset();
            var instanceOriginal = new TestClass1();
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal);

            var instance = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.NotNull(instance);
            Assert.IsType<TestClass1>(instance);
            Assert.Same(instanceOriginal, instance);
        }

        [Fact]
        public void TestGetInstanceWithGenericType()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.NotNull(instance);
            Assert.IsType<TestClass1>(instance);
        }

        [Fact]
        public void TestGetInstanceWithInterface()
        {
            SimpleIoc.Default.Reset();
            var instanceOriginal = new TestClass1();
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal);

            var instance = SimpleIoc.Default.GetInstance(typeof(ITestClass));

            Assert.NotNull(instance);
            Assert.IsType<TestClass1>(instance);
            Assert.Same(instanceOriginal, instance);
        }

        [Fact]
        public void TestGetInstanceWithParameters()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();
            SimpleIoc.Default.Register<TestClass3>();

            var instance = SimpleIoc.Default.GetInstance<TestClass3>();
            var property = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.NotNull(instance);
            Assert.IsType<TestClass3>(instance);

            Assert.NotNull(instance.SavedProperty);
            Assert.Same(instance.SavedProperty, property);
        }

        [Fact]
        public void TestGetInstanceWithParametersRegisterByType()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass>(typeof(TestClass1));

            var instance = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.NotNull(instance);
            Assert.IsType<TestClass1>(instance);
        }

        [Fact]
        public void TestGetInstanceWithType()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance = SimpleIoc.Default.GetInstance(typeof(TestClass1));

            Assert.NotNull(instance);
            Assert.IsType<TestClass1>(instance);
        }

        [Fact]
        public void TestGetInstanceWithUnregisteredClass()
        {
            SimpleIoc.Default.Reset();

            try
            {
                SimpleIoc.Default.GetInstance<SimpleIocTestSingleInstance>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [Fact]
        public void TestRegisterInstanceWithMultiConstructors()
        {
            SimpleIoc.Default.Reset();

            try
            {
                SimpleIoc.Default.Register<TestClassWithMultiConstructors>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [Fact]
        public void TestRegisterInterfaceOnly()
        {
            try
            {
                SimpleIoc.Default.Register<ITestClass>();
                Assert.True(false, "ArgumentException was expected");
            }
            catch (ArgumentException)
            {
            }
        }

        [Fact]
        public void TestReset()
        {
            SimpleIoc.Default.Reset();
            var instanceOriginal = new TestClass1();
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal);
            var instance = SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.NotNull(instance);

            SimpleIoc.Default.Reset();

            try
            {
                SimpleIoc.Default.GetInstance<ITestClass>();
                Assert.True(false, "ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [Fact]
        public void TestGetDefaultWithoutCaching()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>();
            var instance2 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void TestGetFromFactoryWithoutCaching()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            var instance1 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>();
            var instance2 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>();

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void TestGetWithKeyWithoutCaching()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key = "key1";

            var instance1 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>(key);
            var instance2 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>(key);

            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void TestMixCacheAndNoCache()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstanceWithoutCaching<TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());

            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.True(SimpleIoc.Default.IsRegistered<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.NotSame(instance1, instance2);
        }
    }
}