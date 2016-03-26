using HSS.Core.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HSS.Core
{

    /// <summary>
    /// 公共帮助类
    /// </summary>
    public partial class CommonHelper
    {
        /// <summary>
        /// 确保用户电子邮件正确
        /// </summary>
        /// <param name="email">需要验证的Email</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new Exception("Email无效");
            }

            return output;
        }

        /// <summary>
        /// 验证字符串是有效的电子邮件格式
        /// </summary>
        /// <param name="email">需要验证的Email</param>
        /// <returns>True 有效Email / False 非有效Email</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            if (String.IsNullOrEmpty(mobile))
                return false;

            mobile = mobile.Trim();
            var result = Regex.IsMatch(mobile, "/^(13(4-8))[0-9]{8}$/", RegexOptions.IgnoreCase);
            return result;


        }

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>返回字符串</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// 返回一个随机数值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// 字符不允许超过最大长度
        /// </summary>
        /// <param name="str">验证的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="postfix">如果原字符串缩短，将添加到该字符串的字符串</param>
        /// <returns>超过长度截取的字符串</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        /// <summary>
        /// 确保字符串只包含数字值
        /// </summary>
        /// <param name="str">验证的字符串</param>
        /// <returns>数值类字符串 / 空字符串</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// 字符串不为Null
        /// </summary>
        /// <param name="str">验证的字符串</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// 指示指定的字符串是否为空或空字符串
        /// </summary>
        /// <param name="stringsToValidate">字符串数组来验证</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str => {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        /// <summary>
        /// 对象比较
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="a1">对象 1</param>
        /// <param name="a2">对象 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// 查找运行应用程序的信任级别
        /// </summary>
        /// <returns>当前信任级别</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //最小
                _trustLevel = AspNetHostingPermissionLevel.None;

                //最大
                foreach (AspNetHostingPermissionLevel trustLevel in new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break;
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }


        /// <summary>
        /// 转换枚举
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }
        public static TypeConverter GetNopCustomTypeConverter(Type type)
        {
            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">转换的值</param>
        /// <param name="destinationType">目标类型</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">转换的值</param>
        /// <param name="destinationType">目标类型</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetNopCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetNopCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">转换的值</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>转换后的值</returns>
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }
    }
}
