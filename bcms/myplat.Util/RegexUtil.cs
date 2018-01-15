using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    /// <summary>
    /// 正则验证
    /// </summary>
    public class RegexUtil
    {

        /// <summary>
        /// 验证电话号码
        /// </summary>
        public static bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        public static bool IsHandset(string str_handset)
        {
            //验证手机号逻辑
            if (string.IsNullOrEmpty(str_handset) || str_handset.Length != 11)
            {
                return false;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^1\d{10}$");
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        public static bool IsIDcard(string str_idcard)
        {
            //此方法有缺陷，用到的时候联系小强进行完善
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }

        /// <summary>
        /// 验证输入为数字
        /// </summary>
        public static bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]+$");
        }

        /// <summary>
        /// 验证邮编
        /// </summary>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="str_Email"></param>
        /// <returns></returns>
        public static bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检查字符串是否包含特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true|false 包含|不包含</returns>
        public static bool CheckSpecialCharacters(string str){
            return System.Text.RegularExpressions.Regex.IsMatch(str, "[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&;—|{}【】‘；：”“'。，、？]");
        }
    }
}
