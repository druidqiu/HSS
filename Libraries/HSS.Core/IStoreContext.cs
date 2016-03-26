namespace HSS.Core
{
    /// <summary>
    /// 当前站点
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// 全站启用SSL
        /// </summary>
        bool SslEnable { get;}
    }
}
