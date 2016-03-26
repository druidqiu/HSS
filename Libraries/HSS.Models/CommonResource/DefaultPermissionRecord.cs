using HSS.Models;
using System.Collections.Generic;

namespace HSS.Core.Domain.Security
{
    /// <summary>
    /// 默认权限记录
    /// </summary>
    public class DefaultPermissionRecord
    {
        public DefaultPermissionRecord() 
        {
            this.PermissionRecords = new List<PermissionRecord>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string CustomerRoleSystemName { get; set; }

        /// <summary>
        /// 所属权限
        /// </summary>
        public IEnumerable<PermissionRecord> PermissionRecords { get; set; }
    }
}
