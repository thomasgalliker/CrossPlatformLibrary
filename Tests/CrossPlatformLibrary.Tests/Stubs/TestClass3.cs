namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass3
    {
        public ITestClass SavedProperty
        {
            get;
            set;
        }

        public TestClass3(ITestClass parameter)
        {
            this.SavedProperty = parameter;
        }
    }
}
