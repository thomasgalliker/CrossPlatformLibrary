﻿using CrossPlatformLibrary.Tests.Stubs;
using CrossPlatformLibrary.IoC;

using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NEWUNITTEST
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
#endif

namespace CrossPlatformLibrary.Tests.IoC
{
    [TestClass]
    public class SimpleIocTestContains
    {
        [TestMethod]
        public void TestContainsClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>());
            SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>());
        }

        [TestMethod]
        public void TestContainsInstance()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "My key";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);
            SimpleIoc.Default.Register<TestClass2>();

            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass3>());

            SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass3>());

            SimpleIoc.Default.GetInstance<TestClass2>();

            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass3>());
        }

        [TestMethod]
        public void TestContainsInstanceForKey()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "My key";
            const string key2 = "My key2";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);
            SimpleIoc.Default.Register<TestClass2>();

            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>(key2));

            SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass1>(key2));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass2>(key1));
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestClass3>(key1));
        }

        [TestMethod]
        public void TestContainsInstanceAfterUnregister()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestBaseClass>(true);

            Assert.IsTrue(SimpleIoc.Default.IsRegistered<TestBaseClass>());
            Assert.IsTrue(SimpleIoc.Default.ContainsCreated<TestBaseClass>());

            var instance = SimpleIoc.Default.GetInstance<TestBaseClass>();
            instance.Remove();

            Assert.IsTrue(SimpleIoc.Default.IsRegistered<TestBaseClass>());
            Assert.IsFalse(SimpleIoc.Default.ContainsCreated<TestBaseClass>());
        }
    }
}