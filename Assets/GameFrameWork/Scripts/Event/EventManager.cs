#region  
//=====================================================
// 文件名: EventManager                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 16:34:40              	
// Email: 854426372@qq.com               
// 描述: 负责管理所有的EventPool   
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
    public class EventManager : ManagerBase
    {
        public override int Priority
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// 事件池
        /// </summary>
        private EventPool<GlobalEventArgs> m_EventPool;

        public EventManager()
        {
            m_EventPool = new EventPool<GlobalEventArgs>();
        }

        public override void OnInit()
        {
        }

        public override void OnShutdow()
        {
            //关闭并清理事件池
            m_EventPool.Shutdown();
        }

        public override void OnUpdata(float elapaeSeconds, float realElapseSeconds)
        {
            m_EventPool.Update(elapaeSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 检查订阅事件处理方法是否存在
        /// </summary>
        public bool Check(int id, EventHandler<GlobalEventArgs> handler)
        {
            return m_EventPool.Check(id, handler);
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        public void Subscribe(int id, EventHandler<GlobalEventArgs> handler)
        {
            m_EventPool.Subscribe(id, handler);
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        public void Unsubscribe(int id, EventHandler<GlobalEventArgs> handler)
        {
            m_EventPool.Unsubscribe(id, handler);
        }

        /// <summary>
        /// 抛出事件（线程安全）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Fire(object sender,GlobalEventArgs e)
        {
            m_EventPool.Fire(sender, e);
        }

        /// <summary>
        /// 抛出事件（线程不安全）
        /// </summary>
        public void FireNow(object sender, GlobalEventArgs e)
        {
            m_EventPool.FireNow(sender, e);
        }
    }
}