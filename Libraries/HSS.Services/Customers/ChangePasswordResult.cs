using System.Collections.Generic;

namespace HSS.Services.Customers 
{
    /// <summary>
    /// 更改密码反馈
    /// </summary>
    public class ChangePasswordResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ChangePasswordResult() 
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// 是否已成功完成请求
        /// </summary>
        public bool Success
        {
            get { return (this.Errors.Count == 0); }
        }

        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error) 
        {
            this.Errors.Add(error);
        }

        /// <summary>
        /// 错误集合
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
