using System;
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
            Guard.ArgumentNotNull(() => objToSerialize);

            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(objToSerialize.GetType());
            var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            serializer.Serialize(streamWriter, objToSerialize);
            memoryStream = (MemoryStream)streamWriter.BaseStream;
            return ByteConverter.Utf8ByteArrayToString(memoryStream.ToArray());
        }

        public static T DeserializeFromXml<T>(this string xmlString)
        {
            Guard.ArgumentNotNullOrEmpty(() => xmlString);
            Type type = typeof(T);
            Guard.ArgumentMustNotBeInterface(type);

            var serializer = new XmlSerializer(type);
            var memoryStream = new MemoryStream(ByteConverter.StringToUtf8ByteArray(xmlString));
            return (T)serializer.Deserialize(memoryStream);
        }
    }
}