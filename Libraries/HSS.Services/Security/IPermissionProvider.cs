using HSS.Core.Domain.Security;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Security
{
    /// <summary>
    /// 权限提供者
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns>Permissions</returns>
        IEnumerable<PermissionRecord> GetPermissions();

        /// <summary>
        /// 默认获取权限
        /// </summary>
        /// <returns>Default permissions</returns>
        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}
