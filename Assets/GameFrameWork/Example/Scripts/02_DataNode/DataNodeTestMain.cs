#region  
//=====================================================
// 文件名:      DataNodeTestMain                
// 创建者:      岳智伟                
// 创建时间:    2020/11/09/ 17:06:59              	
// Email:       854426372@qq.com               
// 描述:        测试数据结点使用             
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
    public class DataNodeTestMain : MonoBehaviour
    {
        
       
        private void Start()
        {
            //根据绝对路径设置与获取数据
            DataNodeManager dataNodeManager = FrameWorkEntiy.Instance.GetManager<DataNodeManager>();
            dataNodeManager.SetData("Player.Name", "Ellan");
            string playerName = dataNodeManager.GetData<string>("Player.Name");
            Debug.Log(playerName);

            //根据相对路径设置与获取数据
            DataNode playerNode = dataNodeManager.GetNode("Player");
            dataNodeManager.SetData("Level", 99, playerNode);
            int playerLevel = dataNodeManager.GetData<int>("Level",playerNode);
            string playerName2 = playerNode.GetChild("Name").GetData<string>();
            Debug.Log(playerLevel);
            Debug.Log(playerName2);

            //直接通过数据结点来操作
            DataNode playerExpNode = playerNode.GetOrAddChild("Exp");
            playerExpNode.SetData(1000);
            int playerExp = playerExpNode.GetData<int>();
            Debug.Log(playerExp);

        }
    }
}