using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Services.Localization;
using HSS.Web.Framework.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HSS.Web.Framework
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extensions
    {
        public static IEnumerable<T> PagedForCommand<T>(this IEnumerable<T> current, DataSourceRequest command)
        {
            return current.Skip((command.Page - 1) * command.PageSize).Take(command.PageSize);
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, 
            bool markCurrentAsSelected = true, int[] valuesToExclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("必须是枚举类型", "enumObj");

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new { ID = Convert.ToInt32(enumValue), Name = enumValue.GetLocalizedEnum(localizationService, workContext) };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }
        
        public static string RelativeFormat(this DateTime source)
        {
            return RelativeFormat(source, string.Empty);
        }
        /// <summary>
        /// DateTime相关格式（例如2小时前，一个月前）
        /// </summary>
        /// <param name="source">Source (UTC format)</param>
        /// <param name="defaultFormat">默认格式字符串（在相对格式不适用的情况下）</param>
        /// <returns>Formatted date and time string</returns>
        public static string RelativeFormat(this DateTime source, string defaultFormat)
        {
            return RelativeFormat(source, false, defaultFormat);
        }
        /// <summary>
        /// 日期格式（例如2小时前）
        /// </summary>
        /// <param name="source">Source (UTC format)</param>
        /// <param name="convertToUserTime">转换为DateTime实例</param>
        /// <param name="defaultFormat">默认格式字符串（在相对格式不适用的情况下）</param>
        /// <returns>格式化日期和时间字符串</returns>
        public static string RelativeFormat(this DateTime source,
            bool convertToUserTime, string defaultFormat)
        {
            string result = "";

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            double delta = ts.TotalSeconds;

            if (delta > 0)
            {
                if (delta < 60) // 60 (seconds)
                {
                    result = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                }
                else if (delta < 120) //2 (minutes) * 60 (seconds)
                {
                    result = "a minute ago";
                }
                else if (delta < 2700) // 45 (minutes) * 60 (seconds)
                {
                    result = ts.Minutes + " minutes ago";
                }
                else if (delta < 5400) // 90 (minutes) * 60 (seconds)
                {
                    result = "an hour ago";
                }
                else if (delta < 86400) // 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    int hours = ts.Hours;
                    if (hours == 1)
                        hours = 2;
                    result = hours + " hours ago";
                }
                else if (delta < 172800) // 48 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = "yesterday";
                }
                else if (delta < 2592000) // 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = ts.Days + " days ago";
                }
                else if (delta < 31104000) // 12 (months) * 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    result = months <= 1 ? "one month ago" : months + " months ago";
                }
                else
                {
                    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    result = years <= 1 ? "one year ago" : years + " years ago";
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(defaultFormat))
                {
                    result = source.ToString(defaultFormat);
                }
                else
                {
                    result = source.ToString();
                }
            }
            return result;
        }
    }
}
