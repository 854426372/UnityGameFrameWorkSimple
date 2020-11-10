#region  
//=====================================================
// 文件名:      FsmState                
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

    /// <summary>
    /// 状态机事件的响应方法模板（状态机自己的事件系统）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fsm"></param>
    /// <param name="sender"></param>
    /// <param name="userDate"></param>
    public delegate void FsmEventHandler<T>(Fsm<T> fsm, object sender, object userDate) where T : class;
    [Serializable]
    /// <summary>
    /// 状态基类
    /// </summary>
    /// <typeparam name="T">状态持有者类型</typeparam>
    public class FsmState<T> where T : class
    {
        /// <summary>
        /// 状态机订阅事件的字典
        /// </summary>
        private Dictionary<int, FsmEventHandler<T>> m_EventHandlers;

        public FsmState()
        {
            m_EventHandlers = new Dictionary<int, FsmEventHandler<T>>();
        }

        /// <summary>
        /// 订阅状态机事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventHandler"></param>
        protected void SubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                Debug.LogError("状态机事件响应方法为空，无法订阅状态机事件！");
            }
            if (!m_EventHandlers.ContainsKey(eventId))
            {
                m_EventHandlers.Add(eventId, eventHandler);
            }
            else
            {
                m_EventHandlers[eventId] += eventHandler;
            }
        }

        /// <summary>
        /// 取消注册状态机事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventHandler"></param>
        protected void UnSybscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                Debug.LogError("状态机事件响应方法为空，无法取消订阅状态机事件！");
            }
            if (m_EventHandlers.ContainsKey(eventId))
            {
                m_EventHandlers[eventId] -= eventHandler;
            }
        }

        /// <summary>
        /// 状态机响应事件
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="sender"></param>
        /// <param name="eventId"></param>
        /// <param name="userData"></param>
        public void OnEvent(Fsm<T> fsm, object sender, int eventId, object userData)
        {
            FsmEventHandler<T> eventHandlers = null;
            if (m_EventHandlers.TryGetValue(eventId, out eventHandlers))
            {
                if (eventHandlers != null)
                {
                    eventHandlers(fsm, sender, userData);
                }
            }
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="fsm"></param>
        protected void ChangeState<TState>(Fsm<T> fsm) where TState : FsmState<T>
        {
            ChangeState(fsm, typeof(TState));
        }
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="type"></param>
        protected void ChangeState(Fsm<T> fsm, Type type)
        {
            if (fsm == null)
            {
                Debug.LogError("需要切换的状态机为空，无法进行切换！");
            }

            if (type == null)
            {
                Debug.LogError("需要切换到的状态为空，无法进行切换！");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(type))
            {
                Debug.Log("要切换的状态没有直接或者间接的实现FsmState<T>,无法进行切换");
            }
            fsm.ChangeState(type);
        }

        #region 生命周期方法

        /// <summary>
        /// 状态机状态初始化时调用
        /// </summary>
        /// <param name="fsm"></param>
        public virtual void OnInit(Fsm<T> fsm)
        {

        }

        /// <summary>
        /// 状态机进入时调用
        /// </summary>
        /// <param name="fsm"></param>
        public virtual void OnEnter(Fsm<T> fsm)
        {

        }

        /// <summary>
        /// 状态机轮询时调用
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public virtual void OnUpdate(Fsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 状态机离开时调用
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="IsShutdown"></param>
        public virtual void OnLeave(Fsm<T> fsm, bool IsShutdown)
        {

        }

        /// <summary>
        /// 状态机销毁时调用
        /// </summary>
        /// <param name="fsm"></param>
        public virtual void OnDestort(Fsm<T> fsm)
        {
            m_EventHandlers.Clear();
        }
        #endregion
    }
}