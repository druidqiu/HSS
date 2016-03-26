namespace HSS.Models.Configuration
{
    public partial class AdminAreaSettings :ISettings
    {
        /// <summary>
        /// 默认分页根数
        /// </summary>
        public int DefaultGridPageSize { get; set; }
        /// <summary>
        /// 默认分页选项
        /// </summary>
        public string GridPageSizes { get; set; }
    }
}
