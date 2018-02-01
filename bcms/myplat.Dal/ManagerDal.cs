using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myplat.Entity;
using System.Data.SqlClient;
using System.Data;

namespace myplat.Dal
{
    public class ManagerDal
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Manager(");
            strSql.Append("Name,Limit,Password,Des,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@Name,@Limit,@Password,@Des,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY();");
            SqlParameter[] parameters = {
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Limit", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@Password", SqlDbType.NChar,50) ,
                        new SqlParameter("@Des", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Name;
            parameters[1].Value = model.Limit;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Des;
            parameters[4].Value = model.CreateTime;

            object obj = DbHelperSQLMain.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Manager set ");

            strSql.Append(" Name = @Name , ");
            strSql.Append(" Limit = @Limit , ");
            strSql.Append(" Password = @Password , ");
            strSql.Append(" Des = @Des , ");
            strSql.Append(" CreateTime = @CreateTime  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Limit", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@Password", SqlDbType.NChar,50) ,
                        new SqlParameter("@Des", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Limit;
            parameters[3].Value = model.Password;
            parameters[4].Value = model.Des;
            parameters[5].Value = model.CreateTime;
            int rows = DbHelperSQLMain.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd1"></param>
        /// <returns></returns>
        public int UpdatePwd(int id, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Manager set ");
            strSql.Append(" Password = @Password , ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Password", SqlDbType.NChar,50) 
            };

            parameters[0].Value = id;
            parameters[1].Value = pwd;
            int rows = DbHelperSQLMain.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            string sql = "delete from Core where Id = {0}";
            int ret = DBFactory.DbMain.ExecuteCommand(sql, id);
            return ret;
        }

       


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Manager GetModel(int id)
        {
            Manager model = DBFactory.DbMain.Single<Manager>("select * from Manager(nolock) where Id = @Id", new SqlParameter("Id", id));
            return model;
        }
        /// <summary>
        /// list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Manager> GetList()
        {
            string sqlStr = "select * from Manager order by CreateTime desc";
            return DBFactory.DbMain.Select<Manager>(sqlStr);
        }

        /// <summary>
        /// 根据名称查询数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Manager getModelByName(string name)
        {
            Manager model = DBFactory.DbMain.Single<Manager>("select * from Manager(nolock) where [Name] = @Name", new SqlParameter("Name", name));
            return model;
        }

    }
}
