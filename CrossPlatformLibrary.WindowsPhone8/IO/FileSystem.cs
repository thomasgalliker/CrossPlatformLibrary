using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;

namespace CrossPlatformLibrary.IO
{
    public static class FileSystem
    {
        // TODO GATH: How to deal with Filesystem related topics? PCLStorage?
        public static async Task<StorageFile> SaveToLocalFolderAsync(Stream stream, string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (Stream outputStream = await storageFile.OpenStreamForWriteAsync())
            {
                await stream.CopyToAsync(outputStream);
            }

            return storageFile;
        }
    }
}
