#region  
//=====================================================
// 文件名:      ManagerBase                
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
    /// 模块管理器基类
    /// </summary>
    public abstract class ManagerBase
    {
        /// <summary>
        /// 模块优先级，优先级高的模块会先轮询，并且后关闭
        /// </summary>
        public virtual int Priority { get { return 0; } }

        /// <summary>
        /// 初始化模块
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// 轮训模块
        /// </summary>
        /// <param name="elapaeSeconds">逻辑流失秒</param>
        /// <param name="realElapseSeconds">真实流失秒</param>
        public abstract void OnUpdata(float elapaeSeconds, float realElapseSeconds);

        /// <summary>
        /// 停止并清理模块
        /// </summary>
        public abstract void OnShutdow();
    }
}