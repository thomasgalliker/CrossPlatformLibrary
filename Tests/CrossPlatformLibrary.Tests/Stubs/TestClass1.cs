using System;

namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass1 : ITestClass
    {
        public static int InstancesCount
        {
            get;
            private set;
        }

        public static void Reset()
        {
            InstancesCount = 0;
        }

        public TestClass1()
        {
            this.Identifier = Guid.NewGuid().ToString();
            InstancesCount++;
        }

        public string Identifier
        {
            get;
            private set;
        }
    }
}
