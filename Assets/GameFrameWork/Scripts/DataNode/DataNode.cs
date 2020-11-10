#region  
//=====================================================
// 文件名:      DataNode                
// 创建者:      岳智伟                
// 创建时间:    2020/11/09/ 17:06:59              	
// Email:       854426372@qq.com               
// 描述:        当前脚本的功能              
// 修改者列表:  修改者名字以及修改功能      
// 版权所有:    个人独立                 
//======================================================
#endregion 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork
{
    /// <summary>
    /// 数据节点类（储存节点数据以及父子节点信息）
    /// </summary>
    public class DataNode
    {
        public static readonly DataNode[] s_EmptyArray = new DataNode[] { };
        public static readonly string[] s_PathSplit = new string[] { ".", "/", "\\" };

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName
        {
            get
            {
                return Parent == null ? Name : string.Format("{0}{1}{2}", Parent.FullName, s_PathSplit[0], Name);
            }
        }

        /// <summary>
        /// 节点数据
        /// </summary>
        private object m_Data;

        /// <summary>
        /// 父节点
        /// </summary>
        public DataNode Parent { get; private set; }

        /// <summary>
        /// 子节点
        /// </summary>
        private List<DataNode> m_Childs;


        /// <summary>
        /// 子节点的数量
        /// </summary>
        public int ChildCount
        {
            get
            {
                return m_Childs != null ? m_Childs.Count : 0;
            }
        }

        /// <summary>
        /// 检查数据节点名称是否合法
        /// </summary>
        /// <param name="name">要检查的数据节点名称</param>
        /// <returns><是否合法/returns>
        private static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            foreach (string pathSplit in s_PathSplit)
            {
                if (name.Contains(pathSplit))
                {
                    return false;
                }
            }
            return true;
        }

        public DataNode(string name, DataNode parent)
        {
            if (!IsValidName(name))
            {
                Debug.LogError("数据节点的名字不合法：" + name);
                return;
            }

            Name = name;
            m_Data = null;
            Parent = parent;
            m_Childs = null;
        }


        /// <summary>
        /// 获取节点数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetData<T>()
        {
            return (T)m_Data;
        }

        /// <summary>
        /// 设置节点数据
        /// </summary>
        /// <param name="data"></param>
        public void SetData(object data)
        {
            m_Data = data;
        }

        /// <summary>
        /// 根据索引获取子数据节点
        /// </summary>
        /// <param name="index">子数据节点的索引</param>
        /// <returns>指定索引的子数据节点，如果索引越界，则返回Null</returns>
        public DataNode GetChild(int index)
        {
            return index >= ChildCount ? null : m_Childs[index];
        }

        /// <summary>
        /// 根据名称获取子数据节点
        /// </summary>
        /// <param name="name">子数据节点的名称</param>
        /// <returns></returns>
        public DataNode GetChild(string name)
        {
            if (!IsValidName(name))
            {
                Debug.LogError("子数据节点的名称不合法，无法获取。");
                return null;
            }

            if (m_Childs == null)
            {
                return null;
            }

            foreach (DataNode child in m_Childs)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据名称获取或者增加子节点数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataNode GetOrAddChild(string name)
        {
            DataNode node = GetChild(name);
            if (node != null)
            {
                return node;
            }
            node = new DataNode(name, this);
            if (m_Childs == null)
            {
                m_Childs = new List<DataNode>();
            }
            m_Childs.Add(node);
            return node;
        }

        /// <summary>
        /// 根据索引移除子数据节点
        /// </summary>
        /// <param name="index"></param>
        public void RemoveChild(int index)
        {
            DataNode node = GetChild(index);
            if (node == null)
            {
                return;
            }
            node.Clear();
            m_Childs.Remove(node);
        }

        /// <summary>
        /// 根据名字移除子数据节点
        /// </summary>
        /// <param name="name"></param>
        public void RemoveChild(string name)
        {
            DataNode node = GetChild(name);
            if (node == null)
            {
                return;
            }
            node.Clear();
            m_Childs.Remove(node);
        }

        //移除当前数据节点的数据和所有的子数据节点
        public void Clear()
        {
            m_Data = null;
            if (m_Childs != null)
            {
                foreach (DataNode node in m_Childs)
                {
                    node.Clear();
                }
                m_Childs.Clear();
            }
        }
    }
}