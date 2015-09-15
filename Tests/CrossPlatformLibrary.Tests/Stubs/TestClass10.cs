
namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass10 : ITestClass10
    {
        public ITestClass1 TestClass1 { get; private set; }

        public TestClass10(ITestClass1 testClass1)
        {
            this.TestClass1 = testClass1;
        }
    }
}
