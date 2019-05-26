using System;
using System.Linq;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    [Collection("IoC")]
    public class SimpleIocTestMultipleInstances
    {
        [Fact]
        public void TestAddingDefaultForClassRegisteredWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.Same(instance1, foundInstance1);
            Assert.NotSame(foundInstance1, defaultInstance);
        }

        [Fact]
        public void TestAddingFactoryAndKeyForClassRegisteredWithDefault()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();
            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.Same(instance1, foundInstance1);
            Assert.NotSame(defaultInstance, foundInstance1);
        }

        [Fact]
        public void TestAddingFactoryAndKeyForClassRegisteredWithFactory()
        {
            SimpleIoc.Default.Reset();

            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1);

            const string key = "key";
            var instance2 = new TestClass1();
            SimpleIoc.Default.Register(() => instance2, key);

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();
            var foundInstance2 = SimpleIoc.Default.GetInstance<TestClass1>(key);

            Assert.Same(instance1, defaultInstance);
            Assert.Same(instance2, foundInstance2);
            Assert.NotSame(defaultInstance, foundInstance2);
        }

        [Fact]
        public void TestAddingFactoryAndKeyForClassRegisteredWithFactoryAndDifferentKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            const string key2 = "key2";
            var instance2 = new TestClass1();
            SimpleIoc.Default.Register(() => instance2, key2);

            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var foundInstance2 = SimpleIoc.Default.GetInstance<TestClass1>(key2);

            Assert.Same(instance1, foundInstance1);
            Assert.Same(instance2, foundInstance2);
            Assert.NotSame(foundInstance1, foundInstance2);
        }

        [Fact]
        public void TestAddingFactoryAndKeyForClassRegisteredWithFactoryAndSameKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            var instance2 = new TestClass1();

            try
            {
                SimpleIoc.Default.Register(() => instance2, key1);
                Assert.True(false, "InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void TestAddingFactoryForClassRegisteredWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            var instance2 = new TestClass1();
            SimpleIoc.Default.Register(() => instance2);

            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var foundInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.Same(instance1, foundInstance1);
            Assert.Same(instance2, foundInstance2);
            Assert.NotSame(foundInstance1, foundInstance2);
        }

        [Fact]
        public void TestGetAllInstancesOfClassWithCreation()
        {
            SimpleIoc.Default.Reset();
            TestClass1.Reset();

            var instance0 = new TestClass1();
            SimpleIoc.Default.Register(() => instance0, true);

            const string key1 = "key1";
            const string key2 = "key2";
            const string key3 = "key3";
            var instance1 = new TestClass1();
            var instance2 = new TestClass1();
            var instance3 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1, true);
            SimpleIoc.Default.Register(() => instance2, key2, true);
            SimpleIoc.Default.Register(() => instance3, key3);

            Assert.Equal(4, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            instances.Should().HaveCount(3);

            instances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            SimpleIoc.Default.GetInstance<TestClass1>(key3);

            instances.Should().HaveCount(4);

            var list = instances.ToList();

            foreach (var instance in instances)
            {
                if (instance == instance0
                    || instance == instance1
                    || instance == instance2
                    || instance == instance3)
                {
                    list.Remove(instance);
                }
                else
                {
                    Assert.True(false);
                }
            }

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TestGetAllInstancesWithInstance()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";
            const string key5 = "MyKey5";
            const string key6 = "MyKey6";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();
            var instanceOriginal3 = new TestClass1();
            var instanceOriginal4 = new TestClass1();
            var instanceOriginal5 = new TestClass4();
            var instanceOriginal6 = new TestClass4();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register(() => instanceOriginal2, key2);
            SimpleIoc.Default.Register(() => instanceOriginal3, key3);
            SimpleIoc.Default.Register(() => instanceOriginal4, key4);
            SimpleIoc.Default.Register(() => instanceOriginal5, key5);
            SimpleIoc.Default.Register(() => instanceOriginal6, key6);

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key2);
            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key3);
            var instance4 = SimpleIoc.Default.GetInstance<TestClass1>(key4);
            var instance5 = SimpleIoc.Default.GetInstance<TestClass4>(key5);
            var instance6 = SimpleIoc.Default.GetInstance<TestClass4>(key6);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof(TestClass1));
            Assert.Equal(4, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }

                if (instance.Equals(instance5))
                {
                    instance5 = null;
                }

                if (instance.Equals(instance6))
                {
                    instance6 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);
        }

        [Fact]
        public void TestGetAllInstancesWithInstanceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";
            const string key5 = "MyKey5";
            const string key6 = "MyKey6";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();
            var instanceOriginal3 = new TestClass1();
            var instanceOriginal4 = new TestClass1();
            var instanceOriginal5 = new TestClass4();
            var instanceOriginal6 = new TestClass4();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register(() => instanceOriginal2, key2);
            SimpleIoc.Default.Register(() => instanceOriginal3, key3);
            SimpleIoc.Default.Register(() => instanceOriginal4, key4);
            SimpleIoc.Default.Register(() => instanceOriginal5, key5);
            SimpleIoc.Default.Register(() => instanceOriginal6, key6);

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key2);
            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key3);
            var instance4 = SimpleIoc.Default.GetInstance<TestClass1>(key4);
            var instance5 = SimpleIoc.Default.GetInstance<TestClass4>(key5);
            var instance6 = SimpleIoc.Default.GetInstance<TestClass4>(key6);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Equal(4, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }

                if (instance.Equals(instance5))
                {
                    instance5 = null;
                }

                if (instance.Equals(instance6))
                {
                    instance6 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);
        }

        [Fact]
        public void TestGetAllInstancesWithInterface()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key2);
            var instance3 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key3);
            var instance4 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key4);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<ITestClass1>();
            Assert.Equal(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof(ITestClass1));
            Assert.Equal(5, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
        }

        [Fact]
        public void TestGetAllInstancesWithInterfaceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass1>(key2);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass1>(key3);
            var instance4 = SimpleIoc.Default.GetInstance<ITestClass1>(key4);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<ITestClass1>();
            Assert.Equal(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances<ITestClass1>();
            Assert.Equal(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
        }

        [Fact]
        public void TestGetAllInstancesWithType()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";
            const string key5 = "MyKey5";
            const string key6 = "MyKey6";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();
            SimpleIoc.Default.Register<TestClass2>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key2);
            var instance3 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key3);
            var instance4 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key4);
            var instance5 = SimpleIoc.Default.GetInstance(typeof(TestClass2), key5);
            var instance6 = SimpleIoc.Default.GetInstance(typeof(TestClass2), key6);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances(typeof(TestClass1));
            Assert.Equal(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof(TestClass1));
            Assert.Equal(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }

                if (instance.Equals(instance5))
                {
                    instance5 = null;
                }

                if (instance.Equals(instance6))
                {
                    instance6 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);
        }

        [Fact]
        public void TestGetAllInstancesWithTypeGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";
            const string key5 = "MyKey5";
            const string key6 = "MyKey6";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();
            SimpleIoc.Default.Register<TestClass2>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key2);
            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key3);
            var instance4 = SimpleIoc.Default.GetInstance<TestClass1>(key4);
            var instance5 = SimpleIoc.Default.GetInstance<TestClass2>(key5);
            var instance6 = SimpleIoc.Default.GetInstance<TestClass2>(key6);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);
            Assert.NotNull(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            Assert.Equal(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.Equal(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.NotNull(instance);

                if (instance.Equals(instance1))
                {
                    instance1 = null;
                }

                if (instance.Equals(instance2))
                {
                    instance2 = null;
                }

                if (instance.Equals(instance3))
                {
                    instance3 = null;
                }

                if (instance.Equals(instance4))
                {
                    instance4 = null;
                }

                if (instance.Equals(instance5))
                {
                    instance5 = null;
                }

                if (instance.Equals(instance6))
                {
                    instance6 = null;
                }
            }

            Assert.Null(instance1);
            Assert.Null(instance2);
            Assert.Null(instance3);
            Assert.Null(instance4);
            Assert.NotNull(instance5);
            Assert.NotNull(instance6);
        }

        [Fact]
        public void TestGetEmptyInstances()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            SimpleIoc.Default.GetInstance<TestClass1>(key1);
            SimpleIoc.Default.GetInstance<TestClass1>(key2);

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof(TestClass2));

            Assert.NotNull(allInstances);
            Assert.Equal(0, allInstances.Count());
        }

        [Fact]
        public void TestGetEmptyInstancesGeneric()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            SimpleIoc.Default.GetInstance<TestClass1>(key1);
            SimpleIoc.Default.GetInstance<TestClass1>(key2);

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass2>();

            Assert.NotNull(allInstances);
            Assert.Equal(0, allInstances.Count());
        }

        [Fact]
        public void TestGetInstanceWithNullKey()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);

            var instance01 = SimpleIoc.Default.GetInstance<TestClass1>(null);
            var instance02 = SimpleIoc.Default.GetInstance<TestClass1>(string.Empty);
            var instance03 = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.NotSame(instance1, instance01);
            Assert.Same(instance01, instance02);
            Assert.Same(instance01, instance03);
            Assert.Same(instance02, instance03);
        }

        [Fact]
        public void TestGetInstancesWithInstance()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1>(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register<ITestClass1>(() => instanceOriginal2, key2);

            var instance1 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestGetInstancesWithInstanceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1>(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register<ITestClass1>(() => instanceOriginal2, key2);

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass1>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass1>(key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestGetInstancesWithInterface()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof(ITestClass1), key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestGetInstancesWithInterfaceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass1>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass1>(key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestGetInstancesWithParameters()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();
            SimpleIoc.Default.Register<TestClass3>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            var instance1 = SimpleIoc.Default.GetInstance<TestClass3>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass3>(key2);
            var property = SimpleIoc.Default.GetInstance<ITestClass1>();

            Assert.NotNull(instance1);
            Assert.IsType<TestClass3>(instance1);

            Assert.NotNull(instance2);
            Assert.IsType<TestClass3>(instance2);

            Assert.NotSame(instance1, instance2);

            Assert.NotNull(instance1.SavedProperty);
            Assert.Same(instance1.SavedProperty, property);
            Assert.NotNull(instance2.SavedProperty);
            Assert.Same(instance2.SavedProperty, property);
        }

        [Fact]
        public void TestGetInstancesWithType()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof(TestClass1), key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestGetInstancesWithTypeGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key2);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance3);

            Assert.IsType<TestClass1>(instance1);
            Assert.IsType<TestClass1>(instance2);
            Assert.IsType<TestClass1>(instance3);

            Assert.Same(instance1, instance2);
            Assert.NotSame(instance1, instance3);
        }

        [Fact]
        public void TestOverwritingDefaultClassWithFactory()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            try
            {
                SimpleIoc.Default.Register(() => new TestClass1());
                Assert.True(false, "InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void TestOverwritingDefaultClassWithSameDefaultClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "key1";
            const string key2 = "key2";

            var defaultInstance1 = SimpleIoc.Default.GetInstance<TestClass1>();
            var instance11 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance12 = SimpleIoc.Default.GetInstance<TestClass1>(key2);

            SimpleIoc.Default.Register<TestClass1>();

            var defaultInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            var instance21 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance22 = SimpleIoc.Default.GetInstance<TestClass1>(key2);

            Assert.Same(defaultInstance1, defaultInstance2);
            Assert.Same(instance11, instance21);
            Assert.Same(instance12, instance22);
        }

        [Fact]
        public void TestOverwritingFactoryWithDefaultClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            try
            {
                SimpleIoc.Default.Register<TestClass1>();
                Assert.True(false, "InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void TestOverwritingFactoryWithFactory()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            try
            {
                SimpleIoc.Default.Register(() => new TestClass1());
                Assert.True(false, "InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void TestOverwritingInterfaceClassWithOtherClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass1, TestClass1>();

            try
            {
                SimpleIoc.Default.Register<ITestClass1, TestClass4>();
                Assert.True(false, "InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void TestGettingDefaultInstanceAfterRegisteringFactoryAndKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);

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
        public void TestGetAllInstancesGeneric1()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.Equal(2, instances.Count);

            var getInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.Same(instance1, getInstance1);

            Assert.True(instances.Contains(instance1));

            instances.Remove(instance1);
            Assert.Equal(1, instances.Count);

            var getInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instances[0], getInstance2);

            SimpleIoc.Default.GetInstance<TestClass1>("key2");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.Equal(3, instances.Count);
        }

        [Fact]
        public void TestGetAllInstancesGeneric2()
        {
            SimpleIoc.Default.Reset();

            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            instances.Should().HaveCount(1);

            SimpleIoc.Default.GetInstance<TestClass1>("key1");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            instances.Should().HaveCount(2);
        }

        [Fact]
        public void TestGetAllInstancesGeneric3()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass1, TestClass1>();
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass1>();
            instances.Should().HaveCount(3);
        }

        [Fact]
        public void TestGetAllInstancesGeneric4()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1());
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass1>();
            instances.Should().HaveCount(3);
        }

        [Fact]
        public void TestGetAllInstances1()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.Equal(2, instances.Count);

            var getInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.Same(instance1, getInstance1);

            Assert.True(instances.Contains(instance1));

            instances.Remove(instance1);
            Assert.Equal(1, instances.Count);

            var getInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.Same(instances[0], getInstance2);

            SimpleIoc.Default.GetInstance<TestClass1>("key2");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.Equal(3, instances.Count);
        }

        [Fact]
        public void TestGetAllInstances2()
        {
            SimpleIoc.Default.Reset();

            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            instances.Should().HaveCount(1);

            SimpleIoc.Default.GetInstance<TestClass1>("key1");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            instances.Should().HaveCount(2);
        }

        [Fact]
        public void TestGetAllInstances3()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass1, TestClass1>();
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass1>();
            instances.Should().HaveCount(3);
        }

        [Fact]
        public void TestGetAllInstances4()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1());
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass1>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass1>();
            instances.Should().HaveCount(3);
        }
    }
}