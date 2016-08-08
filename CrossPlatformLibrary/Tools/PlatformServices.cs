using System.Reflection;

namespace CrossPlatformLibrary.Tools
{
    public class PlatformServices : IPlatformServices
    {
        public Assembly[] GetAssemblies()
        {
            throw new NotImplementedInReferenceAssemblyException();
        }

        public void LoadReferencedAssemblies()
        {
            throw new NotImplementedInReferenceAssemblyException();
        }
    }
}
