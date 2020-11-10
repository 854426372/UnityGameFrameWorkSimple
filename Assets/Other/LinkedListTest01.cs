//=====================================================
/* 文 件  名:           LinkedListTest0·           */
/* 创 建  者:           #AuthorName#                       */
/* 创建时间:	        #CreateTime#         */
/* Email:	            #AuthorEmail#             */
/* 描  述: 	            当前脚本的功能               */
/* 修改者列表:	        修改者名字以及修改功能       */
/* (C) 版权 2019 -      危机管理系统                 */
/*  版权所有：          上海哲寻科技                 */
//======================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinkedListTest01 : MonoBehaviour
{
    public class Test01
    {
        public int num;
        public Test01(int theNum)
        {
            num = theNum;
        }
    }
    private LinkedList<Test01> test01s = new LinkedList<Test01>();
    private void Start()
    {
        Test01 T1 = new Test01(1);
        Test01 T2 = new Test01(2);
        Test01 T3 = new Test01(3);
        Test01 T4 = new Test01(4);
        test01s.AddLast(T1);
        AddCreate(T2);
        AddCreate(T3);
        AddCreate(T4);

        Debug.Log("第一个"+test01s.First.Value.num);
        Debug.Log("最后一个"+test01s.Last.Value.num);
        Debug.Log("第一个的下一个"+test01s.First.Next.Value.num);


        foreach (var item in test01s)
        {
            Debug.Log("目前内部的顺序" + item.num);
        }
        for (LinkedListNode<Test01> current = test01s.Last; current != null; current = current.Previous)
        {
            Debug.Log("销毁顺序"+current.Value.num);
            //Debug.Log("销毁的顺序"+current.Value.num);
        }

    }
    private void AddCreate(Test01 man)
    {
        LinkedListNode<Test01> current = test01s.First;
        Debug.Log("添加管理器顺序" + man.num);
        while (current != null)
        {
            if (man.num > current.Value.num)
            {
                break;
            }
            current = current.Next;
        }
        if (current != current.Next)
        {
            test01s.AddBefore(current, man);
        }
        else
        {
            test01s.AddLast(man);
        }
    }
}
