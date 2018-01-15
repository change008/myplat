using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    public class DateTimeUtil
    {

        public static DateTime FormatDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }
        public static string FormatDateTime(long timeStamp, string format)
        {
            return FormatDateTime(timeStamp).ToString(format);
        }
        public static string FormatTimeDisplay(Int64 timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return FormatTimeDisplay(dtStart.Add(toNow));
        }

        public static string FormatTimeDisplay(DateTime dtBegin)
        {
            string result = string.Empty;
            TimeSpan ts = DateTime.Now.Subtract(dtBegin);

            int day = ts.Days;
            int hour = ts.Hours;
            int min = ts.Minutes;
            int sec = ts.Seconds;

            if(day == 1)
            {
                result =  "昨天";
            }
            else if(day == 2)
            {
                result =  "前天";
            }
            else if (day > 2 && day < 5)
            {
                result = day + "天前";
            }
            else if (hour > 0)
            {
                result = hour + "小时前";
            }
            else if (min > 0)
            {
                result = min + "分钟前";
            }
            else if (sec > 0)
            {
                result = sec + "秒前";
            }
            else
            {
                result = dtBegin.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return result;

        }
        public static long GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }

        public static long GetTimeStamp(DateTime dt)
        {
            long nowtime = Convert.ToInt64((dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
            return Math.Max(nowtime,0);
        }


        public static string GetNameOfWeek(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Monday) return "星期一";
            if (dt.DayOfWeek == DayOfWeek.Tuesday) return "星期二";
            if (dt.DayOfWeek == DayOfWeek.Wednesday) return "星期三";
            if (dt.DayOfWeek == DayOfWeek.Thursday) return "星期四";
            if (dt.DayOfWeek == DayOfWeek.Friday) return "星期五";
            if (dt.DayOfWeek == DayOfWeek.Saturday) return "星期六";

            return "星期日";
        }

        /// <summary>
        /// 计算两时期 相差天数
        /// </summary>
        public static int IntervalDays(DateTime start, DateTime end)
        {
            TimeSpan x = end - start;
            int days = (int)x.TotalDays;
            return days;
        }

        /// <summary>
        /// 计算两时期 相差分钟
        /// </summary>
        public static int IntervalMinutes(DateTime start, DateTime end)
        {
            TimeSpan x = end - start;
            int minutes = (int)x.TotalMinutes;
            return minutes;
        }

        /// <summary>
        /// 计算两时期 相差秒数
        /// </summary>
        public static int IntervalSeconds(DateTime start, DateTime end)
        {
            TimeSpan x = end - start;
            int seconds = (int)x.TotalSeconds;
            return seconds;
        }

        /// <summary>
        /// 判定是否同一天
        /// </summary>
        public static bool IsSameDay(DateTime day1, DateTime day2)
        {
            return day1.Date == day2.Date;
        }

        /// <summary>
        /// 获取指定日期的凌晨时刻
        /// "2015/9/23 13:57:00" => "2015/9/23 0:00:00"
        /// </summary>
        public static DateTime GetBeforeDawn(DateTime T)
        {
            return T.Date;
        }
    }
}
