using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    public class SimpleIocTestContains
    {
        [Fact]
        public void TestContainsClass()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestClass1>();

            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            SimpleIoc.Default.GetInstance<TestClass1>();
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());
        }

        [Fact]
        public void TestContainsInstance()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "My key";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);
            SimpleIoc.Default.Register<TestClass2>();

            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass3>());

            SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass3>());

            SimpleIoc.Default.GetInstance<TestClass2>();

            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass2>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass3>());
        }

        [Fact]
        public void TestContainsInstanceForKey()
        {
            SimpleIoc.Default.Reset();
            const string key1 = "My key";
            const string key2 = "My key2";
            var instance = new TestClass1();
            SimpleIoc.Default.Register(() => instance, key1);
            SimpleIoc.Default.Register<TestClass2>();

            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>(key2));

            SimpleIoc.Default.GetInstance<TestClass1>(key1);

            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestClass1>(key1));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass1>(key2));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass2>(key1));
            Assert.False(SimpleIoc.Default.ContainsCreated<TestClass3>(key1));
        }

        [Fact]
        public void TestContainsInstanceAfterUnregister()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<TestBaseClass>(true);

            Assert.True(SimpleIoc.Default.IsRegistered<TestBaseClass>());
            Assert.True(SimpleIoc.Default.ContainsCreated<TestBaseClass>());

            var instance = SimpleIoc.Default.GetInstance<TestBaseClass>();
            instance.Remove();

            Assert.True(SimpleIoc.Default.IsRegistered<TestBaseClass>());
            Assert.False(SimpleIoc.Default.ContainsCreated<TestBaseClass>());
        }
    }
}