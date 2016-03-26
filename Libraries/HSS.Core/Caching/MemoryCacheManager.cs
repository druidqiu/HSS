using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace HSS.Core.Caching
{
    /// <summary>
    /// 是一个HTTP请求缓存之间的管理器（长期缓存）
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// 指定的键相关联的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键值对中的键</param>
        /// <returns>指定的键关联的值</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// 指定的键和对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        /// <param name="data">缓存键值对中的值</param>
        /// <param name="cacheTime">缓存时间</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            if (cacheTime == 0)
                policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(365);
            else
                policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 是否缓存与指定密钥相关联的值
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        /// <returns>是否存在</returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// 从缓存中移除制定键的值
        /// </summary>
        /// <param name="key">缓存键值对中的键</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 根据模式移除
        /// </summary>
        /// <param name="pattern">模式</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        public virtual long Count()
        {
            return Cache.GetCount();
        }
    }
}
