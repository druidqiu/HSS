using System.Collections;
using System.Collections.Generic;

namespace HSS.Web.Framework.UI.Paging
{
	/// <summary>
	/// A collection of objects that has been split into pages.
	/// </summary>
	public interface IPageableModel : IEnumerable
    {
        /// <summary>
        /// 当前页面索引（从0开始）
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 当前页号（从1开始）
        /// </summary>
        int PageNumber { get; }
        /// <summary>
        /// 每一页中的项目数
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 项目总数量
        /// </summary>
        int TotalItems { get; }
        /// <summary>
        /// 总的页面数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 页面中第一项的索引
        /// </summary>
        int FirstItem { get; }
        /// <summary>
        /// 页面中最后一项的索引
        /// </summary>
        int LastItem { get; }
        /// <summary>
        /// 是否有页之前的当前页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 当前页面是否有页
        /// </summary>
        bool HasNextPage { get; }
	}


	/// <summary>
	/// 一般形式<see cref="IPageableModel"/>
	/// </summary>
	/// <typeparam name="T">对象分页</typeparam>
	public interface IPagination<T> : IPageableModel, IEnumerable<T>
	{

	}
}