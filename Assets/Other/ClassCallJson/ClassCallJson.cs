#region  
//=====================================================
// 文件名: ClassCallJson                
// 创建者: 岳智伟                
// 创建时间: 2020/11/10/ 09:59:40              	
// Email: 854426372@qq.com               
// 描述: 当前脚本的功能              
// 修改者列表: 修改者名字以及修改功能      
// 版权所有: 个人独立                 
//======================================================
#endregion 



using System;
using UnityEngine;

[Serializable]
public class TestA
{
    public string AAA { get; set; }
    public int BBB;
    public Vect3 vect3;
    private string Aaa { get; set; }
}
[Serializable]
public class Vect3
{
    public double X;
    public double Y;
    public double Z;
}
public class ClassCallJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestA testA = new TestA();
        testA.AAA = "asd";
        testA.BBB = 111;
        Vect3 vect3 = new Vect3();
        vect3.X = 1.1f;
        vect3.Y = 1.2f;
        vect3.Z = 1.4f;
        testA.vect3 = vect3;

        var json = JsonUtility.ToJson(testA);
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
