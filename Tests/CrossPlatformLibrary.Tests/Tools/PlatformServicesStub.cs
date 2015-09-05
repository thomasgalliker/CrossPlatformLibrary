using System.Reflection;

using CrossPlatformLibrary.Tools;

namespace CrossPlatformLibrary.Tests.Tools
{
    public class PlatformServicesStub : IPlatformServices
    {
        public Assembly[] GetAssemblies()
        {
            return new Assembly[]{};
        }

        public void LoadReferencedAssemblies()
        {
        }
    }
}
