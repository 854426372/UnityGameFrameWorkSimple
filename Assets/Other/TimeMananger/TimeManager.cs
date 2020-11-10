//=====================================================
/* 文 件  名:           岳智伟                        */
/* 创 建  者:           #AuthorName#                  */
/* 创建时间:	        #CreateTime#                  */
/* Email:	            #AuthorEmail#                 */
/* 描  述: 	            当前脚本的功能                */
/* 修改者列表:	        修改者名字以及修改功能        */
/* (C) 版权 2019 -      危机管理系统                  */
/*  版权所有：          上海哲寻科技                  */
//======================================================


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TimeManager
{
    public class TimeManager : MonoBehaviour
    {
        public readonly Dictionary<object, TimeItem> TimeEvent = new Dictionary<object, TimeItem>();
        public float TotalTime;
        void Update()
        {
            TotalTime =  Time.realtimeSinceStartup;
            TimeItem[] timeItems = new TimeItem[TimeEvent.Values.Count];
            TimeEvent.Values.CopyTo(timeItems, 0);
            foreach (var item in timeItems)
            {
                if (null != item)
                {
                    item.Run(TotalTime);
                }
            }
        }
        public void RegisterTime(object theHolder, float theRegisterTime,float theDelay, Action theCallBack)
        {
            TimeItem timeItem = null;
            if (!TimeEvent.TryGetValue(theHolder,out timeItem))
            {
                TimeEvent.Add(theHolder, new TimeItem(theRegisterTime, theDelay, theCallBack));
            }
            else
            {
                Debug.LogError("Time of registeration！");
            }
        }
        public void UnRegister(object theHolder)
        {
            TimeItem timeItem = null;
            if (TimeEvent.TryGetValue(theHolder, out timeItem))
            {
                TimeEvent.Remove(theHolder);
            }
            else
            {
                Debug.LogError("Nonexistent time for registration！");
            }
        }
        public void ClearAll()
        {
            TimeEvent.Clear();
        }
    }
    public class TimeItem
    {
        public float RejisterTime;
        public float DelayTime;
        public Action CallBack;
        public TimeItem(float theRejister,float theDelayTime, Action theCallback)
        {
            this.RejisterTime = theRejister;
            this.DelayTime = theDelayTime;
            this.CallBack = theCallback;
        }
        public void Run(float currentTime)
        {
            float Interval = currentTime - RejisterTime;
            if (Interval>=DelayTime)
            {
                float count = Interval / this.DelayTime - 1;
                float mod = Interval % this.DelayTime;
                for (int index = 0; index < count; index++)
                {
                    this.CallBack();
                }
                this.RejisterTime = currentTime - mod;
            }
        }
    }
}