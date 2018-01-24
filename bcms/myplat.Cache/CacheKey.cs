using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Cache
{
    public static class CacheKey
    {

        /****************************************军事头条KEY 开始**********************************************************/

        /// <summary>
        /// 用户数据缓存  KEY
        /// </summary>
        public static string User_Key = "User_Key";

        #region wap站 key
        private static string Wap_ArticleDetail = "Wap_ArticleDetail_{0}";
        public static string GetWapArticleDetail(int id)
        {
            return string.Format(Wap_ArticleDetail, id);
        }

        private static string Wap_ArticleList = "WapArticleList_{0}_{1}_{2}";
        public static string GetWapArticleList(int id,int rowNumber,int p)
        {
            return string.Format(Wap_ArticleList, id, rowNumber, p);
        }
        #endregion

    }
}
