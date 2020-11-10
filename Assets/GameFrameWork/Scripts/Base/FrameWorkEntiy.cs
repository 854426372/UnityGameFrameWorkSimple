#region  
//=====================================================
// 文件名:      FrameWorkEntiy                
// 创建者:      岳智伟                
// 创建时间:    2020/11/09/ 17:06:59              	
// Email:       854426372@qq.com               
// 描述:        当前脚本的功能              
// 修改者列表:  修改者名字以及修改功能      
// 版权所有:    个人独立                 
//======================================================
#endregion 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork
{
    public class FrameWorkEntiy : MonoSingleton<FrameWorkEntiy>
    {
        /// <summary>
        /// 所有模块管理器的链表
        /// </summary>
        private LinkedList<ManagerBase> m_Managers = new LinkedList<ManagerBase>();
        void Update()
        {
            foreach (var item in m_Managers)
            {
                item.OnUpdata(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }
        private void OnDestroy()
        {
            for (LinkedListNode<ManagerBase> current = m_Managers.Last; current != null; current = current.Previous)
            {
                current.Value.OnShutdow();
            }
            m_Managers.Clear();
        }
        public T GetManager<T>() where T : ManagerBase
        {
            Type manageType = typeof(T);
            foreach (var item in m_Managers)
            {
                if (item.GetType() == manageType)
                {
                    return item as T;
                }
            }
            return CreateManager(manageType) as T;
        }
        public ManagerBase CreateManager(Type manager)
        {
            ManagerBase man = (ManagerBase)Activator.CreateInstance(manager);
            if (man == null)
            {
                Debug.LogError("模块管理器创建失败：" + manager.GetType().Name);
            }
            LinkedListNode<ManagerBase> current = m_Managers.First;
            while (current != null)
            {
                if (man.Priority > current.Value.Priority)
                {
                    break;
                }
                current = current.Next;
            }
            if (current != null)
            {
                m_Managers.AddBefore(current, man);
            }
            else
            {
                m_Managers.AddLast(man);
            }
            man.OnInit();
            return man;
        }

    }
}