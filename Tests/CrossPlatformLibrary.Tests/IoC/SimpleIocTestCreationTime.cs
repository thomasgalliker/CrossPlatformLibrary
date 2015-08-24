using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    public class SimpleIocTestCreationTime
    {
        [Fact]
        public void TestCreationOfMultipleInstances()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();

            var factoryWasUsed = 0;

            SimpleIoc.Default.Register(
                () =>
                {
                    factoryWasUsed++;
                    return new TestClassForCreationTime();
                });

            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);

            SimpleIoc.Default.GetInstance<TestClassForCreationTime>();
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>("Key1");
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>("Key2");

            Assert.Equal(3, TestClassForCreationTime.InstancesCreated);
            Assert.Equal(3, factoryWasUsed);
        }

        [Fact]
        public void TestCreationTimeForDefaultInstance()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.Register<TestClassForCreationTime>();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
        }

        [Fact]
        public void TestCreationTimeForNamedInstance()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.Register<TestClassForCreationTime>();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>("Key1");
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>("Key2");
            Assert.Equal(2, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>("Key1");
            Assert.Equal(2, TestClassForCreationTime.InstancesCreated);
        }

        [Fact]
        public void TestCreationTimeWithFactory()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.Register(() => new TestClassForCreationTime());
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<TestClassForCreationTime>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
        }

        [Fact]
        public void TestCreationTimeWithInterfaceForDefaultInstance()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.Register<ITestClass, TestClassForCreationTime>();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<ITestClass>();
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
        }

        [Fact]
        public void TestCreationTimeWithInterfaceForNamedInstance()
        {
            SimpleIoc.Default.Reset();
            TestClassForCreationTime.Reset();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.Register<ITestClass, TestClassForCreationTime>();
            Assert.Equal(0, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<ITestClass>("Key1");
            Assert.Equal(1, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<ITestClass>("Key2");
            Assert.Equal(2, TestClassForCreationTime.InstancesCreated);
            SimpleIoc.Default.GetInstance<ITestClass>("Key1");
            Assert.Equal(2, TestClassForCreationTime.InstancesCreated);
        }
    }
}