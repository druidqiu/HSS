using HSS.Core.Domain.Common;

namespace HSS.Services.Customers
{
    /// <summary>
    /// 密码要求
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 是否验证请求
        /// </summary>
        public bool ValidateRequest { get; set; }
        /// <summary>
        /// Password format
        /// </summary>
        public PasswordFormat NewPasswordFormat { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// 老密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="validateRequest">A value indicating whether we should validate request</param>
        /// <param name="newPasswordFormat">Password format</param>
        /// <param name="newPassword">New password</param>
        /// <param name="oldPassword">Old password</param>
        public ChangePasswordRequest(string loginName, bool validateRequest, 
            PasswordFormat newPasswordFormat, string newPassword, string oldPassword = "")
        {
            this.LoginName = loginName;
            this.ValidateRequest = validateRequest;
            this.NewPasswordFormat = newPasswordFormat;
            this.NewPassword = newPassword;
            this.OldPassword = oldPassword;
        }
    }
}
