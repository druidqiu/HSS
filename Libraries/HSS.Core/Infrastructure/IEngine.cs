using HSS.Core.Infrastructure.DependencyManagement;
using System;

namespace HSS.Core.Infrastructure
{
    /// <summary>
    /// 启动器
    /// </summary>
    public interface IEngine
    {

        /// <summary>
        /// 初始化环境和组件
        /// </summary>
        void Initialize();

        /// <summary>
        /// 依赖寄存器管理器
        /// </summary>
        ContainerManager ContainerManager { get; }
        

        /// <summary>
        /// 映射依赖关系
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// 映射依赖关系
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 映射依赖关系集合
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }
}
