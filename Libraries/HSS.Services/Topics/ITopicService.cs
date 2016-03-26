using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Topics
{
    /// <summary>
    /// 主题服务接口
    /// </summary>
    public partial interface ITopicService
    {
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="topic">Topic</param>
        void DeleteTopic(Topic topic);

        /// <summary>
        /// 根据主键获取主题
        /// </summary>
        /// <param name="topicId">主键</param>
        /// <returns>Topic</returns>
        Topic GetTopicById(int topicId);

        /// <summary>
        /// 根据系统名称获取主题
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <returns>Topic</returns>
        Topic GetTopicBySystemName(string systemName);

        /// <summary>
        /// 获取所有主题(分页)
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<Topic> GetAllTopics(string keywords = "", int pageIndex = 0, int pageSize = int.MaxValue);

        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="topic">Topic</param>
        void InsertTopic(Topic topic);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="topic">Topic</param>
        void UpdateTopic(Topic topic);
    }
}
