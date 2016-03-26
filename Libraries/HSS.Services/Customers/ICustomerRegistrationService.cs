using HSS.Models;
using HSS.Models.CommonEnum;

namespace HSS.Services.Customers
{
    /// <summary>
    /// 用户注册服务
    /// </summary>
    public partial interface ICustomerRegistrationService
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        CustomerLoginResults ValidateCustomer(string loginName, string password);

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        ChangePasswordResult ChangePassword(ChangePasswordRequest request);
        
    }
}