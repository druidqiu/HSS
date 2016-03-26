using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Topics
{
    /// <summary>
    /// �������ӿ�
    /// </summary>
    public partial interface ITopicService
    {
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="topic">Topic</param>
        void DeleteTopic(Topic topic);

        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="topicId">����</param>
        /// <returns>Topic</returns>
        Topic GetTopicById(int topicId);

        /// <summary>
        /// ����ϵͳ���ƻ�ȡ����
        /// </summary>
        /// <param name="systemName">ϵͳ����</param>
        /// <returns>Topic</returns>
        Topic GetTopicBySystemName(string systemName);

        /// <summary>
        /// ��ȡ��������(��ҳ)
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<Topic> GetAllTopics(string keywords = "", int pageIndex = 0, int pageSize = int.MaxValue);

        

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="topic">Topic</param>
        void InsertTopic(Topic topic);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="topic">Topic</param>
        void UpdateTopic(Topic topic);
    }
}
