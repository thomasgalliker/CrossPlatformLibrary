namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClassForCreationTime : ITestClass
    {
        public static int InstancesCreated
        {
            get;
            private set;
        }

        public TestClassForCreationTime()
        {
            InstancesCreated++;
        }

        public static void Reset()
        {
            InstancesCreated = 0;
        }
    }
}
