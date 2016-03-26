using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Security
{
    /// <summary>
    /// 权限服务接口
    /// </summary>
    public partial interface IPermissionService
    {
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="permission">Permission</param>
        void DeletePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="permissionId">Permission 主键</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordById(int permissionId);

        /// <summary>
        /// 根据系统名称获取权限
        /// </summary>
        /// <param name="systemName">权限系统名称</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns>Permissions</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="permission">Permission</param>
        void InsertPermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="permission">Permission</param>
        void UpdatePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 安全权限
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        void InstallPermissions(IPermissionProvider permissionProvider);

        /// <summary>
        /// 卸载权限
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        void UninstallPermissions(IPermissionProvider permissionProvider);

        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - 授权; 否则, false</returns>
        bool Authorize(PermissionRecord permission);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - 授权; 否则, false</returns>
        bool Authorize(PermissionRecord permission, Customer customer);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permissionRecordSystemName">权限记录系统名称</param>
        /// <returns>true - 授权; 否则, false</returns>
        bool Authorize(string permissionRecordSystemName);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permissionRecordSystemName">权限记录系统名称</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - 授权; 否则, false</returns>
        bool Authorize(string permissionRecordSystemName, Customer customer);
    }
}