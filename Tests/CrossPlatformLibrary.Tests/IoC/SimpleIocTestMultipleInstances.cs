﻿using CrossPlatformLibrary.Tests.Stubs;

using System;
using System.Linq;

using CrossPlatformLibrary.IoC;

using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NEWUNITTEST
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
#endif

namespace CrossPlatformLibrary.Tests.IoC
{
    [TestClass]
    public class SimpleIocTestMultipleInstances
    {
        [TestMethod]
        public void TestAddingDefaultForClassRegisteredWithFactoryAndKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.AreSame(instance1, foundInstance1);
            Assert.AreNotSame(foundInstance1, defaultInstance);
        }

        [TestMethod]
        public void TestAddingFactoryAndKeyForClassRegisteredWithDefault()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            var defaultInstance = SimpleIoc.Default.GetInstance<TestClass1>();
            var foundInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.AreSame(instance1, foundInstance1);
            Assert.AreNotSame(defaultInstance, foundInstance1);
        }

        [TestMethod]
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

            Assert.AreSame(instance1, defaultInstance);
            Assert.AreSame(instance2, foundInstance2);
            Assert.AreNotSame(defaultInstance, foundInstance2);
        }

        [TestMethod]
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

            Assert.AreSame(instance1, foundInstance1);
            Assert.AreSame(instance2, foundInstance2);
            Assert.AreNotSame(foundInstance1, foundInstance2);
        }

        [TestMethod]
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
                Assert.Fail("InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
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

            Assert.AreSame(instance1, foundInstance1);
            Assert.AreSame(instance2, foundInstance2);
            Assert.AreNotSame(foundInstance1, foundInstance2);
        }

        [TestMethod]
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

            Assert.AreEqual(4, TestClass1.InstancesCount);

            var instances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            Assert.AreEqual(3, instances.Count());

            instances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            SimpleIoc.Default.GetInstance<TestClass1>(key3);

            Assert.AreEqual(4, instances.Count());

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
                    Assert.Fail();
                }
            }

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
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

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof (TestClass1));
            Assert.AreEqual(4, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);
        }

        [TestMethod]
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

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(4, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);
        }

        [TestMethod]
        public void TestGetAllInstancesWithInterface()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key2);
            var instance3 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key3);
            var instance4 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key4);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<ITestClass>();
            Assert.AreEqual(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof (ITestClass));
            Assert.AreEqual(5, allInstances.Count());

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
        }

        [TestMethod]
        public void TestGetAllInstancesWithInterfaceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";
            const string key3 = "MyKey3";
            const string key4 = "MyKey4";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass>(key2);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass>(key3);
            var instance4 = SimpleIoc.Default.GetInstance<ITestClass>(key4);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<ITestClass>();
            Assert.AreEqual(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.AreEqual(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
        }

        [TestMethod]
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

            var instance1 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key2);
            var instance3 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key3);
            var instance4 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key4);
            var instance5 = SimpleIoc.Default.GetInstance(typeof (TestClass2), key5);
            var instance6 = SimpleIoc.Default.GetInstance(typeof (TestClass2), key6);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances(typeof(TestClass1));
            Assert.AreEqual(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof(TestClass1));
            Assert.AreEqual(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);
        }

        [TestMethod]
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

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);
            Assert.IsNotNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);

            var createdInstances = SimpleIoc.Default.GetAllCreatedInstances<TestClass1>();
            Assert.AreEqual(4, createdInstances.Count());

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(5, allInstances.Count()); // including default instance

            foreach (var instance in allInstances)
            {
                Assert.IsNotNull(instance);

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

            Assert.IsNull(instance1);
            Assert.IsNull(instance2);
            Assert.IsNull(instance3);
            Assert.IsNull(instance4);
            Assert.IsNotNull(instance5);
            Assert.IsNotNull(instance6);
        }

        [TestMethod]
        public void TestGetEmptyInstances()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            SimpleIoc.Default.GetInstance<TestClass1>(key1);
            SimpleIoc.Default.GetInstance<TestClass1>(key2);

            var allInstances = SimpleIoc.Default.GetAllInstances(typeof (TestClass2));

            Assert.IsNotNull(allInstances);
            Assert.AreEqual(0, allInstances.Count());
        }

        [TestMethod]
        public void TestGetEmptyInstancesGeneric()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            SimpleIoc.Default.GetInstance<TestClass1>(key1);
            SimpleIoc.Default.GetInstance<TestClass1>(key2);

            var allInstances = SimpleIoc.Default.GetAllInstances<TestClass2>();

            Assert.IsNotNull(allInstances);
            Assert.AreEqual(0, allInstances.Count());
        }

        [TestMethod]
        public void TestGetInstanceWithNullKey()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            const string key1 = "My key 1";

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);

            var instance01 = SimpleIoc.Default.GetInstance<TestClass1>(null);
            var instance02 = SimpleIoc.Default.GetInstance<TestClass1>(string.Empty);
            var instance03 = SimpleIoc.Default.GetInstance<TestClass1>();

            Assert.AreNotSame(instance1, instance01);
            Assert.AreSame(instance01, instance02);
            Assert.AreSame(instance01, instance03);
            Assert.AreSame(instance02, instance03);
        }

        [TestMethod]
        public void TestGetInstancesWithInstance()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal2, key2);

            var instance1 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestGetInstancesWithInstanceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            var instanceOriginal1 = new TestClass1();
            var instanceOriginal2 = new TestClass1();

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal1, key1);
            SimpleIoc.Default.Register<ITestClass>(() => instanceOriginal2, key2);

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass>(key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestGetInstancesWithInterface()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof (ITestClass), key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestGetInstancesWithInterfaceGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<ITestClass>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<ITestClass>(key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestGetInstancesWithParameters()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();
            SimpleIoc.Default.Register<TestClass3>();

            const string key1 = "My key 1";
            const string key2 = "My key 2";

            var instance1 = SimpleIoc.Default.GetInstance<TestClass3>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass3>(key2);
            var property = SimpleIoc.Default.GetInstance<ITestClass>();

            Assert.IsNotNull(instance1);
            Assert.IsInstanceOfType(instance1, typeof (TestClass3));

            Assert.IsNotNull(instance2);
            Assert.IsInstanceOfType(instance2, typeof (TestClass3));

            Assert.AreNotSame(instance1, instance2);

            Assert.IsNotNull(instance1.SavedProperty);
            Assert.AreSame(instance1.SavedProperty, property);
            Assert.IsNotNull(instance2.SavedProperty);
            Assert.AreSame(instance2.SavedProperty, property);
        }

        [TestMethod]
        public void TestGetInstancesWithType()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key1);
            var instance2 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key1);
            var instance3 = SimpleIoc.Default.GetInstance(typeof (TestClass1), key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestGetInstancesWithTypeGeneric()
        {
            const string key1 = "MyKey1";
            const string key2 = "MyKey2";

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            var instance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance2 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            var instance3 = SimpleIoc.Default.GetInstance<TestClass1>(key2);

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof (TestClass1));
            Assert.IsInstanceOfType(instance2, typeof (TestClass1));
            Assert.IsInstanceOfType(instance3, typeof (TestClass1));

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void TestOverwritingDefaultClassWithFactory()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            try
            {
                SimpleIoc.Default.Register(() => new TestClass1());
                Assert.Fail("InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
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

            Assert.AreSame(defaultInstance1, defaultInstance2);
            Assert.AreSame(instance11, instance21);
            Assert.AreSame(instance12, instance22);
        }

        [TestMethod]
        public void TestOverwritingFactoryWithDefaultClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            try
            {
                SimpleIoc.Default.Register<TestClass1>();
                Assert.Fail("InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
        public void TestOverwritingFactoryWithFactory()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new TestClass1());

            try
            {
                SimpleIoc.Default.Register(() => new TestClass1());
                Assert.Fail("InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
        public void TestOverwritingInterfaceClassWithOtherClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<ITestClass, TestClass1>();

            try
            {
                SimpleIoc.Default.Register<ITestClass, TestClass4>();
                Assert.Fail("InvalidOperationException was expected");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
        public void TestGettingDefaultInstanceAfterRegisteringFactoryAndKey()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);

            try
            {
                SimpleIoc.Default.GetInstance<TestClass1>();
                Assert.Fail("ActivationException was expected");
            }
            catch (ActivationException)
            {
            }
        }

        [TestMethod]
        public void TestGetAllInstancesGeneric1()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.AreEqual(2, instances.Count);

            var getInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.AreSame(instance1, getInstance1);

            Assert.IsTrue(instances.Contains(instance1));

            instances.Remove(instance1);
            Assert.AreEqual(1, instances.Count);

            var getInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.AreSame(instances[0], getInstance2);

            SimpleIoc.Default.GetInstance<TestClass1>("key2");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.AreEqual(3, instances.Count);
        }

        [TestMethod]
        public void TestGetAllInstancesGeneric2()
        {
            SimpleIoc.Default.Reset();

            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(1, instances.Count());

            SimpleIoc.Default.GetInstance<TestClass1>("key1");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(2, instances.Count());
        }

        [TestMethod]
        public void TestGetAllInstancesGeneric3()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass, TestClass1>();
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.AreEqual(3, instances.Count());
        }

        [TestMethod]
        public void TestGetAllInstancesGeneric4()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1());
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.AreEqual(3, instances.Count());
        }

        [TestMethod]
        public void TestGetAllInstances1()
        {
            SimpleIoc.Default.Reset();

            const string key1 = "key1";
            var instance1 = new TestClass1();
            SimpleIoc.Default.Register(() => instance1, key1);

            SimpleIoc.Default.Register<TestClass1>();

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.AreEqual(2, instances.Count);

            var getInstance1 = SimpleIoc.Default.GetInstance<TestClass1>(key1);
            Assert.AreSame(instance1, getInstance1);

            Assert.IsTrue(instances.Contains(instance1));

            instances.Remove(instance1);
            Assert.AreEqual(1, instances.Count);

            var getInstance2 = SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.AreSame(instances[0], getInstance2);

            SimpleIoc.Default.GetInstance<TestClass1>("key2");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>().ToList();
            Assert.AreEqual(3, instances.Count);
        }

        [TestMethod]
        public void TestGetAllInstances2()
        {
            SimpleIoc.Default.Reset();

            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance);

            var instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(1, instances.Count());

            SimpleIoc.Default.GetInstance<TestClass1>("key1");

            instances = SimpleIoc.Default.GetAllInstances<TestClass1>();
            Assert.AreEqual(2, instances.Count());
        }

        [TestMethod]
        public void TestGetAllInstances3()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass, TestClass1>();
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.AreEqual(3, instances.Count());
        }

        [TestMethod]
        public void TestGetAllInstances4()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1());
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key1");
            SimpleIoc.Default.Register<ITestClass>(() => new TestClass1(), "key2");

            var instances = SimpleIoc.Default.GetAllInstances<ITestClass>();
            Assert.AreEqual(3, instances.Count());
        }
    }
}