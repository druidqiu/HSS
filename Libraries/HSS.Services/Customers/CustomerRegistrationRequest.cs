using HSS.Core.Domain.Common;
using HSS.Models;

namespace HSS.Services.Customers
{
    /// <summary>
    /// 注册申请
    /// </summary>
    public class CustomerRegistrationRequest
    {
        /// <summary>
        /// Customer
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password format
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }
        /// <summary>
        /// Is approved
        /// </summary>
        public bool IsApproved { get; set; }
        
        public CustomerRegistrationRequest(Customer customer, string loginName,
            string password, 
            PasswordFormat passwordFormat,
            bool isApproved = true)
        {
            this.Customer = customer;
            this.LoginName = loginName;
            this.Password = password;
            this.PasswordFormat = passwordFormat;
            this.IsApproved = isApproved;
        }
    }
}
