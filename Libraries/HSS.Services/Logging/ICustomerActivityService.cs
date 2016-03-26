using HSS.Core;
using HSS.Models;
using System;
using System.Collections.Generic;

namespace HSS.Services.Logging
{
    /// <summary>
    /// 用户活动日志接口
    /// </summary>
    public partial interface ICustomerActivityService
    {
        /// <summary>
        /// 新增类型
        /// </summary>
        /// <param name="activityLogType">日志类型</param>
        void InsertActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// 更新类型
        /// </summary>
        /// <param name="activityLogType">日志类型</param>
        void UpdateActivityType(ActivityLogType activityLogType);
                
        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="activityLogType">日志类型</param>
        void DeleteActivityType(ActivityLogType activityLogType);
        
        /// <summary>
        /// 获取所有类型
        /// </summary>
        IList<ActivityLogType> GetAllActivityTypes();
        
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="activityLogTypeId">类型主键</param>
        ActivityLogType GetActivityTypeById(int activityLogTypeId);
        
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        ActivityLog InsertActivity(string systemKeyword, string comment, params object[] commentParams);

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="customer">The customer</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        ActivityLog InsertActivity(string systemKeyword, 
            string comment, Customer customer, params object[] commentParams);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="activityLog">Activity log</param>
        void DeleteActivity(ActivityLog activityLog);

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; null to load all customers</param>
        /// <param name="createdOnTo">Log item creation to; null to load all customers</param>
        /// <param name="customerId">Customer identifier; null to load all customers</param>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Activity log items</returns>
        IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, int? customerId = null, int activityLogTypeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue);
        
        /// <summary>
        /// 根据主键获取日志
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        ActivityLog GetActivityById(int activityLogId);

        /// <summary>
        /// Clears activity log
        /// </summary>
        void ClearAllActivities();
    }
}
