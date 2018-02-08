using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myplat.Entity;
using System.Data.SqlClient;
using System.Data;
using myplat.Util;

namespace myplat.Dal
{
    /// <summary>
    /// CoreDal
    /// </summary>
    public class CoreDal
    {
        //增删改查业务逻辑

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Core model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Core(Title, Intro, Cover, ShowTime, Author, ViewCount, DingCount, OriginalLink, HContent, FrameLink, RedirectLink, Type, Status, Imgs)");
            strSql.Append(" values (");
            strSql.Append("@Title, @Intro, @Cover, @ShowTime, @Author, @ViewCount, @DingCount, @OriginalLink, @HContent, @FrameLink, @RedirectLink, @Type, @Status, @Imgs");
            strSql.Append(" ) ");
            strSql.Append(";select SCOPE_IDENTITY();");
            SqlParameter[] parameters =
            {
                new SqlParameter("Title", SqlDbType.NVarChar, 255),
                new SqlParameter("Intro", SqlDbType.NVarChar, 2048),
                new SqlParameter("Cover", SqlDbType.NVarChar, 2048),
                new SqlParameter("ShowTime", SqlDbType.DateTime),
                new SqlParameter("Author", SqlDbType.NVarChar, 50),
                new SqlParameter("ViewCount", SqlDbType.Int, 4),
                new SqlParameter("DingCount", SqlDbType.Int, 4),
                new SqlParameter("OriginalLink", SqlDbType.NVarChar, 2048),
                new SqlParameter("HContent", SqlDbType.NVarChar, -1),
                new SqlParameter("FrameLink", SqlDbType.NVarChar, 2048),
                new SqlParameter("RedirectLink", SqlDbType.NVarChar, 2048),
                new SqlParameter("Type", SqlDbType.Int, 4),
                new SqlParameter("Status", SqlDbType.Int, 4),
                new SqlParameter("Imgs", SqlDbType.NVarChar, -1)
            };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Intro;
            parameters[2].Value = model.Cover.IfNullToEmpty();
            parameters[3].Value = model.ShowTime;
            parameters[4].Value = model.Author.IfNullToEmpty();
            parameters[5].Value = model.ViewCount;
            parameters[6].Value = model.DingCount;
            parameters[7].Value = model.OriginalLink.IfNullToEmpty();
            parameters[8].Value = model.HContent;
            parameters[9].Value = model.FrameLink.IfNullToEmpty();
            parameters[10].Value = model.RedirectLink.IfNullToEmpty();
            parameters[11].Value = model.Type;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.Imgs.IfNullToThis("[]");

            int coreId = int.Parse(DbHelperSQLMain.GetSingle(strSql.ToString(), parameters).ToString());
            return coreId;
        }

        /// <summary>
        /// 删除--逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateStatus(int id, int status)
        {
            string sql = "update Core set Status = {0} where Id = {1}";
            int ret = DBFactory.DbMain.ExecuteCommand(sql, status, id);
            return ret;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Core model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Core set ");

            strSql.Append(" HContent = @HContent , ");
            strSql.Append(" FrameLink = @FrameLink , ");
            strSql.Append(" RedirectLink = @RedirectLink , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Intro = @Intro , ");
            strSql.Append(" Cover = @Cover , ");
            strSql.Append(" ShowTime = @ShowTime , ");
            strSql.Append(" Author = @Author , ");
            strSql.Append(" ViewCount = @ViewCount , ");
            strSql.Append(" DingCount = @DingCount , ");
            strSql.Append(" OriginalLink = @OriginalLink,  ");
            strSql.Append(" Imgs = @Imgs,  ");
            strSql.Append(" Type = @Type,  ");
            strSql.Append(" Status = @Status  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@HContent", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@FrameLink", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@RedirectLink", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@Title", SqlDbType.NVarChar,512) ,
                        new SqlParameter("@Intro", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@Cover", SqlDbType.NVarChar,2048) ,
                        new SqlParameter("@ShowTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Author", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ViewCount", SqlDbType.Int,4) ,
                        new SqlParameter("@DingCount", SqlDbType.Int,4) ,
                        new SqlParameter("@OriginalLink", SqlDbType.NVarChar,2048),
                        new SqlParameter("@Imgs", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@Type", SqlDbType.Int,4) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.HContent;
            parameters[2].Value = model.FrameLink;
            parameters[3].Value = model.RedirectLink;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.Intro;
            parameters[6].Value = model.Cover;
            parameters[7].Value = model.ShowTime;
            parameters[8].Value = model.Author;
            parameters[9].Value = model.ViewCount;
            parameters[10].Value = model.DingCount;
            parameters[11].Value = model.OriginalLink;
            parameters[12].Value = model.Imgs;
            parameters[13].Value = model.Type;
            parameters[14].Value = model.Status;
            int rows = DbHelperSQLMain.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }


        /// <summary>
        /// 请求单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Core Get(int id)
        {
            Core model = DBFactory.DbMain.Single<Core>("select * from Core(nolock) where Id = @Id", new SqlParameter("Id", id));
            return model;
        }

        /// <summary>
        /// 请求数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) as count from Core with(nolock) ");
            sb.Append(sqlWhere);

            return DBFactory.DbMain.ExecuteScalar<int>(sb.ToString());
        }

        /// <summary>
        /// 请求具体数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IEnumerable<Core> GetList(int startIndex, int endIndex, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM ( SELECT * , ROW_NUMBER() OVER(Order by a.ID DESC ) AS Row FROM Core AS a with(nolock) {0} ) AS b", strWhere);
            strSql.AppendFormat(" WHERE Row between {0} and {1}", startIndex, endIndex);

            IEnumerable<Core> list = DBFactory.DbMain.Select<Core>(strSql.ToString());
            return list;
        }

    }
}
