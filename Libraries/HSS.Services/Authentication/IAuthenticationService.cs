using HSS.Models;

namespace HSS.Services.Authentication
{
    /// <summary>
    /// �û���֤�ӿ�
    /// </summary>
    public partial interface IAuthenticationService 
    {
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="createPersistentCookie"></param>
        void SignIn(Customer customer, bool createPersistentCookie);
        /// <summary>
        /// �ǳ�
        /// </summary>
        void SignOut();

        Customer GetAuthenticatedCustomer();
    }
}