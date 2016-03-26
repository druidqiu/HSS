using HSS.Core;
using HSS.Models;
using System;

namespace HSS.Services.Customers
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="entity"></param>
        void DeleteCustomer(Customer entity);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        void UpdateCustomer(Customer entity);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="entity"></param>
        Customer InsertGuestCustomer();

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="customer"></param>
        void InsertCustomer(Customer customer);

        /// <summary>
        /// 获得一个匿名用户
        /// </summary>
        /// <returns></returns>
        Customer GetGuestCustomer();

        /// <summary>
        /// 根据主键获取用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomerById(int customerId);

        Customer GetCustomerByName(string loginName);

        /// <summary>
        /// 根据Guid获取用户
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        Customer GetCustomerByGuid(Guid customerGuid);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <returns></returns>
        IPagedList<Customer> GetAllCustomers();

    }
}
