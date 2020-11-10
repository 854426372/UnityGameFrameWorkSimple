#region  
//=====================================================
// 文件名: #SCRIPTNAME#                
// 创建者: #AuthorName#                
// 创建时间: #CreateTime#              	
// Email: #AuthorEmail#               
// 描述: 当前脚本的功能              
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: #Copyright#                 
//======================================================
#endregion 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Copyright : UnityEditor.AssetModificationProcessor
{
    private const string AuthorName = "岳智伟";
    private const string AuthorEmail = "854426372@qq.com";
    private const string AuthorCopyright = "个人独立";
    private const string DateFormat=  "yyyy/MM/dd/ HH:mm:ss";
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = File.ReadAllText(path);
            allText = allText.Replace("#AuthorName#", AuthorName);
            allText = allText.Replace("#AuthorEmail#", AuthorEmail);
            allText = allText.Replace("#CreateTime#", System.DateTime.Now.ToString(DateFormat));
            allText = allText.Replace("#Copyright#", AuthorCopyright);
            File.WriteAllText(path, allText);
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}
