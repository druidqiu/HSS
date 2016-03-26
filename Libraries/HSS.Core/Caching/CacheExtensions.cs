using System;

namespace HSS.Core.Caching
{
    /// <summary>
    /// 缓存扩展类
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 可变（锁），以支持线程安全
        /// </summary>
        private static readonly object _syncObject = new object();

        /// <summary>
        /// 获取缓存项目。如果不是在缓存中，然后加载和缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheManager">缓存管理器</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">未缓存的数据中加载此项</param>
        /// <returns>缓存项</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// 获取缓存项目。如果不是在缓存中，然后加载和缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheManager">缓存管理器</param>
        /// <param name="key">缓存键</param>
        /// <param name="cacheTime">缓存时间（0 -不缓存）</param>
        /// <param name="acquire">未缓存的数据中加载此项</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            lock (_syncObject)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
    }
}