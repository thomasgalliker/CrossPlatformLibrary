using System;
using System.Reflection;

using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    // An implementation IAdapterResolver that probes for platforms-specific adapters by dynamically
    // looking for concrete types in platform-specific assemblies named MyBase.Platform
    internal class ProbingAdapterResolver : IAdapterResolver
    {
        private readonly Func<AssemblyName, Assembly> assemblyLoader;
        private readonly object lockObject = new object();
        private readonly IRegistrationConvention registrationConvention;

        public ProbingAdapterResolver(IRegistrationConvention registrationConvention)
            : this(registrationConvention, Assembly.Load)
        {
        }

        public ProbingAdapterResolver(IRegistrationConvention registrationConvention, Func<AssemblyName, Assembly> assemblyLoader)
        {
            Guard.ArgumentNotNull(() => registrationConvention);
            Guard.ArgumentNotNull(() => assemblyLoader);

            this.registrationConvention = registrationConvention;
            this.assemblyLoader = assemblyLoader;
        }

        public object Resolve(Type interfaceType, object[] args)
        {
            Guard.ArgumentNotNull(() => interfaceType);

            lock (this.lockObject)
            {
                var platformSpecificAssembly = this.ProbeForPlatformSpecificAssembly(interfaceType);
                var classType = this.TryConvertInterfaceTypeToClassType(platformSpecificAssembly, interfaceType);
                var instance = this.CreateInstance(classType, args);

                return instance;
            }
        }

        public Type ResolveClassType(Type interfaceType, bool throwIfNotFound = true)
        {
            Guard.ArgumentNotNull(() => interfaceType);

            lock (this.lockObject)
            {
                var platformSpecificAssembly = this.ProbeForPlatformSpecificAssembly(interfaceType);
                if (platformSpecificAssembly == null)
                {
                    string errorMessage = string.Format("PlatformNotSupportedException: Platform-specific assembly which implements interface {0} could not be found. Make sure your project references all necessary platform-specific assemblies.", interfaceType.FullName);
                    throw new PlatformNotSupportedException(errorMessage);
                }

                var classType = this.TryConvertInterfaceTypeToClassType(platformSpecificAssembly, interfaceType);

                if (classType == null && throwIfNotFound)
                {
                    string errorMessage = string.Format("PlatformNotSupportedException: Type {0} could not be resolved.", interfaceType.FullName);
                    throw new PlatformNotSupportedException(errorMessage);
                }

                return classType;
            }
        }

        private Type TryConvertInterfaceTypeToClassType(Assembly assembly, Type interfaceType)
        {
            string typeName = this.registrationConvention.InterfaceToClassNamingConvention(interfaceType);
            try
            {
                 return assembly.GetType(typeName);
            }
            catch(Exception ex)
            {
                //TODO GATH: Trace
            }

            try
            {
                return this.GetType().GetTypeInfo().Assembly.GetType(typeName);
            }
            catch(Exception ex)
            {
                //TODO GATH: Trace
            }

            return null;
        }

        private object CreateInstance(Type type, object[] args)
        {
            if (type != null)
            {
                try
                {
                    return Activator.CreateInstance(type, args);
                }
                catch(Exception ex)
                {
                    //TODO GATH: Trace
                }
            }

            return null;
        }

        private Assembly ProbeForPlatformSpecificAssembly(Type interfaceType)
        {
            AssemblyName assemblyName = new AssemblyName(interfaceType.GetTypeInfo().Assembly.FullName);
            assemblyName.Name = this.registrationConvention.PlatformNamingConvention(assemblyName);

            Assembly assm = null;
            try
            {
                assm = this.assemblyLoader(assemblyName);
            }
            catch (Exception)
            {
                // Try again without the SN for WP8
                // HACK...no real strong name support here
                assemblyName.SetPublicKey(null);
                assemblyName.SetPublicKeyToken(null);

                try
                {
                    assm = this.assemblyLoader(assemblyName);
                }
                catch (Exception)
                {
                }
            }

            return assm;
        }
    }
}