#region  
//=====================================================
// 文件名:      ProcedureManager                
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
    /// 流程管理类（负责管理所有的流程）
    /// </summary>
    public class ProcedureManager : ManagerBase
    {

        public override int Priority => -10;

        /// <summary>
        /// 状态机管理器
        /// </summary>
        private FsmManager m_FsmManager;

        /// <summary>
        /// 流程的状态机
        /// </summary>
        private Fsm<ProcedureManager> m_ProcedureFsm;

        /// <summary>
        /// 所有流程的列表
        /// </summary>
        private List<ProcedureBase> m_Procedures;

        /// <summary>
        /// 入口流程
        /// </summary>
        private ProcedureBase m_EntranceProcedure;

        /// <summary>
        /// 当前流程
        /// </summary>
        private ProcedureBase CurrentProcedure
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    Debug.LogError("流程状态机为空，无法获取当前流程！");
                }
                return (ProcedureBase)m_ProcedureFsm.CurrentState;
            }
        }

        public ProcedureManager()
        {
            m_FsmManager = FrameWorkEntiy.Instance.GetManager<FsmManager>();
            m_ProcedureFsm = null;
            m_Procedures = new List<ProcedureBase>();
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="procedure"></param>
        public void AddProcedure(ProcedureBase procedure)
        {
            if (procedure == null)
            {
                Debug.LogError("要添加的流程为空");
                return;
            }
            m_Procedures.Add(procedure);
        }

        /// <summary>
        /// 设置流程入口
        /// </summary>
        /// <param name="procedure"></param>
        public void SetEntranceProcedure(ProcedureBase procedure)
        {
            m_EntranceProcedure = procedure;
        }

        /// <summary>
        /// 创建流程状态机
        /// </summary>
        public void CreateProceduresFsm()
        {
            m_ProcedureFsm = m_FsmManager.CreateFsm(this, "", m_Procedures.ToArray());

            if (m_EntranceProcedure == null)
            {
                Debug.LogError("入口流程为空，无法开始流程！");
                return;
            }

            //开始流程
            m_ProcedureFsm.Start(m_EntranceProcedure.GetType());
        }

        public override void OnInit()
        {

        }

        public override void OnShutdow()
        {

        }

        public override void OnUpdata(float elapaeSeconds, float realElapseSeconds)
        {

        }

    }
}