using System.Web;

namespace HSS.Core
{
    /// <summary>
    /// 公共类接口
    /// </summary>
    public partial interface IWebHelper
    {
        /// <summary>
        /// 获取URl链接
        /// </summary>
        /// <returns>URL referrer</returns>
        string GetUrlReferrer();

        /// <summary>
        /// 获取当前Id地址
        /// </summary>
        /// <returns></returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// 获取当前页URL地址
        /// </summary>
        /// <param name="includeQueryString">是否包含查询字符串（扩展参数）</param>
        /// <returns>URL地址</returns>
        string GetThisPageUrl(bool includeQueryString);

        /// <summary>
        /// 获取当前页URL地址
        /// </summary>
        /// <param name="includeQueryString">是否包含查询字符串（扩展参数）</param>
        /// <param name="useSsl">SSL</param>
        /// <returns>URL地址</returns>
        string GetThisPageUrl(bool includeQueryString, bool useSsl);

        /// <summary>
        /// 是否安全链接
        /// </summary>
        /// <returns>True - 安全/ False 非安全</returns>
        bool IsCurrentConnectionSecured();

        /// <summary>
        /// 获取服务器变量名称
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>服务器变量名</returns>
        string ServerVariables(string name);

        /// <summary>
        /// 获取当前站点HOST
        /// </summary>
        /// <param name="useSsl">是否引用SSL</param>
        /// <returns>站点HOST</returns>
        string GetStoreHost(bool useSsl);

        /// <summary>
        /// 获取站点地址
        /// </summary>
        /// <returns>站点地址</returns>
        string GetStoreLocation();

        /// <summary>
        /// 获取站点地址
        /// </summary>
        /// <param name="useSsl">是否引用SSL</param>
        /// <returns>站点地址</returns>
        string GetStoreLocation(bool useSsl);

        /// <summary>
        /// 如果请求的资源是不需要处理的典型资源，则返回真
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>静态资源</returns>
        /// <remarks>
        /// 静态资源扩展名:
        /// .css
        ///	.gif
        /// .png 
        /// .jpg
        /// .jpeg
        /// .js
        /// .axd
        /// .ashx
        /// </remarks>
        bool IsStaticResource(HttpRequest request);

        /// <summary>
        /// 虚拟路径映射到物理路径
        /// </summary>
        /// <param name="path">虚拟路径 例如:"~/Bin/"</param>
        /// <returns>物理路径 E:/Path</returns>
        string MapPath(string path);


        /// <summary>
        /// 修改查询字符串
        /// </summary>
        /// <param name="url">需要修改的字符串</param>
        /// <param name="queryStringModification">修改查询字符串</param>
        /// <param name="anchor">Anchor</param>
        /// <returns>新的URL</returns>
        string ModifyQueryString(string url, string queryStringModification, string anchor);
        
        /// <summary>
        /// URL移除参数
        /// </summary>
        /// <param name="url">需要修改的字符串</param>
        /// <param name="queryString">需要移除的参数</param>
        /// <returns>新的URL</returns>
        string RemoveQueryString(string url, string queryString);
        
        /// <summary>
        /// 重启应用程序
        /// </summary>
        /// <param name="makeRedirect">是否在重新启动后重新进行重定向</param>
        /// <param name="redirectUrl">如果你想重定向到当前网页的网址</param>
        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "");

        /// <summary>
        /// 是否将用户重定向到新的位置
        /// </summary>
        bool IsRequestBeingRedirected { get; }

        /// <summary>
        /// 是否将用户重定向到一个新的位置
        /// </summary>
        bool IsPostBeingDone { get; set; }
    }
}
