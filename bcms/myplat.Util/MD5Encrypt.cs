using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    public class MD5Encrypt
    {
        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        public static string Md5Encrypt(string str, string salt)
        {
            if (str == null) return null;

            if (!String.IsNullOrWhiteSpace(salt))
                str += salt;

            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }


        /// <summary>
        /// 计算MD5 16位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5Hex(String str)
        {

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            StringBuilder strReturn = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                strReturn.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
            }

            return strReturn.ToString().PadLeft(32, '0');
        }


        public static string Sha512Encrypt(string str, string salt)
        {
            if (str == null) return null;

            if (!String.IsNullOrWhiteSpace(salt))
                str += salt;

            SHA512 s512 = new SHA512Managed();
            byte[] byte1 = s512.ComputeHash(Encoding.Default.GetBytes(str));
            s512.Clear();
            return Convert.ToBase64String(byte1);
        }

        public static string Md5Sha512Encrypt(string str, string salt)
        {
            if (str == null) return null;

            var md5 = Md5Encrypt(str, null);
            var sha512 = Sha512Encrypt(md5, salt);
            return sha512;
        }

        public static string StrToMD5(string str)
        {
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();
        }

        // code 为加密位数，16和32   
        public static string Md5(string str, int code = 32)
        {
            return "";
        }
    }
}
