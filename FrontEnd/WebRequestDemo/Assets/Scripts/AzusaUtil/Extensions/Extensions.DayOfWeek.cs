using System;

namespace Azusa.Extensions
{
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// <inheritdoc cref="DayOfWeek"/>枚举转字符串（中文）
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ToStringCHN(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Saturday:
                    return "周六";
                case DayOfWeek.Sunday:
                    return "周日";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                default:
                    throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null);
            }
        }
    }
}