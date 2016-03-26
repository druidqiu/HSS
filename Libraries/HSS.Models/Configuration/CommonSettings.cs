namespace HSS.Models.Configuration
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public partial class CommonSettings : ISettings
    {
        /// <summary>
        /// 站点是否启用SSL
        /// </summary>
        public bool SiteSslEnabled { get; set; }        
    }
}
