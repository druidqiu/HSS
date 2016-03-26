using HSS.Models;
using System;

namespace HSS.Services.Customers
{
    public static class CustomerExtensions
    {
        /// <summary>
        /// 判断用户是否注册
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>true 注册用户 / 反之 非注册</returns>
        public static bool IsRegistered(this Customer customer)
        {
            if (customer.LoginName == null)
                return false;
            if (customer.LoginName == "")
                return false;
            else return true;
        }
    }
}
