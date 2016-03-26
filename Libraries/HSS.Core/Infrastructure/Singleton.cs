using System;
using System.Collections.Generic;

namespace HSS.Core.Infrastructure
{
    /// <summary>
    /// 静态编译的“映射”用于存储对象在整个
    /// </summary>
    /// <typeparam name="T">类型的对象来存储</typeparam>
    public class Singleton<T> : Singleton
    {
        static T instance;

        /// <summary>指定类型的单例实例。
        /// 只有一个实例（当时）为每个类型的对象。</summary>
        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// 提供特定类型的单例列表
    /// </summary>
    /// <typeparam name="T">存储列表的类型</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>指定类型的单例实例
        /// 只有一个实例（按时间），该列表中的每一种类型</summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    /// <summary>
    /// 提供了一个单一的字典一定的键和值的类型
    /// </summary>
    /// <typeparam name="TKey">Key的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }

    /// <summary>
    /// 提供存取所有“单例”存储 <see cref="Singleton{T}"/>.
    /// </summary>
    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> allSingletons;

        /// <summary>Dictionary of type to singleton instances.</summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }
}