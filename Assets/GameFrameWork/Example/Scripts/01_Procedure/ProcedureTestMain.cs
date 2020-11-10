    #region  
//=====================================================
// 文件名:      ProcedureTestMain                
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
    public class ProcedureTestMain : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ProcedureManager proceManager = FrameWorkEntiy.Instance.GetManager<ProcedureManager>();
            //添加入口流程
            Procedure_Start start = new Procedure_Start();
            proceManager.AddProcedure(start);
            proceManager.SetEntranceProcedure(start);

            //添加其他流程
            proceManager.AddProcedure(new Procedure_Play());
            proceManager.AddProcedure(new Procedure_Over());

            //创建流程状态机
            proceManager.CreateProceduresFsm();

            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {

            }
        }
    }
}