
using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass5
    {
        public ITestClass1 MyProperty
        {
            get;
            private set;
        }

        public TestClass5()
        {
            
        }

        [PreferredConstructor]
        public TestClass5(ITestClass1 myProperty)
        {
            this.MyProperty = myProperty;
        }
    }
}
