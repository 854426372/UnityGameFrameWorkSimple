#region  
//=====================================================
// 文件名: ReferenceCollection                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 14:05:09              	
// Email: 854426372@qq.com               
// 描述:  引用集合类，负责管理实现了IReference的对象
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork
{
    public class ReferenceCollection : MonoBehaviour
    {
        /// <summary>
        /// 引用队列
        /// </summary>
        private Queue<IReference> m_References;

        public ReferenceCollection()
        {
            m_References = new Queue<IReference>();
        }

        /// <summary>
        /// 获取引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Acquire<T>() where T :class ,IReference,new()
        {
            lock (m_References)
            {
                if (m_References.Count > 0 )
                {
                    return m_References.Dequeue() as T;
                }
            }

            return new T();
        }

        /// <summary>
        /// 获取引用
        /// </summary>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        public IReference Acquire(Type referenceType)
        {
            lock (m_References)
            {
                if (m_References.Count > 0 )
                {
                    return m_References.Dequeue();
                }
            }
            return (IReference)Activator.CreateInstance(referenceType);
        }

        /// <summary>
        /// 释放引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        public void Release<T>(T reference) where T:class, IReference
        {
            reference.Clear();
            lock (m_References)
            {
                m_References.Enqueue(reference);
            }
        }

        /// <summary>
        /// 添加引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void Add<T>(int count)where T :class, IReference, new()
        {
            lock (m_References)
            {
                while (count -- >0)
                {
                    m_References.Enqueue(new T());
                }
            }
        }

        /// <summary>
        /// 删除引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void Remove<T>(int count) where T:class, IReference
        {
            lock (m_References)
            {
                if (count > m_References.Count)
                {
                    count = m_References.Count;
                }
                while (count-- >0)
                {
                    m_References.Dequeue();
                }
            }
        }

        /// <summary>
        /// 删除所有的引用
        /// </summary>
        public void RemoveAll()
        {
            lock (m_References)
            {
                m_References.Clear();
            }
        }
    }
}