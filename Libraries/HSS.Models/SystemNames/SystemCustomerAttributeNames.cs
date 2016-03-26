namespace HSS.Models.SystemNames
{
    /// <summary>
    /// 用户属性名称
    /// </summary>
    public static partial class SystemCustomerAttributeNames
    {
        public static string ContactName { get { return "ContactName"; } }
        public static string Gender { get { return "Gender"; } }
        public static string DateOfBirth { get { return "DateOfBirth"; } }
        public static string Phone { get { return "Phone"; } }

        #region  其他属性

        public static string PasswordRecoveryToken { get { return "PasswordRecoveryToken"; } }
        public static string PasswordRecoveryTokenDateGenerated { get { return "PasswordRecoveryTokenDateGenerated"; } }


        public static string AccountActivationToken { get { return "AccountActivationToken"; } }

        public static string AvatarPictureId { get { return "AvatarPictureId"; } }
        public static string Signature { get { return "Signature"; } }
        public static string LastVisitedPage { get { return "LastVisitedPage"; } }
        public static string ImpersonatedCustomerId { get { return "ImpersonatedCustomerId"; } }
        public static string AdminAreaStoreScopeConfiguration { get { return "AdminAreaStoreScopeConfiguration"; } }

        /// <summary>
        /// 语言自动检测
        /// </summary>
        public static string LanguageAutomaticallyDetected { get { return "LanguageAutomaticallyDetected"; } }
        /// <summary>
        /// 用户选择的语言
        /// </summary>
        public static string LanguageId { get { return "LanguageId"; } }
        #endregion
    }
}
