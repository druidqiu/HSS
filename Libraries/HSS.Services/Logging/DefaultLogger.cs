using HSS.Core;
using HSS.Core.Data;
using HSS.Models;
using HSS.Models.CommonEnum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.Logging
{
    /// <summary>
    /// Default logger
    /// </summary>
    public partial class DefaultLogger : ILogger
    {
        #region Fields

        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor
        
        public DefaultLogger(IRepository<Log> logRepository,
            IWebHelper webHelper)
        {
            this._logRepository = logRepository;
            this._webHelper = webHelper;
        }

        #endregion
        
        #region Methods
        
        public virtual bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }
        
        public virtual void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Delete(log);
        }
        
        public virtual void ClearLog()
        {
                        
        }
        
        public virtual IPagedList<Log> GetAllLogs(DateTime? from = null, DateTime? to = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _logRepository.Table;
            if (from.HasValue)
                query = query.Where(l => from.Value <= l.CreatedOn);
            if (to.HasValue)
                query = query.Where(l => to.Value >= l.CreatedOn);
            if (logLevel.HasValue)
            {
                var logLevelId = (int)logLevel.Value;
                query = query.Where(l => logLevelId == l.LogLevelId);
            }
            if (!String.IsNullOrEmpty(message))
                query = query.Where(l => l.ShortMessage.Contains(message) || l.FullMessage.Contains(message));
            query = query.OrderByDescending(l => l.CreatedOn);

            var log = new PagedList<Log>(query, pageIndex, pageSize);
            return log;
        }
        
        public virtual Log GetLogById(int logId)
        {
            if (logId == 0)
                return null;

            return _logRepository.GetById(logId);
        }
        
        public virtual IList<Log> GetLogByIds(int[] logIds)
        {
            if (logIds == null || logIds.Length == 0)
                return new List<Log>();

            var query = from l in _logRepository.Table
                        where logIds.Contains(l.Id)
                        select l;
            var logItems = query.ToList();

            var sortedLogItems = new List<Log>();
            foreach (int id in logIds)
            {
                var log = logItems.Find(x => x.Id == id);
                if (log != null)
                    sortedLogItems.Add(log);
            }
            return sortedLogItems;
        }
        
        public virtual Log InsertLog(LogLevel logLevel, string shortMessage, LogSource source = LogSource.Web, string fullMessage = "", int? customerId = null)
        {
            var log = new Log
            {
                LogLevelId = (int)logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                CustomerId = customerId.HasValue ? customerId.Value : 0,
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOn = DateTime.Now,
                Source = (int)source,
            };

            _logRepository.Insert(log);

            return log;
        }

        #endregion
    }
}
