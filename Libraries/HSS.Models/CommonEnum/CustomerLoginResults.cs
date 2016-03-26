namespace HSS.Models.CommonEnum
{
    /// <summary>
    /// 用户登录结果枚举
    /// </summary>
    public enum CustomerLoginResults
    {
        /// <summary>
        ///成功
        /// </summary>
        Successful = 1,
        /// <summary>
        /// 用户不存在
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// 密码错误
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// 用户未激活
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// 用户已删除
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// 未注册
        /// </summary>
        NotRegistered = 6,
    }
}
