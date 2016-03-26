using System.Collections.Generic;

namespace HSS.Models.Configuration
{
    /// <summary>
    /// 安全配置
    /// </summary>
    public partial class SecuritySettings :ISettings
    {
        public SecuritySettings() {
            this.AdminAreaAllowedIpAddresses = new List<string>();
        }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// 是否所有页面将被迫使用SSL
        /// </summary>
        public bool ForceSslForAllPages { get; set; }


        /// <summary>
        /// 系统后台允许访问的IP地址
        /// </summary>
        public List<string> AdminAreaAllowedIpAddresses { get; set; }

        /// <summary>
        /// 是否应启用公用存储XSRF保护
        /// </summary>
        public bool EnableXsrfProtectionForPublicStore { get; set; }



    }
}
