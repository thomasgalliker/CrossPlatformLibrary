using System.IO;
using System.Xml;
using System.Xml.Linq;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.IO
{
    public static class StreamExtensions
    {
        public static XDocument ToXDocument(this Stream stream)
        {
            Guard.ArgumentNotNull(() => stream);

            XDocument document;
            using (var reader = XmlReader.Create(stream))
            {
                document = XDocument.Load(reader);
            }

            return document;
        }
    }
}
