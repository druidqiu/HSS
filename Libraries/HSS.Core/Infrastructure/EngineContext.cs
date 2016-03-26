using System.Runtime.CompilerServices;

namespace HSS.Core.Infrastructure
{
    /// <summary>
    /// 驱动器
    /// </summary>
    public class EngineContext
    {
        #region Utilities

        /// <summary>
        /// 创建一个工厂实例和添加一个HTTP应用注射设备
        /// </summary>
        /// <param name="config">Config</param>
        /// <returns>新的引擎实例</returns>
        protected static IEngine CreateEngineInstance()
        {
            return new Engine();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化工厂静态实例
        /// </summary>
        /// <param name="forceRecreate">创建一个新工厂实例，即使工厂已被初始化</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize()
        {
            if (Singleton<IEngine>.Instance == null)
            {
                Singleton<IEngine>.Instance = CreateEngineInstance();
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// 将静态引擎实例设置为所提供的引擎。使用此方法来提供您的引擎实现
        /// </summary>
        /// <param name="engine">引擎</param>        
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取用于访问服务的单引擎服务
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize();
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
