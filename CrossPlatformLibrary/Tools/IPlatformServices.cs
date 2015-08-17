using System.Reflection;

namespace CrossPlatformLibrary.Tools
{
    public interface IPlatformServices
    {
        Assembly[] GetAssemblies();

        void LoadAssemblies();
    }
}