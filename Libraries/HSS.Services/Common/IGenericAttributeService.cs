using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Common
{
    /// <summary>
    /// 通用属性接口
    /// </summary>
    public partial interface IGenericAttributeService 
    {
        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attribute"></param>
        void DeleteAttribute(GenericAttribute attribute);

        /// <summary>
        /// 根据主键获取属性
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        GenericAttribute GetAttributeById(int attributeId);

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="attribute"></param>
        void InsertAttribute(GenericAttribute attribute);

        /// <summary>
        /// 更新树形
        /// </summary>
        /// <param name="attribute"></param>
        void UpdateAttribute(GenericAttribute attribute);

        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="keyGroup"></param>
        /// <returns></returns>
        IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup);

        /// <summary>
        /// 存储属性
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value);
    }

}