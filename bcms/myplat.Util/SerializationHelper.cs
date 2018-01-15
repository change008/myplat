using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;


namespace myplat.Util
{
    public static class SerializationHelper {

        /// <summary>
        /// 将可序列化对象转成Byte数组
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>返回相关数组</returns>
        public static byte[] ObjectToByteArray(object o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, o);
                ms.Close();
                return ms.ToArray();
            }

        }

        /// <summary>
        /// 将可序列化对象转成的byte数组还原为对象并压缩
        /// </summary>
        /// <param name="b">byte数组</param>
        /// <returns>相关对象</returns>
        public static object ByteArrayToObject(byte[] b)
        {
            using (MemoryStream ms = new MemoryStream(b, 0, b.Length))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(ms);
            }
        }

        /// <summary>  
        /// Deserialize an instance of T.  
        /// </summary>  
        /// <typeparam name="T">Any type.</typeparam>  
        /// <returns>The result of deserialized.</returns>  
        public static T Deserialize<T>(string path) {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
                return default(T);
            T t;
            using (FileStream fs = new FileStream(path, FileMode.Open , FileAccess.Read, FileShare.Read )) {
                BinaryFormatter formatter = new BinaryFormatter();
                t = (T) formatter.Deserialize(fs);
            }
            return t;
        }

        public static T Deserialize<T>(Stream stream) {
            if (stream == null)
                throw new ArgumentNullException("stream");
            
            var formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }

        public static void Serialize<T>(T obj, string path) {

            FileInfo fi = new FileInfo(path);
            if (fi.Exists) {
                File.Delete(path);
            }
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }

        public static void Serialize<T>(T obj, Stream stream) {
            if (stream == null)
                throw new ArgumentNullException("stream");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
        }

        public static bool SerializeToXml<T>(T obj, string path) {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (TextWriter writer = new StreamWriter(path, false)) {
                xs.Serialize(writer, obj);
            }
            return true;
        }

        public static bool SerializeToXml<T>(T obj, Stream stream) {
            if (stream == null)
                throw new ArgumentNullException("stream");
            var xmlSerializer = new XmlSerializer(obj.GetType());
            xmlSerializer.Serialize(stream, obj);
            return true;
        }

        public static T DeserializeFromXml<T>(string path) {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(path)) {
                return (T)xs.Deserialize(reader);
            }
        }

        public static T DeserializeFromXml<T>(Stream stream) {
            if (stream == null)
                throw new ArgumentNullException("stream");
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T) xs.Deserialize(stream);
        }

 


    }
}
