using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Seo
{
    /// <summary>
    /// 友情链接服务接口
    /// </summary>
    public interface IFriendlyLinkService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="link"></param>
        void DeleteLink(FriendlyLink link);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="link"></param>
        void InsertLink(FriendlyLink link);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="link"></param>
        void UpdateLink(FriendlyLink link);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FriendlyLink GetLinkById(int id);

        /// <summary>
        /// 根据系统名称获取友情链接集合
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        IList<FriendlyLink> GetLinkBySystemName(string systemName, bool showHidden = false);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="showImage"></param>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<FriendlyLink> GetAllLinks(bool? showImage = null, string name = "", bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
