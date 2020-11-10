#region  
//=====================================================
// 文件名:      DataManager                
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
    /// 数据节点的管理器，负责管理根节点
    /// </summary>
    public class DataNodeManager : ManagerBase
    {
        public override int Priority => 10;
        private static readonly string[] s_EmptyStringArray = new string[] { };

        /// <summary>
        /// 跟节点
        /// </summary>
        public DataNode Root { get; private set; }

        /// <summary>
        /// 根节点名称
        /// </summary>
        private const string RootName = "<Root>";

        public DataNodeManager()
        {
            Root = new DataNode(RootName, null);
        }

        public override void OnInit()
        {

        }

        public override void OnShutdow()
        {
            Root.Clear();
        }

        public override void OnUpdata(float elapaeSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 数据节点路径切分（这个方法能够将形如aaa.bbb.ccc的路径切分成{aaa,bbb,ccc}的字符串数组，以便我们通过遍历该数组，逐步寻找到最末尾的子结点）
        /// </summary>
        /// <param name="path">要切分的数据节点路径</param>
        /// <returns>切分后的字符串数组</returns>
        private static string[] GetSplitPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return s_EmptyStringArray;
            }
            return path.Split(DataNode.s_PathSplit, System.StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 获取数据节点
        /// </summary>
        /// <param name="path">相对于node的查找路径</param>
        /// <param name="node">查找起始节点</param>
        /// <returns></returns>
        public DataNode GetNode(string path, DataNode node = null)
        {
            DataNode current = node ?? Root;

            string[] splitpath = GetSplitPath(path);//获取子节点路径的数组

            foreach (string ChildName in splitpath)
            {
                current = current.GetChild(ChildName);//根据数组里的路径名获取子节点
                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        /// <summary>
        /// 获取或者增加数据节点
        /// </summary>
        /// <param name="path">相对于node 的查找路径</param>
        /// <param name="node">查找起始节点</param>
        /// <returns>指定位置的数据节点，如果没有找到，则增加相应的数据节点</returns>
        public DataNode GetOrAddNode(string path, DataNode node = null)
        {
            DataNode current = node ?? Root;

            string[] splitPath = GetSplitPath(path);
            
            foreach (string childName in splitPath)
            {
                current = current.GetOrAddChild(childName);
            }
            return current;
        }

        /// <summary>
        /// 移除数据节点
        /// </summary>
        /// <param name="path">相对于 node 的查找路径</param>
        /// <param name="node">查找起始节点</param>
        public void RemoveNode(string path, DataNode node = null)
        {
            DataNode current = node ?? Root;
            DataNode parent = current.Parent;
            string[] splitPath = GetSplitPath(path);
            foreach (string childName in splitPath)
            {
                parent = current;
                current = current.GetChild(childName);
                if (current == null)
                {
                    return;
                }
            }

            if (parent != null)
            {
                parent.RemoveChild(current.Name);
            }
        }

        /// <summary>
        /// 根据类型获取结点的数据
        /// </summary>
        /// <typeparam name="T">要获取的数据类型</typeparam>
        /// <param name="path">相对于 node 的查找路径</param>
        /// <param name="node">查找起始结点</param>
        public T GetData<T>(string path, DataNode node = null)
        {
            DataNode current = GetNode(path, node);
            if (current == null)
            {
                Debug.Log("要获取数据的结点不存在：" + path);
                return default(T);
            }
            return current.GetData<T>();
        }

        /// <summary>
        /// 设置数据结点的数据
        /// </summary>
        /// <param name="path">相对于node的查找路径</param>
        /// <param name="data">要设置的数据</param>
        /// <param name="node">查找起始结点</param>
        public void SetData(string path, object data, DataNode node = null)
        {
            DataNode current = GetOrAddNode(path, node);
            current.SetData(data);
        }
    }
}