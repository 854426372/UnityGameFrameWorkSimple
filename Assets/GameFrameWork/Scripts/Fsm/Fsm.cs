#region  
//=====================================================
// 文件名:      Fsm                
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
    /// 状态机
    /// </summary>
    /// <typeparam name="T">状态机持有者类型</typeparam>
    public class Fsm<T> : IFsm where T : class
    {
        public string Name { get; private set; }

        public Type OwnerType => typeof(T);

        public bool IsDestroyed { get; private set; }

        public float CurrentStateTime { get; private set; }

        /// <summary>
        /// 状态机里所有状态的字典
        /// </summary>
        private Dictionary<string, FsmState<T>> m_states;

        /// <summary>
        /// 状态机里所有数据的字典
        /// </summary>
        private Dictionary<string, object> m_Datas;

        /// <summary>
        /// 当前状态
        /// </summary>
        public FsmState<T> CurrentState { get; private set; }

        /// <summary>
        /// 状态机的持有者
        /// </summary>
        public T Owner { get; private set; }

        public Fsm(string name, T owner, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                Debug.LogError("状态机的持有者为空！");
            }

            if (states == null || states.Length < 1)
            {
                Debug.LogError("没有要添加进状态机的状态！");
            }

            Name = name;
            Owner = owner;
            m_states = new Dictionary<string, FsmState<T>>();
            m_Datas = new Dictionary<string, object>();

            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    Debug.LogError("要添加进去的状态机的状态为空！");
                }

                string stateName = state.GetType().FullName;
                if (m_states.ContainsKey(stateName))
                {
                    Debug.LogError("要添加进去的状态机的状态已经存在：" + stateName);
                }

                m_states.Add(stateName, state);
                state.OnInit(this);
            }

            CurrentStateTime = 0f;
            CurrentState = null;
            IsDestroyed = false;
        }

        /// <summary>
        /// 关闭并清理状态机
        /// </summary>
        public void Shutdown()
        {
            if (CurrentState != null)
            {
                CurrentState.OnLeave(this, true);
                CurrentState = null;
                CurrentStateTime = 0f;
            }

            foreach (KeyValuePair<string, FsmState<T>> state in m_states)
            {
                state.Value.OnDestort(this);
            }

            m_states.Clear();
            m_Datas.Clear();

            IsDestroyed = true;
        }

        /// <summary>
        /// 状态机轮询
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (CurrentState == null)
            {
                return;
            }

            CurrentStateTime += elapseSeconds;
            CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <returns></returns>
        public TState GetState<TState>() where TState : FsmState<T>
        {
            return GetState(typeof(TState)) as TState;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                Debug.LogError("要获取的状态为空！");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                Debug.LogError("要获取的状态" + stateType.FullName + "没有直接或者间接的实现:" + typeof(FsmState<T>).FullName);
            }

            FsmState<T> state = null;
            if (m_states.TryGetValue(stateType.FullName, out state))
            {
                return state;
            }
            return null;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        public void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="type"></param>
        public void ChangeState(Type type)
        {
            if (CurrentState == null)
            {
                Debug.LogError("当前状态为空，无法切换状态！");
            }

            FsmState<T> state = GetState(type);
            if (state == null)
            {
                Debug.Log("获取到的状态为空，无法进行切换：" + type.FullName);
            }

            CurrentState.OnLeave(this, false);
            CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 开始状态机
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        public void Start<TState>() where TState : FsmState<T>
        {
            Start(typeof(TState));
        }

        /// <summary>
        /// 开始状态机
        /// </summary>
        /// <param name="statetype"></param>
        public void Start(Type statetype)
        {
            if (CurrentState != null)
            {
                Debug.LogError("当前状态已经开始，无法再次开始！");
            }

            if (statetype == null)
            {
                Debug.LogError("要开始的状态机为空，无法开始！");
            }

            FsmState<T> state = GetState(statetype);
            if (state == null)
            {
                Debug.LogError("获取到的状态为空，无法开始！");
            }

            CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 抛出状态机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventId"></param>
        public void FireEvent(object sender, int eventId)
        {
            if (CurrentState == null)
            {
                Debug.Log("当前状态为空，无法抛出事件！");
            }

            CurrentState.OnEvent(this, sender, eventId, null);
        }

        /// <summary>
        /// 是否存在状态机的数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("要查询的状态机数据名字为空的！");
            }
            return m_Datas.ContainsKey(name);
        }

        /// <summary>
        /// 获取状态机数据
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public TData GetData<TData>(string name)
        {
            return (TData)GetData(name);
        }

        /// <summary>
        /// 获取状态机数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("要获取的状态机数据的名字为空！");
            }
            object data = null;
            m_Datas.TryGetValue(name, out data);
            return data;
        }

        /// <summary>
        /// 设置状态机数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SetData(string name, object data)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("要设置的状态机数据的名字为空！");
            }
            m_Datas[name] = data;
        }

        /// <summary>
        /// 移除状态机数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("要移除的状态机数据的名字为空！");
            }
            return m_Datas.Remove(name);
        }
    }
}