#region  
//=====================================================
// 文件名:      IFsm                
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
    /// 状态机的接口
    /// </summary>
    public interface IFsm
    {
        /// <summary>
        /// 状态机的名字
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 状态机的持有者
        /// </summary>
        Type OwnerType { get; }

        /// <summary>
        /// 状态机是否被销毁
        /// </summary>
        bool IsDestroyed { get; }

        /// <summary>
        /// 当前状态运行时间
        /// </summary>
        float CurrentStateTime { get; }

        /// <summary>
        /// 状态机轮询
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理状态机
        /// </summary>
        void Shutdown();
    }
}