#region  
//=====================================================
// 文件名:      Procedure_Start                
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
    public class Procedure_Start : ProcedureBase
    {
        public override void OnUpdate(Fsm<ProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (Input.GetMouseButtonUp(0))
            {
                ChangeState<Procedure_Play>(fsm);
            }
        }
    }
}
