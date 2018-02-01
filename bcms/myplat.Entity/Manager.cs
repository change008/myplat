using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace myplat.Entity
{
    //Manager
    public class Manager
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
        /// Name
        /// </summary>		
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Limit
        /// </summary>		
        public string Limit
        {
            get;
            set;
        }
        /// <summary>
        /// Password
        /// </summary>		
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// Des
        /// </summary>		
        public string Des
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        public string CreateTimeStr
        {
            get;
            set;
        }
    }
}