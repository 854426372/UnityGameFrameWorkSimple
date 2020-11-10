#region  
//=====================================================
// 文件名: ReferencePool                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 14:05:18              	
// Email: 854426372@qq.com               
// 描述: 引用池类，负责管理所有的RefenceCollection      
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork {
    public static class ReferencePool
    {
        /// <summary>
        /// 引用集合的字典
        /// </summary>
        private static Dictionary<string, ReferenceCollection> s_ReferenceCollections = new Dictionary<string, ReferenceCollection>();

        /// <summary>
        /// 获取引用池的数量
        /// </summary>
        public static int Count
        {
            get
            {
                return s_ReferenceCollections.Count;
            }
        }

        /// <summary>
        /// 获取引用集合
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private static ReferenceCollection GetReferenceCollection(string fullName)
        {
            ReferenceCollection referenceCollection = null;
            lock (s_ReferenceCollections)
            {
                if (!s_ReferenceCollections.TryGetValue(fullName,out referenceCollection))
                {
                    referenceCollection = new ReferenceCollection();
                    s_ReferenceCollections.Add(fullName, referenceCollection);
                }
            }
            return referenceCollection;
        }

        /// <summary>
        /// 清除所有引用集合
        /// </summary>
        public static void ClearAll()
        {
            lock (s_ReferenceCollections)
            {
                foreach (KeyValuePair<string,ReferenceCollection> item in s_ReferenceCollections)
                {
                    item.Value.RemoveAll();
                }
            }
            s_ReferenceCollections.Clear();
        }

        /// <summary>
        /// 向引用集合中追加指定数量的引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public static void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T).FullName).Add<T>(count);
        }

        /// <summary>
        /// 从引用集合中移除指定数量的引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public static void Remove<T>(int count)where T:class, IReference
        {
            GetReferenceCollection(typeof(T).FullName).Remove<T>(count);
        }

        /// <summary>
        /// 从引用集合中移除指定数量的引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RemoveAll<T>() where T:class, IReference
        {
            GetReferenceCollection(typeof(T).FullName).RemoveAll();
        }

        /// <summary>
        /// 从引用集合获取引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Acquire<T>()where T :class, IReference, new()
        {
            return GetReferenceCollection(typeof(T).FullName).Acquire<T>();
        }

        /// <summary>
        /// 将引用归还引用集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        public static void Release<T>(T reference)where T :class, IReference
        {
            if (reference == null)
            {
                Debug.LogError("要归还的引用为空！");
            }
            GetReferenceCollection(typeof(T).FullName).Release(reference);
        }


    }
}