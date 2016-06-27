using System.Reflection;

namespace CrossPlatformLibrary.Tools
{
    public class PlatformServices : IPlatformServices
    {
        public Assembly[] GetAssemblies()
        {
            throw Exceptions.NotImplementedInReferenceAssembly();
        }

        public void LoadReferencedAssemblies()
        {
            throw Exceptions.NotImplementedInReferenceAssembly();
        }
    }
}
