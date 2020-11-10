#region  
//=====================================================
// 文件名: IReference                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 14:04:51              	
// Email: 854426372@qq.com               
// 描述: 引用池接口，只有实现了该接口的对象才会被纳入引用池管理当中            
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameWork
{
    public interface IReference 
    {
        /// <summary>
        /// 清理引用
        /// </summary>
        void Clear();
    }
}