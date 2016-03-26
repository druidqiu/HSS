using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Security
{
    /// <summary>
    /// Ȩ�޷���ӿ�
    /// </summary>
    public partial interface IPermissionService
    {
        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="permission">Permission</param>
        void DeletePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// ��ȡȨ��
        /// </summary>
        /// <param name="permissionId">Permission ����</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordById(int permissionId);

        /// <summary>
        /// ����ϵͳ���ƻ�ȡȨ��
        /// </summary>
        /// <param name="systemName">Ȩ��ϵͳ����</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        /// <summary>
        /// ��ȡ����Ȩ��
        /// </summary>
        /// <returns>Permissions</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="permission">Permission</param>
        void InsertPermissionRecord(PermissionRecord permission);

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="permission">Permission</param>
        void UpdatePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// ��ȫȨ��
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        void InstallPermissions(IPermissionProvider permissionProvider);

        /// <summary>
        /// ж��Ȩ��
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        void UninstallPermissions(IPermissionProvider permissionProvider);

        /// <summary>
        /// Ȩ����֤
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - ��Ȩ; ����, false</returns>
        bool Authorize(PermissionRecord permission);

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - ��Ȩ; ����, false</returns>
        bool Authorize(PermissionRecord permission, Customer customer);

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        /// <param name="permissionRecordSystemName">Ȩ�޼�¼ϵͳ����</param>
        /// <returns>true - ��Ȩ; ����, false</returns>
        bool Authorize(string permissionRecordSystemName);

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        /// <param name="permissionRecordSystemName">Ȩ�޼�¼ϵͳ����</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - ��Ȩ; ����, false</returns>
        bool Authorize(string permissionRecordSystemName, Customer customer);
    }
}