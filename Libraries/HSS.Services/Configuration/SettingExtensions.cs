using System;
using System.Linq.Expressions;
using System.Reflection;
using HSS.Models.Configuration;

namespace HSS.Services.Configuration
{
    public static class SettingExtensions
    {
        public static string GetSettingKey<T, TPropType>(this T entity,
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "表达式 '{0}' 是一个方法, 并非属性",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "表达式 '{0}' 是一个字段, 并非属性",
                       keySelector));
            }

            var key = typeof(T).Name + "." + propInfo.Name;
            return key;
        }
    }
}
