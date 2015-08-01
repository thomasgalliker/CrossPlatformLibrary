using System.IO;
using System.Text;
using System.Xml.Serialization;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.IO
{
    public static class XmlSerialierHelper
    {
        public static string SerializeToXml(this object objToSerialize)
        {
            if (objToSerialize == null)
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(objToSerialize.GetType());
            var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            serializer.Serialize(streamWriter, objToSerialize);
            memoryStream = (MemoryStream)streamWriter.BaseStream;
            return ByteConverter.Utf8ByteArrayToString(memoryStream.ToArray());
        }

        public static T DeserializeFromXml<T>(this string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            var memoryStream = new MemoryStream(ByteConverter.StringToUtf8ByteArray(xml));
            return (T)serializer.Deserialize(memoryStream);
        }
    }
}