using System.IO;
using System.Xml.Serialization;

namespace CrossPlatformLibrary.IO
{
    public static class XmlSerializerHelper
    {
        public static string Serialize<T>(T obj)
        {
            var outStream = new StringWriter();
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(outStream, obj);
            return outStream.ToString();
        }

        public static T Deserialize<T>(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
            {
                return default(T);
            }

            var inStream = new StringReader(serialized);
            var ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(inStream);
        }
    }
}