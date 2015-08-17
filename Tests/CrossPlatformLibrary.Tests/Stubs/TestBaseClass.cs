using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tests.Stubs
{
    public class TestBaseClass
    {
        public void Remove()
        {
            SimpleIoc.Default.Unregister(this);
        }
    }
}