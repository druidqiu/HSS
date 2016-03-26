using HSS.Models;
using HSS.Models.CommonEnum;

namespace HSS.Core
{
    /// <summary>
    /// 工作引擎
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        Customer CurrentCustomer { get; set; }

        /// <summary>
        /// 当前语言工作环境
        /// </summary>
        Language WorkingLanguage { get; set; }

        /// <summary>
        /// 引擎来源
        /// </summary>
        LogSource?  Source { get; set; }

    }
}
