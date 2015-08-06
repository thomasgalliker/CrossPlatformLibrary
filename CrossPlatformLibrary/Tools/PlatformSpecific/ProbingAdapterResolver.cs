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
        private readonly Func<AssemblyName, string> platformNamingConvention;
        private readonly Func<Type, string> interfaceToClassNamingConvention;
        private Assembly assembly;

        public ProbingAdapterResolver(Func<AssemblyName, string> platformNamingConvention, Func<Type, string> interfaceToClassNamingConvention)
            : this(platformNamingConvention, interfaceToClassNamingConvention, Assembly.Load)
        {
        }

        public ProbingAdapterResolver(Func<AssemblyName, string> platformNamingConvention, Func<Type, string> interfaceToClassNamingConvention, Func<AssemblyName, Assembly> assemblyLoader)
        {
            Guard.ArgumentNotNull(() => platformNamingConvention);
            Guard.ArgumentNotNull(() => interfaceToClassNamingConvention);
            Guard.ArgumentNotNull(() => assemblyLoader);

            this.platformNamingConvention = platformNamingConvention;
            this.interfaceToClassNamingConvention = interfaceToClassNamingConvention;
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
                    instance = this.ResolveAdapter(platformSpecificAssembly, type, args);
                    this.adapters.Add(type, instance);
                }

                return instance;
            }
        }

        private object ResolveAdapter(Assembly assembly, Type interfaceType, object[] args)
        {
            string typeName = this.interfaceToClassNamingConvention(interfaceType);

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

                return type;
            }
            catch
            {
                return null;
            }
        }

        private string MakeAdapterTypeName(Type interfaceType) // TODO GATH: Refactor: Find types which implement interfaceType
        {
            Guard.ArgumentIsTrue(() => interfaceType.GetTypeInfo().IsInterface);
            Guard.ArgumentIsTrue(() => interfaceType.DeclaringType == null);
            Guard.ArgumentIsTrue(() => interfaceType.Name.StartsWith("I", StringComparison.Ordinal));

            return interfaceType.Namespace + "." + interfaceType.Name.Substring(1);
        }

        private Assembly GetPlatformSpecificAssembly(Type type)
        {
            // We should be under a lock

            ////if (this.assembly == null) // TODO Add dictionary with caching
            {
                this.assembly = this.ProbeForPlatformSpecificAssembly(type);
                if (this.assembly == null)
                {
                    throw new InvalidOperationException("AssemblyNotSupported"); // TODO Add real message
                }
            }

            return this.assembly;
        }

        private Assembly ProbeForPlatformSpecificAssembly(Type type)
        {
            AssemblyName assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            assemblyName.Name = this.platformNamingConvention(assemblyName);

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