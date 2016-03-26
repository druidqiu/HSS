using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Common
{
    /// <summary>
    /// ͨ�����Խӿ�
    /// </summary>
    public partial interface IGenericAttributeService 
    {
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="attribute"></param>
        void DeleteAttribute(GenericAttribute attribute);

        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        GenericAttribute GetAttributeById(int attributeId);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="attribute"></param>
        void InsertAttribute(GenericAttribute attribute);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="attribute"></param>
        void UpdateAttribute(GenericAttribute attribute);

        /// <summary>
        /// ��ȡ���Լ���
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="keyGroup"></param>
        /// <returns></returns>
        IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup);

        /// <summary>
        /// �洢����
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value);
    }

}