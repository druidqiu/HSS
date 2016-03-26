using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.News
{
    /// <summary>
    /// ��������ӿ�
    /// </summary>
    public partial interface INewsService
    {
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="newsItem">����ʵ��</param>
        void DeleteNews(NewsItem newsItem);

        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="newsId">����</param>
        /// <returns>News</returns>
        NewsItem GetNewsById(int newsId);

        /// <summary>
        /// ��ȡ�������ţ���ҳ��
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden">�Ƿ���ʾ���ص�ֵ</param>
        /// <returns>News items</returns>
        IPagedList<NewsItem> GetAllNews(string keywords = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="news">����ʵ��</param>
        void InsertNews(NewsItem news);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="news">����ʵ��</param>
        void UpdateNews(NewsItem news);        

    }
}
