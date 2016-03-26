using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HSS.Core;

namespace HSS.Services.Topics
{
    /// <summary>
    /// 主题服务接口
    /// </summary>
    public partial class TopicService : ITopicService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : pageIndex
        /// {1} : pageSize 
        /// {2} : keyword
        /// </remarks>
        private const string TOPICS_ALL_KEY = "zh.topics.search-{0}-{1}-{2}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : topic ID
        /// </remarks>
        private const string TOPICS_BY_ID_KEY = "zh.topics.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string TOPICS_PATTERN_KEY = "zh.topics.";

        #endregion
        
        #region Fields

        private readonly IRepository<Topic> _topicRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public TopicService(IRepository<Topic> topicRepository, 
            ICacheManager cacheManager)
        {
            this._topicRepository = topicRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods
        
        public virtual void DeleteTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            _topicRepository.Delete(topic);

            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);
        }
        
        public virtual Topic GetTopicById(int topicId)
        {
            if (topicId == 0)
                return null;

            string key = string.Format(TOPICS_BY_ID_KEY, topicId);
            return _cacheManager.Get(key, () => _topicRepository.GetById(topicId));
        }

        /// <summary>
        /// Gets a topic
        /// </summary>
        /// <param name="systemName">The topic system name</param>
        /// <param name="storeId">Store identifier; pass 0 to ignore filtering by store and load the first one</param>
        /// <returns>Topic</returns>
        public virtual Topic GetTopicBySystemName(string systemName)
        {
            if (String.IsNullOrEmpty(systemName))
                return null;

            var query = _topicRepository.Table;
            query = query.Where(t => t.SystemName == systemName);
            query = query.OrderBy(t => t.Id);
            var topics = query.ToList();
            return topics.FirstOrDefault();
        }
        
        
        public virtual void InsertTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            _topicRepository.Insert(topic);

            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);
        }
        
        public virtual void UpdateTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            _topicRepository.Update(topic);

            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);
        }
        
        public virtual IPagedList<Topic> GetAllTopics(string keywords = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            string key = "";
            if (!String.IsNullOrWhiteSpace(keywords))
                key = string.Format(TOPICS_ALL_KEY, pageIndex, pageSize, keywords);
            else
                key = string.Format(TOPICS_ALL_KEY, pageIndex, pageSize, "");
            return _cacheManager.Get(key, () => {
                var query = _topicRepository.Table;
                if (!String.IsNullOrWhiteSpace(keywords))
                    query = query.Where(x => x.Title.Contains(keywords));
                
                query = query.OrderByDescending(x => x.DisplayOrder);

                return new PagedList<Topic>(query, pageIndex, pageSize);
            });

        }

        #endregion
    }
}
