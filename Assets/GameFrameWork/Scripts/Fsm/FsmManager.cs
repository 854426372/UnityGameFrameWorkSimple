#region  
//=====================================================
// 文件名:      FsmManager                
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
    /// 有限状态机管理器
    /// </summary>
    public class FsmManager : ManagerBase
    {
        public override int Priority => 60;

        /// <summary>
        /// 所有状态机的字典（在这里，状态机接口的作用就显示出来了）
        /// </summary>
        private Dictionary<string, IFsm> m_Fsms;

        private List<IFsm> m_TempFsms;


        public override void OnInit()
        {
            m_Fsms = new Dictionary<string, IFsm>();
            m_TempFsms = new List<IFsm>();
        }

        public override void OnShutdow()
        {
            foreach (KeyValuePair<string, IFsm> fsm in m_Fsms)
            {
                fsm.Value.Shutdown();
            }

            m_Fsms.Clear();
            m_TempFsms.Clear();
        }

        public override void OnUpdata(float elapaeSeconds, float realElapseSeconds)
        {
            m_TempFsms.Clear();

            if (m_Fsms.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<string, IFsm> fsm in m_Fsms)
            {
                m_TempFsms.Add(fsm.Value);
            }

            foreach (IFsm fsm in m_TempFsms)
            {
                if (fsm.IsDestroyed)
                {
                    continue;
                }
                fsm.Update(elapaeSeconds, realElapseSeconds);
            }
        }


        /// <summary>
        /// 是否存在状态机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasFsm<T>()
        {
            return HasFsm(typeof(T));
        }

        /// <summary>
        /// 是否存在状态机
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasFsm(Type type)
        {
            return HasFsm(type.FullName);
        }

        /// <summary>
        /// 是否存在状态机
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private bool HasFsm(string fullName)
        {
            return m_Fsms.ContainsKey(fullName);
        }


        /// <summary>
        /// 创建状态机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public Fsm<T> CreateFsm<T>(T owner, string name = "", params FsmState<T>[] states) where T : class
        {
            if (HasFsm<T>())
            {
                Debug.LogError("要创建的状态机已经存在！");
            }

            if (name == "")
            {
                name = typeof(T).FullName;
            }

            Fsm<T> fsm = new Fsm<T>(name, owner, states);
            m_Fsms.Add(name, fsm);
            return fsm;
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DestroyFsm(string name)
        {
            IFsm fsm = null;
            if (m_Fsms.TryGetValue(name, out fsm))
            {
                fsm.Shutdown();
                return m_Fsms.Remove(name);
            }
            return false;
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool DestroyFsm<T>() where T : class
        {
            return DestroyFsm(typeof(T).FullName);
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="fsm"></param>
        /// <returns></returns>
        public bool DestroyFsm(IFsm fsm)
        {
            return DestroyFsm(fsm.Name);
        }
    }
}