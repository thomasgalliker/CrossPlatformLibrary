namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass6
    {
        public ITestClass MyProperty
        {
            get;
            set;
        }

        public TestClass6()
        {
            
        }

        public TestClass6(ITestClass myProperty)
        {
            this.MyProperty = myProperty;
        }
    }
}
