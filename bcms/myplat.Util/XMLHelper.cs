using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;


namespace myplat.Util
{
    /// <summary>
    /// XMLHelper 的摘要说明
    /// </summary>
    public static class XMLHelper
    {
    

        /// <summary>
        /// 序列化xml
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool SaveLocalFileConfigSetting<T>(string address, T baseConfigList)
        {
            bool result = false;
            TextWriter writer = new StreamWriter(address);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(writer, baseConfigList);//要序列化的对象
                writer.Close();
                result = true;
                #region 测试
                //BaseData baseConfig = new BaseData();
                //baseConfig.OnceCheckCount = 200;
                //baseConfig.NeedCheckCount = 300;
                //baseConfig.Hourbefore = 0;
                //baseConfig.SimHashCoe = 0.91;
                //baseConfig.SimHashCoe = 0.81;
                //XMLHelper.SaveLocalFileConfigSetting(AppDomain.CurrentDomain.BaseDirectory + "BaseConfig.xml", baseConfig);
                #endregion
            }
            catch
            {
                writer.Close();
                return result;
            }
            return result;
        }

        /// <summary>
        ///  获取XML数据(反序列化xml为List<BaseConfig>)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static T GetLocalFileConfigSetting<T>(string address)
        {
            T baseConfig = default(T);
            TextReader reader = new StreamReader(address);
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T));
                object obj = deserializer.Deserialize(reader);
                baseConfig = (T)obj;
                reader.Close();
            }
            catch//(Exception ex)
            {
                reader.Close();
                return baseConfig;
            }
            return baseConfig;
        }



    }
}