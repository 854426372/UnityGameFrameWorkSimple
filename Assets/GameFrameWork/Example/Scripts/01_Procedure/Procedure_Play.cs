#region  
//=====================================================
// 文件名:      Procedure_Play                
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
using UnityGameFrameWork;


namespace UnityGameFrameWork_Example
{
    public class Procedure_Play : ProcedureBase
    {
        public override void OnInit(Fsm<ProcedureManager> fsm)
        {
            base.OnInit(fsm);
            SubscribeEvent(1, TestEventHandler);
        }
        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            base.OnEnter(fsm);
            OnEvent(fsm, 1, 1, "Procedure_Play");
        }
        public override void OnUpdate(Fsm<ProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (Input.GetMouseButtonUp(0))
            {
                ChangeState<Procedure_Over>(fsm);
            }
        }

        private void TestEventHandler<T>(Fsm<T> fsm, object sender, object userDate) where T : class
        {
            Debug.LogError((string)userDate);
        }
    }
}
