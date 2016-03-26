
namespace HSS.Core.Domain.Common
{
    /// <summary>
    /// 加密方式
    /// </summary>
    public enum PasswordFormat
    {
        /// <summary>
        /// 不加密
        /// </summary>
        Clear = 0,
        /// <summary>
        /// 散列
        /// </summary>
        Hashed = 1,
        /// <summary>
        /// 加密
        /// </summary>
        Encrypted = 2
    }
}
