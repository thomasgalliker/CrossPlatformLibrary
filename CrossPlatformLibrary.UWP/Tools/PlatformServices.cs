﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Internals;
using Windows.Storage;

namespace CrossPlatformLibrary.Tools
{
    /// <summary>
    /// Provides an implementation of <see cref="IPlatformServices"/> for Windows Phone, Windows Store Apps and Universal Windows Platform.
    /// </summary>
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

        private async Task<IEnumerable<StorageFile>> GetAssemblyFiles()
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path);
            var files = await folder.GetFilesAsync();
            return files.Where(f => f.Name.StartsWithAny(new[]{"CrossPlatformLibrary"}) && f.Name.EndsWith(DllFileExtension));
        }

        private Assembly LoadAssembly(IStorageFile dllFile)
        {
            var assemblyName = dllFile.Name.Replace(DllFileExtension, "");
            try
            {
                this.tracer.Debug($"Calling Assembly.Load({assemblyName})");
#if NETFX_CORE
                return Assembly.Load(new AssemblyName(assemblyName));
#else
                return Assembly.Load(assemblyName);
#endif
            }
            catch (Exception ex)
            {
                this.tracer.Exception(ex, $"Assembly.Load({assemblyName}) failed. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
