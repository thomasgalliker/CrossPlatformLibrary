using System;
using System.Reflection;

using CrossPlatformLibrary.Tracing;
using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tools
{
    public abstract class PlatformServicesBase : IPlatformServices
    {
        protected readonly ITracer tracer;

        protected PlatformServicesBase(ITracer tracer)
        {
            Guard.ArgumentNotNull(() => tracer);

            this.tracer = tracer;
        }
        ///   <summary> 
        /// Returns all assemblies currently loaded in current AppDomain.
        /// Source: https://forums.xamarin.com/discussion/21255/device-getassemblies
        /// </summary>
        public virtual Assembly[] GetAssemblies()
        {
            var currentdomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
            var getAssembliesMethodInfo = currentdomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] { });
            if (getAssembliesMethodInfo == null)
            {
                const string ErrorMessage = "Could not reflect the method GetAssemblies. You may have to turn off the linker to get a valid result.";
                this.tracer.Error(ErrorMessage);
                throw new InvalidOperationException(ErrorMessage);
            }

            var assemblies = getAssembliesMethodInfo.Invoke(currentdomain, new object[] { }) as Assembly[];
            return assemblies;
        }

        public abstract void LoadAssemblies();
    }
}
