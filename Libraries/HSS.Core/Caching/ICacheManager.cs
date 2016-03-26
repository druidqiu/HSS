namespace HSS.Core.Caching
{
    /// <summary>
    /// 缓存管理器接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 指定的键相关联的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键值对中的键</param>
        /// <returns>关联的值</returns>
        T Get<T>(string key);

        /// <summary>
        /// 指定的键和对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        /// <param name="data">缓存键值对中的值</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 是否缓存与指定密钥相关联的值
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        /// <returns>是否存在</returns>
        bool IsSet(string key);

        /// <summary>
        /// 从缓存中移除制定键的值
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        void Remove(string key);

        /// <summary>
        /// 根据模式移除
        /// </summary>
        /// <param name="pattern">模式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清除缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 总缓存项
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
