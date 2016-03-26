using System.Collections.Generic;

namespace HSS.Core
{
    /// <summary>
    /// 分页接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 页个数
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总个数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 前一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
