using System;
using System.Reflection;

namespace CrossPlatformLibrary.Tools
{
    public class PlatformServices : IPlatformServices
    {
        ///   <summary> 
        /// Returns all assemblies currently loaded in current AppDomain.
        /// Source: https://forums.xamarin.com/discussion/21255/device-getassemblies
        /// </summary>
        public Assembly[] GetAssemblies()
        {
            var currentdomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
            var getAssembliesMethodInfo = currentdomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] { });
            if (getAssembliesMethodInfo == null)
            {
                throw new InvalidOperationException("Could not reflect the method GetAssemblies. You may have to turn off the linker to get a valid result.");
            }

            var assemblies = getAssembliesMethodInfo.Invoke(currentdomain, new object[] { }) as Assembly[];
            return assemblies;
        }

    }
}
