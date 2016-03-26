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
                    "����ʽ '{0}' ��һ������, ��������",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "����ʽ '{0}' ��һ���ֶ�, ��������",
                       keySelector));
            }

            var key = typeof(T).Name + "." + propInfo.Name;
            return key;
        }
    }
}