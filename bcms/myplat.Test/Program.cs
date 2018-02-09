using myplat.Dal;
using myplat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myplat.Biz;

namespace myplat.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //CoreTestAdd();

            uploadimg();
        }

        static void uploadimg()
        {
            QiniuImgBiz q = new QiniuImgBiz();
            string addr = "";
            q.UploadUri("http://www.taopic.com/uploads/allimg/140729/240450-140HZP45790.jpg", out addr);
            string addr1 = "";
            q.UploadUri("https://mmbiz.qpic.cn/mmbiz_jpg/G8vkERUJibks09JnDjMNX42CiaNvmQXv69UfEoF2xbr6EI9qvJNMcaod93LvzoUfVtBP9dLHoU0ZBibJ7sa7zBZgA/640?wx_fmt=jpeg&tp=webp&wxfrom=5&wx_lazy=1", out addr1);
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
