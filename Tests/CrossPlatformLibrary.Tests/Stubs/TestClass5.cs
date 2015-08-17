
using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestClass5
    {
        public ITestClass MyProperty
        {
            get;
            private set;
        }

        public TestClass5()
        {
            
        }

        [PreferredConstructor]
        public TestClass5(ITestClass myProperty)
        {
            this.MyProperty = myProperty;
        }
    }
}
