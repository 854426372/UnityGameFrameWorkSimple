#region  
//=====================================================
// 文件名: GlobalEventArgs                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 16:35:00              	
// Email: 854426372@qq.com               
// 描述: 全局事件的基类     (继承该类的事件类才能被事件池管理)    
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork
{
    public abstract class GlobalEventArgs : EventArgs, IReference
    {
        /// <summary>
        /// 事件类型ID
        /// </summary>
        public abstract int Id { get; }

        public abstract void Clear();
    }
}