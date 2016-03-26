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
        /// ��ǰҳ����������0��ʼ��
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// ��ǰҳ�ţ���1��ʼ��
        /// </summary>
        int PageNumber { get; }
        /// <summary>
        /// ÿһҳ�е���Ŀ��
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// ��Ŀ������
        /// </summary>
        int TotalItems { get; }
        /// <summary>
        /// �ܵ�ҳ����
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// ҳ���е�һ�������
        /// </summary>
        int FirstItem { get; }
        /// <summary>
        /// ҳ�������һ�������
        /// </summary>
        int LastItem { get; }
        /// <summary>
        /// �Ƿ���ҳ֮ǰ�ĵ�ǰҳ
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// ��ǰҳ���Ƿ���ҳ
        /// </summary>
        bool HasNextPage { get; }
	}


	/// <summary>
	/// һ����ʽ<see cref="IPageableModel"/>
	/// </summary>
	/// <typeparam name="T">�����ҳ</typeparam>
	public interface IPagination<T> : IPageableModel, IEnumerable<T>
	{

	}
}