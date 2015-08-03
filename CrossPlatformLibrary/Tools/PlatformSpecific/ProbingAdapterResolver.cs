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
        private Assembly assembly;

        public ProbingAdapterResolver()
            : this(assemblyName => { return Assembly.Load(assemblyName); })
        {
        }

        public ProbingAdapterResolver(Func<AssemblyName, Assembly> assemblyLoader)
        {
            Guard.ArgumentNotNull(() => assemblyLoader);

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
                    Assembly platformSpecificAssembly = this.GetPlatformSpecificAssembly();
                    instance = ResolveAdapter(platformSpecificAssembly, type, args);
                    this.adapters.Add(type, instance);
                }

                return instance;
            }
        }

        private static object ResolveAdapter(Assembly assembly, Type interfaceType, object[] args)
        {
            string typeName = MakeAdapterTypeName(interfaceType);

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

        private static string MakeAdapterTypeName(Type interfaceType) // TODO GATH: Refactor: Find types which implement interfaceType
        {
            Guard.ArgumentIsTrue(() => interfaceType.GetTypeInfo().IsInterface);
            Guard.ArgumentIsTrue(() => interfaceType.DeclaringType == null);
            Guard.ArgumentIsTrue(() => interfaceType.Name.StartsWith("I", StringComparison.Ordinal));

            // For example, if we're looking for an implementation of System.Security.Cryptography.ICryptographyFactory, 
            // then we'll look for System.Security.Cryptography.CryptographyFactory
            return interfaceType.Namespace + "." + interfaceType.Name.Substring(1);
        }

        private Assembly GetPlatformSpecificAssembly()
        {
            // We should be under a lock

            if (this.assembly == null)
            {
                this.assembly = this.ProbeForPlatformSpecificAssembly();
                if (this.assembly == null)
                {
                    throw new InvalidOperationException("AssemblyNotSupported");
                }
            }

            return this.assembly;
        }

        private Assembly ProbeForPlatformSpecificAssembly()
        {
            AssemblyName assemblyName = new AssemblyName(this.GetType().GetTypeInfo().Assembly.FullName);
            assemblyName.Name = "CrossPlatformLibrary.Platform";

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