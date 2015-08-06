using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    // An implementation IAdapterResolver that probes for platforms-specific adapters by dynamically
    // looking for concrete types in platform-specific assemblies named MyBase.Platform
    internal class ProbingAdapterResolver : IAdapterResolver
    {
        private readonly Func<AssemblyName, Assembly> assemblyLoader;
        private readonly object lockObject = new object();
        private readonly Dictionary<Type, object> adapters = new Dictionary<Type, object>();
        private readonly Func<AssemblyName, string> convertToPlatformSpecificAssemblyName;
        private readonly Func<Type, string> convertInterfaceToClassName;

        public ProbingAdapterResolver(Func<AssemblyName, string> platformNamingConvention, Func<Type, string> interfaceToClassNamingConvention)
            : this(platformNamingConvention, interfaceToClassNamingConvention, Assembly.Load)
        {
        }

        public ProbingAdapterResolver(Func<AssemblyName, string> platformNamingConvention, Func<Type, string> interfaceToClassNamingConvention, Func<AssemblyName, Assembly> assemblyLoader)
        {
            Guard.ArgumentNotNull(() => platformNamingConvention);
            Guard.ArgumentNotNull(() => interfaceToClassNamingConvention);
            Guard.ArgumentNotNull(() => assemblyLoader);

            this.convertToPlatformSpecificAssemblyName = platformNamingConvention;
            this.convertInterfaceToClassName = interfaceToClassNamingConvention;
            this.assemblyLoader = assemblyLoader;
        }

        public object Resolve(Type type, object[] args)
        {
            Debug.Assert(type != null);

            lock (this.lockObject)
            {
                object instance;
                if (!this.adapters.TryGetValue(type, out instance))
                {
                    Assembly platformSpecificAssembly = this.GetPlatformSpecificAssembly(type);
                    instance = this.CreateInstance(platformSpecificAssembly, type, args);
                    this.adapters.Add(type, instance);
                }

                return instance;
            }
        }

        private object CreateInstance(Assembly assembly, Type interfaceType, object[] args)
        {
            string typeName = this.convertInterfaceToClassName(interfaceType);

            try
            {
                Type type = assembly.GetType(typeName);
                if (type != null)
                {
                    return Activator.CreateInstance(type);
                }

                // Fallback to looking in this assembly for a default
                type = typeof(ProbingAdapterResolver).GetTypeInfo().Assembly.GetType(typeName);
                if (type != null)
                {
                    return Activator.CreateInstance(type, args);
                }
            }
            catch
            {
            }

            return null;
        }

        private Assembly GetPlatformSpecificAssembly(Type type)
        {
            var platformSpecificAssembly = this.ProbeForPlatformSpecificAssembly(type);
            if (platformSpecificAssembly == null)
            {
                throw new InvalidOperationException("AssemblyNotSupported");
            }
            return platformSpecificAssembly;
        }

        private Assembly ProbeForPlatformSpecificAssembly(Type type)
        {
            AssemblyName assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            assemblyName.Name = this.convertToPlatformSpecificAssemblyName(assemblyName);

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