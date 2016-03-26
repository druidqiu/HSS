namespace HSS.Core.Infrastructure
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface IStartupTask 
    {
        /// <summary>
        /// 任务执行
        /// </summary>
        void Execute();
        
        /// <summary>
        /// 任务排序
        /// </summary>
        int Order { get; }
    }
}
