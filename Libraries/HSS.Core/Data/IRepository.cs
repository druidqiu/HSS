using System.Collections.Generic;
using System.Linq;

namespace HSS.Core.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// 表数据
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 只读数据
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
