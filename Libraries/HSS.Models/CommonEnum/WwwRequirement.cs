namespace HSS.Models.CommonEnum
{
    /// <summary>
    /// WWW要求
    /// </summary>
    public enum WwwRequirement
    {
        /// <summary>
        /// 不处理
        /// </summary>
        NoMatter = 0,
        /// <summary>
        /// URL必须包含WWW
        /// </summary>
        WithWww = 10,
        /// <summary>
        /// 页面不能包含WWW
        /// </summary>
        WithoutWww = 20,
    }
}
