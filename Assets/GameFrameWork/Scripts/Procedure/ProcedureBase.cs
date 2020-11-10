#region  
//=====================================================
// 文件名:      ProcedureBase                
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
    /// 流程基类（属于一种状态）
    /// </summary>
    public class ProcedureBase : FsmState<ProcedureManager>
    {
        public override void OnInit(Fsm<ProcedureManager> fsm)
        {
            base.OnInit(fsm);
          
        }
        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("进入流程:" + GetType().Name);

        }

        public override void OnLeave(Fsm<ProcedureManager> fsm, bool IsShutdown)
        {
            base.OnLeave(fsm, IsShutdown);
            Debug.Log("离开流程:" + GetType().Name);
        }

    }
}