using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

using CrossPlatformLibrary.Utils;

using Guards;

namespace CrossPlatformLibrary.IO
{
    public static class XmlSerializerHelper
    {
        /// <summary>
        /// Serializes objects into XML strings.
        /// </summary>
        /// <param name="value">The object to be serialized.</param>
        /// <returns>The serialized XML string.</returns>
        public static string SerializeToXml(this object value)
        {
            Guard.ArgumentNotNull(() => value);

            var sourceType = value.GetType();
            var objectToSerialize = new ValueToTypeMapping { Value = value, TypeName = sourceType.FullName };

            var serializer = new XmlSerializer(objectToSerialize.GetType(), new[] { sourceType });

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    serializer.Serialize(streamWriter, objectToSerialize);
                    return ByteConverter.Utf8ByteArrayToString(((MemoryStream)streamWriter.BaseStream).ToArray());
                } 
            }
        }

        /// <summary>
        /// Deserializes XML strings into objects of given type T.
        /// </summary>
        /// <typeparam name="T">Target type T.</typeparam>
        /// <param name="xmlString">The serialized XML string.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeFromXml<T>(this string xmlString)
        {
            Guard.ArgumentNotNullOrEmpty(() => xmlString);

            Type targetType = typeof(T);
            bool isTargetTypeAnInterface = targetType.GetTypeInfo().IsInterface;
            Type[] extraTypes = { };
            if (!isTargetTypeAnInterface)
            {
                extraTypes = new[] { targetType };
            }

            var serializerBefore = new XmlSerializer(typeof(ValueToTypeMapping), extraTypes);
            ValueToTypeMapping deserializedObject = null;

            using (var memoryStream = new MemoryStream(ByteConverter.StringToUtf8ByteArray(xmlString)))
            {
                deserializedObject = (ValueToTypeMapping)serializerBefore.Deserialize(memoryStream);
            }

            Type serializedType = Type.GetType(deserializedObject.TypeName);

            // If the target type is an interface, we need to deserialize again with more type information
            if (isTargetTypeAnInterface)
            {
                var serializerAfter = new XmlSerializer(typeof(ValueToTypeMapping), new[] { serializedType });
                using (var memoryStream = new MemoryStream(ByteConverter.StringToUtf8ByteArray(xmlString)))
                {
                    deserializedObject = (ValueToTypeMapping)serializerAfter.Deserialize(memoryStream);
                }

                return (T)Convert.ChangeType(deserializedObject.Value, serializedType);
            }

            return (T)deserializedObject.Value;
        }

        public class ValueToTypeMapping
        {
            public string TypeName { get; set; }

            public object Value { get; set; }
        }
    }
}