using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace myplat.Entity
{
    //Core
    public class Core
    {

        /// <summary>
        /// Id
        /// </summary>		
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// Title
        /// </summary>		
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// Intro
        /// </summary>		
        public string Intro
        {
            get;
            set;
        }
        /// <summary>
        /// Cover
        /// </summary>		
        public string Cover
        {
            get;
            set;
        }
        /// <summary>
        /// ShowTime
        /// </summary>		
        public DateTime ShowTime
        {
            get;
            set;
        }
        /// <summary>
        /// Author
        /// </summary>		
        public string Author
        {
            get;
            set;
        }
        /// <summary>
        /// ViewCount
        /// </summary>		
        public int ViewCount
        {
            get;
            set;
        }
        /// <summary>
        /// DingCount
        /// </summary>		
        public int DingCount
        {
            get;
            set;
        }
        /// <summary>
        /// OriginalLink
        /// </summary>		
        public string OriginalLink
        {
            get;
            set;
        }
        /// <summary>
        /// HContent
        /// </summary>		
        public string HContent
        {
            get;
            set;
        }

        /// <summary>
        /// 内部访问链接
        /// </summary>
        public string FrameLink
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string RedirectLink
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 类型-普通类型
        /// </summary>
        public const int CoreType_Normal = 1;

        /// <summary>
        /// 数据状态，1：正常， 2：已删除
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 状态-正常
        /// </summary>
        public const int CoreStatus_Normal = 1;
        /// <summary>
        /// 已删除
        /// </summary>
        public const int CoreStatus_Del = 2;

        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }

    }
}