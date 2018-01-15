using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    /// <summary>
    /// 日期时间扩展方法
    /// </summary>
    public static class DateExtension
    {
        public static long ToTimestamp(this DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static DateTime ToDateTime(this long timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 获取当前分钟数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetTotalMinutes(this DateTime dt)
        {
            return dt.Hour * 60 + dt.Minute;
        }
        public static bool IsWeekend(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday;
        }

        /// <summary>
        /// 时间精确到小时
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeAccurateHour(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
        }

        /// <summary>
        /// 时间精确到天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeAccurateDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }
    }
}
