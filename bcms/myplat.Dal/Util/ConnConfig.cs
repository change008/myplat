using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Dal
{
    public class ConnConfig
    {

        /// <summary>
        /// 获取连接字符串--CO数据库
        /// </summary>
        public static string ConnStrTT
        {
            get
            {
                // string _connectionString = ConfigurationManager.AppSettings["DBConnString"];
                string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringWXJiang"].ConnectionString;
                return _connectionString;
            }
        }

    }
}
