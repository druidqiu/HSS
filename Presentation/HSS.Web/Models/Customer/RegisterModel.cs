using FluentValidation.Attributes;
using HSS.Web.Framework;
using HSS.Web.Framework.Mvc;
using HSS.Web.Validators.Customer;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HSS.Web.Models.Customer
{
    /// <summary>
    /// 注册实体
    /// </summary>
    [Validator(typeof(RegisterValidator))]
    public class RegisterModel : BaseModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [ResourceDisplayName("Account.Fields.LoginName")]
        [AllowHtml]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DataType(DataType.Password)]
        [ResourceDisplayName("Account.Fields.Password")]
        [AllowHtml]
        public string Password { get; set; }

        /// <summary>
        /// 二次密码
        /// </summary>
        [DataType(DataType.Password)]
        [ResourceDisplayName("Account.Fields.ConfirmPassword")]
        [AllowHtml]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [ResourceDisplayName("Account.Fields.Mobile")]
        [AllowHtml]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [ResourceDisplayName("Account.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }
    }
}