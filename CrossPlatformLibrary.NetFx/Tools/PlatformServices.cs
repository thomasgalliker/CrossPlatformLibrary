using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tracing;

namespace CrossPlatformLibrary.Tools
{
    public class PlatformServices : PlatformServicesBase
    {
        private const string DllFileExtension = ".dll";

        public PlatformServices(ITracer tracer)
            : base(tracer)
        {
        }

        public override Assembly[] GetAssemblies()
        {
#if NETFX_CORE
            return AsyncInline.Run(this.LoadInternal).ToArray();
#else
            // Overriding GetAssemblies with a more efficient and less error-prone implementation
            return AppDomain.CurrentDomain.GetAssemblies();
#endif
        }

        public override void LoadReferencedAssemblies()
        {
            AsyncInline.Run(this.LoadInternal);
        }

        private async Task<IEnumerable<Assembly>> LoadInternal()
        {
            var dllFiles = await this.GetAssemblyFiles();

            return dllFiles.Select(this.LoadAssembly).ToList();
        }

        private async Task<IEnumerable<FileInfo>> GetAssemblyFiles()
        {
            var folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var files = folder.GetFiles();
            return files.Where(f => f.Name.StartsWithAny(CrossPlatformLibrary.AssemblyNamespaces) && f.Name.EndsWith(DllFileExtension));
        }

        private Assembly LoadAssembly(FileInfo dllFile)
        {
            var assemblyName = dllFile.Name.Replace(DllFileExtension, "");
            try
            {
                this.tracer.Debug("Calling Assembly.Load({0})", assemblyName);
#if NETFX_CORE
                return Assembly.Load(new AssemblyName(assemblyName));
#else
                return Assembly.Load(assemblyName);
#endif
            }
            catch (Exception ex)
            {
                this.tracer.Exception(ex, "Assembly.Load({0}) failed. Exception: {1}", assemblyName, ex.Message);
                throw;
            }
        }
    }
}
