﻿using System;
using System.IO;
using System.Text;
using System.Collections;

namespace myplat.Util
{
    public class LocalFileIO
    {
        public static bool Exist(string path)
        {
            return File.Exists(path);
        }

        public static string ReadAsUTF8(string filePath)
        {
            if (!Exist(filePath)) return null;

            string data = File.ReadAllText(filePath, Encoding.UTF8);

            return data;
        }

        public static void WriteAsUTF8(string filePath, string utf8content)
        {
            writeToLocal(filePath, utf8content, FileMode.Create);
        }

        public static void AppendAsUTF8(string filePath, string utf8content)
        {
            writeToLocal(filePath, utf8content, FileMode.Append);
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        private static void writeToLocal(string filePath, string utf8content,FileMode mode)
        {
            string folderpath = filePath.Substring(0, filePath.LastIndexOf(@"\") + 1);
            string filename = Path.GetFileName(filePath);

            if (!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);

            FileStream fs = new FileStream(filePath, mode);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(utf8content);
            sw.Flush();
            sw.Close();
            fs.Close();

            
        }

        public static ArrayList GetFiles(string folderpath)
        {
            ArrayList list = null;
            try
            {
                string[] files = Directory.GetFiles(folderpath);
                list = new ArrayList(files);
            }
            catch
            {
            }

            return list;
        }

        public static ArrayList GetFiles(string folderpath, string searchPattern)
        {
            ArrayList list = null;
            try
            {
                string[] files = Directory.GetFiles(folderpath, searchPattern);
                list = new ArrayList(files);
            }
            catch
            {
            }

            return list;
        }

        public static string ApplicationDirectory
        {
            get {
                return Environment.CurrentDirectory;
            }
        }

        public static string DesktopDirectory
        {
            get{
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
        }

        public static string CombinePath(string str1,string str2)
        {
            return Path.Combine(str1, str2);
        }

    }

   

}
