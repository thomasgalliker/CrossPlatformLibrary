namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass6
    {
        public ITestClass1 MyProperty
        {
            get;
            set;
        }

        public TestClass6()
        {
            
        }

        public TestClass6(ITestClass1 myProperty)
        {
            this.MyProperty = myProperty;
        }
    }
}
