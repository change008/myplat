using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myplat.Dal;
using myplat.Entity;
using System.Configuration;
using log4net;
using myplat.Util;

namespace myplat.Biz
{
    public class ManagerBiz
    {
        #region dal相关操作
        //dal
        private readonly ManagerDal dal = new ManagerDal();

        /// <summary>
        /// add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Manager model)
        {
            model.Password = MD5Encrypt.Md5Hex(model.Password); //加密
            return dal.Add(model);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(int id)
        {
            return dal.Delete(id) == 1;
        }

        /// <summary>
        /// 更新Model数据，根据id进行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Manager model)
        {
            return dal.Update(model) == 1;
        }

        /// <summary>
        /// 请求单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Manager Get(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据名称获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Manager GetModelByName(string name)
        {
            return dal.getModelByName(name);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool UpdatePwd(int id, string pwd)
        {
            string pwd1 = MD5Encrypt.Md5Hex(pwd);
            return dal.UpdatePwd(id, pwd1) == 1;
        }


        /// <summary>
        /// 所有用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Manager> GetList()
        {
            return dal.GetList();
        }

        #endregion

        /// <summary>
        /// 用户cookie管理
        /// </summary>
        private static readonly UserAuthCookie _userCookie = new UserAuthCookie();

        /// <summary>
        /// log4net
        /// </summary>
        private static ILog _logger = LogManager.GetLogger(typeof(ManagerBiz));

        /// <summary>
        /// 校验用户
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Manager Check(string name, string password, out int Id)
        {
            Manager admin = GetModelByName(name);
            string pwd = MD5Encrypt.Md5Hex(password);
            if (admin != null && admin.Password == pwd)
            {
                //_userCookie.SignIn(_CookieName, name, admin.Id);
                Id = admin.Id;
                return admin;
            }
            else
            {
                Id = 0;
                return null;
            }
        }




        /// <summary>
        /// 验证管理员权限
        /// </summary>
        /// <returns>true|false</returns>
        public bool VerificationAdmin()
        {
            return VerificationAdmin(UserAuthCookie._CookieName);
        }
        public bool VerificationAdmin(string cookieName)
        {
            try
            {
                int userID = _userCookie.GetUserID(cookieName);
                if (userID <= 0) return false;
                Manager admin = Get(userID);
                if (admin != null)
                { 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }




    }
}
