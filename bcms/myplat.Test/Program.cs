using myplat.Dal;
using myplat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //CoreTestAdd();

            PwdCreate();
        }


        static void PwdCreate()
        {
            string pwd = "admin";
            string pwdmd5 = Util.MD5Encrypt.Md5Hex(pwd);
            string xyz = pwdmd5;
        }

        static void CoreTestAdd()
        {
            Core model = new Core();
            model.Author = "author";
            model.Cover = "cover";
            model.CreateTime = DateTime.Now;
            model.DingCount = 1;
            model.FrameLink = "frameLink";
            model.HContent = "hcontent";
            model.Intro = "Intro";
            model.OriginalLink = "originallink";
            model.RedirectLink = "redirectlink";
            model.ShowTime = DateTime.Now;
            model.Title = "title";
            model.ViewCount = 2;
            model.Type = 2;
            model.Status = 3;

            CoreDal dal = new CoreDal();
            int id = dal.Add(model);

            int j = id;

        }

    }
}
