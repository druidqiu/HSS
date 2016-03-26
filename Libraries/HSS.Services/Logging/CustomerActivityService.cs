using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Models;
using HSS.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.Logging
{
    /// <summary>
    /// 用户活动日志
    /// </summary>
    public class CustomerActivityService : ICustomerActivityService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string ACTIVITYTYPE_ALL_KEY = "Nop.activitytype.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ACTIVITYTYPE_PATTERN_KEY = "Nop.activitytype.";

        #endregion

        #region Fields

        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ActivityLog> _activityLogRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IWorkContext _workContext;
        private readonly CommonSettings _commonSettings;
        #endregion
        
        #region Ctor
        public CustomerActivityService(ICacheManager cacheManager,
            IRepository<ActivityLog> activityLogRepository,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IWorkContext workContext,
            CommonSettings commonSettings)
        {
            this._cacheManager = cacheManager;
            this._activityLogRepository = activityLogRepository;
            this._activityLogTypeRepository = activityLogTypeRepository;
            this._workContext = workContext;
            this._commonSettings = commonSettings;
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class ActivityLogTypeForCaching
        {
            public int Id { get; set; }
            public string SystemKeyword { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        #endregion

        #region Utitlies

        /// <summary>
        /// Gets all activity log types (class for caching)
        /// </summary>
        /// <returns>Activity log types</returns>
        protected virtual IList<ActivityLogTypeForCaching> GetAllActivityTypesCached()
        {
            //cache
            string key = string.Format(ACTIVITYTYPE_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var result = new List<ActivityLogTypeForCaching>();
                var activityLogTypes = GetAllActivityTypes();
                foreach (var alt in activityLogTypes)
                {
                    var altForCaching = new ActivityLogTypeForCaching
                    {
                        Id = alt.Id,
                        SystemKeyword = alt.SystemKeyword,
                        Name = alt.Name,
                        Enabled = alt.Enabled
                    };
                    result.Add(altForCaching);
                }
                return result;
            });
        }

        #endregion

        #region Methods

        
        public virtual void InsertActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Insert(activityLogType);
            _cacheManager.RemoveByPattern(ACTIVITYTYPE_PATTERN_KEY);
        }

        
        public virtual void UpdateActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Update(activityLogType);
            _cacheManager.RemoveByPattern(ACTIVITYTYPE_PATTERN_KEY);
        }
                
        
        public virtual void DeleteActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Delete(activityLogType);
            _cacheManager.RemoveByPattern(ACTIVITYTYPE_PATTERN_KEY);
        }

        
        public virtual IList<ActivityLogType> GetAllActivityTypes()
        {
            var query = from alt in _activityLogTypeRepository.Table
                orderby alt.Name
                select alt;
            var activityLogTypes = query.ToList();
            return activityLogTypes;
        }

        
        public virtual ActivityLogType GetActivityTypeById(int activityLogTypeId)
        {
            if (activityLogTypeId == 0)
                return null;

            return _activityLogTypeRepository.GetById(activityLogTypeId);
        }

        
        public virtual ActivityLog InsertActivity(string systemKeyword,
            string comment, params object[] commentParams)
        {
            return InsertActivity(systemKeyword, comment, _workContext.CurrentCustomer, commentParams);
        }
        

        
        public virtual ActivityLog InsertActivity(string systemKeyword, 
            string comment, Customer customer, params object[] commentParams)
        {
            if (customer == null)
                return null;

            var activityTypes = GetAllActivityTypesCached();
            var activityType = activityTypes.ToList().Find(at => at.SystemKeyword == systemKeyword);
            if (activityType == null || !activityType.Enabled)
                return null;

            comment = CommonHelper.EnsureNotNull(comment);
            comment = string.Format(comment, commentParams);
            comment = CommonHelper.EnsureMaximumLength(comment, 4000);

            

            var activity = new ActivityLog();
            activity.ActivityLogTypeId = activityType.Id;
            activity.CustomerId = customer.Id;
            activity.Comment = comment;
            activity.CreatedOn = DateTime.Now;

            _activityLogRepository.Insert(activity);

            return activity;
        }
        
        
        public virtual void DeleteActivity(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException("activityLog");

            _activityLogRepository.Delete(activityLog);
        }

        
        public virtual IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, int? customerId = null, int activityLogTypeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _activityLogRepository.Table;
            if (createdOnFrom.HasValue)
                query = query.Where(al => createdOnFrom.Value <= al.CreatedOn);
            if (createdOnTo.HasValue)
                query = query.Where(al => createdOnTo.Value >= al.CreatedOn);
            if (activityLogTypeId > 0)
                query = query.Where(al => activityLogTypeId == al.ActivityLogTypeId);
            if (customerId.HasValue)
                query = query.Where(al => customerId.Value == al.CustomerId);

            query = query.OrderByDescending(al => al.CreatedOn);

            var activityLog = new PagedList<ActivityLog>(query, pageIndex, pageSize);
            return activityLog;
        }
        
        
        public virtual ActivityLog GetActivityById(int activityLogId)
        {
            if (activityLogId == 0)
                return null;

            return _activityLogRepository.GetById(activityLogId);
        }


        public virtual void ClearAllActivities()
        {
            var activityLog = _activityLogRepository.Table.ToList();
            foreach (var activityLogItem in activityLog)
                _activityLogRepository.Delete(activityLogItem);

        }
        #endregion

    }
}
