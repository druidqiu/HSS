using HSS.Core;
using HSS.Models;
using HSS.Models.CommonEnum;
using System;
using System.Collections.Generic;

namespace HSS.Services.Logging
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public partial interface ILogger
    {
        /// <summary>
        /// 日志级别是否启用
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="log">Log item</param>
        void DeleteLog(Log log);

        /// <summary>
        /// 清除日志
        /// </summary>
        void ClearLog();

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="from">起始日期</param>
        /// <param name="to">结束日期</param>
        /// <param name="message">Message</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Log item items</returns>
        IPagedList<Log> GetAllLogs(DateTime? from = null, DateTime? to = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        Log GetLogById(int logId);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        IList<Log> GetLogByIds(int[] logIds);

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="shortMessage">短消息</param>
        /// <param name="source">日志来源</param>
        /// <param name="fullMessage">完整说明</param>
        /// <param name="customerId">日志操作人</param>
        Log InsertLog(LogLevel logLevel, string shortMessage, LogSource source = LogSource.Web, string fullMessage = "", int? customerId = null);
    }
}
