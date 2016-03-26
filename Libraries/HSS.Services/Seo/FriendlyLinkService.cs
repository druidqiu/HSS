using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.Seo
{
    public partial class FriendlyLinkService :IFriendlyLinkService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system Name
        /// </remarks>
        private const string FRIEND_SYSTEM = "zh.friendlylink.system-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string URLRECORD_PATTERN_KEY = "zh.friendlylink.";

        #endregion

        #region Fields

        private readonly IRepository<FriendlyLink> _linkRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        
        public FriendlyLinkService(ICacheManager cacheManager,
            IRepository<FriendlyLink> linkRepository)
        {
            this._cacheManager = cacheManager;
            this._linkRepository = linkRepository;
        }

        #endregion

        #region Method

        public virtual void DeleteLink(FriendlyLink link)
        {
            if (link == null)
                throw new Exception("FriendlyLink");

            _linkRepository.Delete(link);

            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        public virtual void InsertLink(FriendlyLink link)
        {
            if (link == null)
                throw new Exception("FriendlyLink");

            _linkRepository.Insert(link);

            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        public virtual void UpdateLink(FriendlyLink link)
        {
            if(link == null)
                throw new Exception("FriendlyLink");

            _linkRepository.Update(link);

            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        public virtual FriendlyLink GetLinkById(int id)
        {
            if (id == 0)
                throw new Exception("link id ");

            return _linkRepository.GetById(id);
        }

        public virtual IList<FriendlyLink> GetLinkBySystemName(string systemName, bool showHidden = false)
        {
            if (!String.IsNullOrWhiteSpace(systemName))
                return null;

            string key = string.Format(FRIEND_SYSTEM, systemName);

            return _cacheManager.Get(key, () =>
            {
                var query = _linkRepository.Table;
                query = query.Where(x => x.SystemName == systemName);

                query = query.Where(x => x.Deleted || showHidden);
                return query.ToList();
            });
        }

        public virtual IPagedList<FriendlyLink> GetAllLinks(bool? showImage = null, 
            string name = "", bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _linkRepository.Table;
            
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name) || x.SystemName.Contains(name));

            if (showImage.HasValue)
                query = query.Where(x => x.ShowImage || showImage.Value);

            if (!showHidden)
                query = query.Where(x => x.Deleted);


            query = query.OrderByDescending(x => x.Id);

            return new PagedList<FriendlyLink>(query, pageIndex, pageSize);
        }

        #endregion
    }
}
