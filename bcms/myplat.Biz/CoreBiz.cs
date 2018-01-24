using myplat.Dal;
using myplat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Biz
{
    public class CoreBiz
    {
        //dal
        private readonly CoreDal dal = new CoreDal();

        /// <summary>
        /// add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Core model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(int id)
        {
            return dal.UpdateStatus(id, Core.CoreStatus_Del) == 1;
        }

        /// <summary>
        /// 更新Model数据，根据id进行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Core model)
        {
            return dal.Update(model) == 1;
        }

        /// <summary>
        /// 请求单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Core Get(int id)
        {
            return dal.Get(id);
        }

    }
}
