using HSS.Core.Domain.Common;

namespace HSS.Models.Configuration
{
    public partial class CustomerSettings: ISettings
    {
        /// <summary>
        /// 是否记录最后访问页面
        /// </summary>
        public bool StoreLastVisitedPage { get; set; }

        /// <summary>
        /// 访问轨迹
        /// </summary>
        public bool AccessPathEnabled { get; set; }

        /// <summary>
        /// 是否允许更改用户名
        /// </summary>
        public bool AllowUsersToChangeUsernames { get; set; }

        /// <summary>
        /// 用户名最小长度
        /// </summary>
        public int UsernameMinLength { get; set; }

        /// <summary>
        /// 用户名最大长度
        /// </summary>
        public int UsernameMaxLenght { get; set; }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// 用户密码格式（SHA1，MD5）散列情况下
        /// </summary>
        public string HashedPasswordFormat { get; set; }

        /// <summary>
        /// 默认加密方式
        /// </summary>
        public PasswordFormat DefaultPasswordFormat { get; set; }
    }
}
