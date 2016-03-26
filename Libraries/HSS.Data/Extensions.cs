using HSS.Models;
using System;
using System.Data.Entity.Core.Objects;

namespace HSS.Data
{
    public static class Extensions
    {
        /// <summary>
        /// 反射实体类型
        /// </summary>
        /// </remarks>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Type GetUnproxiedEntityType(this BaseEntity entity)
        {
            var userType = ObjectContext.GetObjectType(entity.type);
            return userType;
        }
    }
}
