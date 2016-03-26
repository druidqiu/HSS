using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Data;
using HSS.Models;
using System;
using System.Linq;

namespace HSS.Services.Common
{
    public static class GenericAttributeExtensions
    {
        /// <summary>
        /// ��ȡʵ�������
        /// </summary>
        /// <typeparam name="TPropType">ʵ����������</typeparam>
        /// <param name="entity">ʵ��</param>
        /// <param name="key">Key</param>
        /// <returns>Attribute</returns>
        public static TPropType GetAttribute<TPropType>(this BaseEntity entity, string key)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return GetAttribute<TPropType>(entity, key, genericAttributeService);
        }

        /// <summary>
        /// ��ȡʵ�������
        /// </summary>
        /// <typeparam name="TPropType">ʵ����������</typeparam>
        /// <param name="entity">ʵ��</param>
        /// <param name="key">Key</param>
        /// <param name="genericAttributeService">GenericAttributeService</param>
        /// <returns>Attribute</returns>
        public static TPropType GetAttribute<TPropType>(this BaseEntity entity,
            string key, IGenericAttributeService genericAttributeService)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string keyGroup = entity.GetUnproxiedEntityType().Name;

            var props = genericAttributeService.GetAttributesForEntity(entity.Id, keyGroup);
            if (props == null)
                return default(TPropType);
            if (props.Count == 0)
                return default(TPropType);

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); 

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return default(TPropType);

            return CommonHelper.To<TPropType>(prop.Value);
        }
    }
}
