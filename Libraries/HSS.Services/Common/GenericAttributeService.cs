using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Data;
using HSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.Common
{
    /// <summary>
    /// 通用属性服务实现类
    /// </summary>
    public partial class GenericAttributeService : IGenericAttributeService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : key group
        /// </remarks>
        private const string GENERICATTRIBUTE_KEY = "zh.genericattribute.{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string GENERICATTRIBUTE_PATTERN_KEY = "zh.genericattribute.";
        #endregion

        #region Fields

        private readonly IRepository<GenericAttribute> _genericAttributeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="cacheManager">缓存管理器</param>
        /// <param name="genericAttributeRepository">Generic attribute repository</param>
        public GenericAttributeService(ICacheManager cacheManager,
            IRepository<GenericAttribute> genericAttributeRepository)
        {
            this._cacheManager = cacheManager;
            this._genericAttributeRepository = genericAttributeRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attribute">属性</param>
        public virtual void DeleteAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Delete(attribute);

            //cache
            _cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);
            
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="attributeId">属性主键</param>
        /// <returns>属性</returns>
        public virtual GenericAttribute GetAttributeById(int attributeId)
        {
            if (attributeId == 0)
                return null;

            return _genericAttributeRepository.GetById(attributeId);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="attribute">属性</param>
        public virtual void InsertAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Insert(attribute);

            //cache
            _cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);
            
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="attribute">属性</param>
        public virtual void UpdateAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Update(attribute);

            //cache
            _cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);
            
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="entityId">实体主键</param>
        /// <param name="keyGroup">键组</param>
        /// <returns>属性</returns>
        public virtual IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup)
        {
            string key = string.Format(GENERICATTRIBUTE_KEY, entityId, keyGroup);
            return _cacheManager.Get(key, () =>
            {
                var query = from ga in _genericAttributeRepository.Table
                            where ga.EntityId == entityId &&
                            ga.KeyGroup == keyGroup
                            select ga;
                var attributes = query.ToList();
                return attributes;
            });
        }

        /// <summary>
        /// 保存属性
        /// </summary>
        /// <typeparam name="TPropType">类别属性（反射）</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public virtual void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (key == null)
                throw new ArgumentNullException("key");

            string keyGroup = entity.GetUnproxiedEntityType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    DeleteAttribute(prop);
                }
                else
                {
                    prop.Value = valueStr;
                    UpdateAttribute(prop);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(valueStr))
                {
                    prop = new GenericAttribute
                    {
                        EntityId = entity.Id,
                        Key = key,
                        KeyGroup = keyGroup,
                        Value = valueStr,

                    };
                    InsertAttribute(prop);
                }
            }
        }

        #endregion
    }
}