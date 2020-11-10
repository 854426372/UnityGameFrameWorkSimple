#region  
//=====================================================
// 文件名: EventTestMain                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 17:21:09              	
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

public class EventTestMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FrameWorkEntiy.Instance.GetManager<EventManager>().Subscribe(1, EventTestMethod);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventTestArgs e = ReferencePool.Acquire<EventTestArgs>();

            //派发事件
            FrameWorkEntiy.Instance.GetManager<EventManager>().Fire(this, e.Fill("EventArgs"));
        }

    }
    /// <summary>
    /// 事件处理方法
    /// </summary>
    private void EventTestMethod(object sender, GlobalEventArgs e)
    {
        EventTestArgs args = e as EventTestArgs;
        Debug.Log(args.m_Name);
    }
}
