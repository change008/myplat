using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiexue.Framework.Data;

namespace myplat.Dal
{
    public static class DBFactory
    {
        /// <summary>
        /// 综合数据库
        /// </summary>
        private static DefaultDataFacade dbMain;

        /// <summary>
        /// 获取连接字符串--Main数据库
        /// </summary>
        public static DefaultDataFacade DbMain
        {
            get
            {
                if (dbMain == null)
                {
                    dbMain = new DefaultDataFacade(ConnConfig.ConnStrMain);
                }
                return dbMain;
            }
        }




    }
}
