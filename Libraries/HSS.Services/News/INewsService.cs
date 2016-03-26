using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.News
{
    /// <summary>
    /// 新增服务接口
    /// </summary>
    public partial interface INewsService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="newsItem">新闻实体</param>
        void DeleteNews(NewsItem newsItem);

        /// <summary>
        /// 根据主键获取新闻
        /// </summary>
        /// <param name="newsId">主键</param>
        /// <returns>News</returns>
        NewsItem GetNewsById(int newsId);

        /// <summary>
        /// 获取所有新闻（分页）
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden">是否显示隐藏的值</param>
        /// <returns>News items</returns>
        IPagedList<NewsItem> GetAllNews(string keywords = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="news">新闻实体</param>
        void InsertNews(NewsItem news);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="news">新闻实体</param>
        void UpdateNews(NewsItem news);        

    }
}
