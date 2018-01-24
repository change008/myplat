using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myplat.Entity;

namespace Tiexue.Mobile.TT.ManageUI.Helper
{
    public static class MixHelper
    {
        /// <summary>
        /// 根据id获得名称 -- 人工精选类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetArtificialTypeNameById(int id)
        {
            //var ret = TTArtificial.ArtificialTypeList.ToList().Find(n => n.Key == id);
            //if (ret.Key == 0)
            //{
            //    return id.ToString();
            //}
            //return ret.Value;
            return id.ToString();
        }
    }
}