#region  
//=====================================================
// 文件名:      MonoSingleton                
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
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError("场景中存在的单例数量>1" + _instance.GetType().Name);
                        return _instance;
                    }
                    if (_instance == null)
                    {
                        string InstanceName = typeof(T).Name;
                        GameObject InstanceGo = GameObject.Find(InstanceName);
                        if (InstanceGo == null)
                        {
                            InstanceGo = new GameObject(InstanceName);
                            DontDestroyOnLoad(InstanceGo);
                            _instance = InstanceGo.AddComponent<T>();
                            DontDestroyOnLoad(_instance);
                        }
                        else
                        {
                            Debug.LogError("场景中单例物体所挂载的物体" + InstanceGo.name);
                        }
                    }
                }
                return _instance;
            }
        }
        private void OnDestroy()
        {
            _instance = null;
        }
    }
}