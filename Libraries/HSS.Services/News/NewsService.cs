using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.News
{
    /// <summary>
    /// 新闻服务实现类
    /// </summary>
    public partial class NewsService : INewsService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : pageIndex
        /// {1} : pageSize 
        /// {2} : keyword
        /// {3} : showHidden
        /// </remarks>
        private const string NEWS_ALL_KEY = "zh.news.search-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : topic ID
        /// </remarks>
        private const string NEWS_BY_ID_KEY = "zh.news.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string NEWS_PATTERN_KEY = "zh.news.";

        #endregion

        #region Fields

        private readonly IRepository<NewsItem> _newsItemRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public NewsService(IRepository<NewsItem> newsItemRepository, ICacheManager cacheManager)
        {
            this._newsItemRepository = newsItemRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        
        public virtual void DeleteNews(NewsItem newsItem)
        {
            if (newsItem == null)
                throw new ArgumentNullException("newsItem");

            _newsItemRepository.Delete(newsItem);

            _cacheManager.RemoveByPattern(NEWS_PATTERN_KEY);

        }

        
        public virtual NewsItem GetNewsById(int newsId)
        {
            if (newsId == 0)
                return null;

            return _cacheManager.Get(string.Format(NEWS_BY_ID_KEY, newsId),
                () => _newsItemRepository.GetById(newsId));
        }
        
        public virtual IPagedList<NewsItem> GetAllNews(string keywords= "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            string key = "";
            if (!String.IsNullOrWhiteSpace(keywords))
                key = string.Format(NEWS_ALL_KEY, pageIndex, pageSize, keywords, showHidden);
            else
                key = string.Format(NEWS_ALL_KEY, pageIndex, pageSize, "", showHidden);

            return _cacheManager.Get(key, () => {
                var query = _newsItemRepository.Table;
                if (!String.IsNullOrWhiteSpace(keywords))
                    query = query.Where(x => x.Title.Contains(keywords));

                query = query.Where(x => x.Published || showHidden);

                query = query.OrderByDescending(x => x.CreatedOn);

                return new PagedList<NewsItem>(query, pageIndex, pageSize);
            });

        }

        
        public virtual void InsertNews(NewsItem news)
        {
            if (news == null)
                throw new ArgumentNullException("news");

            _newsItemRepository.Insert(news);

            _cacheManager.RemoveByPattern(NEWS_PATTERN_KEY);
        }
        
        public virtual void UpdateNews(NewsItem news)
        {
            if (news == null)
                throw new ArgumentNullException("news");

            _newsItemRepository.Update(news);

            _cacheManager.RemoveByPattern(NEWS_PATTERN_KEY);
        }
        
        #endregion
    }
}
