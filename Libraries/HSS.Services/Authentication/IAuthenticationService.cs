using HSS.Models;

namespace HSS.Services.Authentication
{
    /// <summary>
    /// 用户认证接口
    /// </summary>
    public partial interface IAuthenticationService 
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="createPersistentCookie"></param>
        void SignIn(Customer customer, bool createPersistentCookie);
        /// <summary>
        /// 登出
        /// </summary>
        void SignOut();

        Customer GetAuthenticatedCustomer();
    }
}