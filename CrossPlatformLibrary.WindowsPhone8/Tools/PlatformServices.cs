using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tracing;
using Windows.Storage;

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
            // Overriding GetAssemblies with a more efficient and less error-prone implementation
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public override void LoadAssemblies()
        {
            AsyncInline.Run(this.LoadInternal);
        }

        private async Task LoadInternal()
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path);
            var files = await folder.GetFilesAsync();
            var dllFiles = files.Where(f => f.Name.StartsWithAny(CrossPlatformLibrary.AssemblyNamespaces) && f.Name.EndsWith(DllFileExtension)).ToList();

            foreach (var dllFile in dllFiles)
            {
                var assemblyName = dllFile.Name.Replace(DllFileExtension, "");
                try
                {
                    this.tracer.Debug("Calling Assembly.Load({0})", assemblyName);
                    Assembly.Load(assemblyName);
                }
                catch (Exception ex)
                {
                    this.tracer.Exception(ex, "Assembly.Load({0}) failed. Exception: {1}", assemblyName, ex.Message);
                }
            }
        }
    }
}
