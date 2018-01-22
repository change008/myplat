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
        private static DefaultDataFacade dbTT;

        /// <summary>
        /// 获取连接字符串--CO数据库
        /// </summary>
        public static DefaultDataFacade DbTT
        {
            get
            {
                if (dbTT == null)
                {
                    dbTT = new DefaultDataFacade(ConnConfig.ConnStrTT);
                }
                return dbTT;
            }
        }




    }
}
