#region  
//=====================================================
// 文件名: EventTestArgs                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 17:21:22              	
// Email: 854426372@qq.com               
// 描述: 当前脚本的功能              
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFrameWork;

public class EventTestArgs : GlobalEventArgs
{
    public string m_Name;

    public override int Id => typeof(EventTestArgs).GetHashCode();

    public override void Clear()
    {
        m_Name = string.Empty;
    }

    public EventTestArgs Fill(string name)
    {
        m_Name = name;
        return this;
    }
}
