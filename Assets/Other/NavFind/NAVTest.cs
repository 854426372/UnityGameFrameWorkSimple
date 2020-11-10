//=====================================================
/* 文 件  名:           NAVTest           */
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
using UnityEngine.AI;

public class NAVTest : MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public RaycastHit m_HitInfo = new RaycastHit();
    public Transform line;//就是我们刚才添加的 gameobject
    public LineRenderer _lineRenderer;//储存 gameobject 的 LineRenderer 组件
    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public bool IsCalulatePath = false;
    public NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        _lineRenderer = line.GetComponent<LineRenderer>();
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);
        _lineRenderer.widthCurve = curve;
        _lineRenderer.widthMultiplier = 10;
    }
    private void Update()
    {
       
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            Debug.Log("true");
            if (Input.GetMouseButtonDown(0))
            {
                //m_Agent.Stop();  
                _lineRenderer.gameObject.SetActive(true);
                if (!IsCalulatePath)
                {
                    Vect3.Clear();
                    _lineRenderer.positionCount = 0;
                    path.ClearCorners();
                    StartPoint = m_HitInfo.point;
                }
                else
                {
                    EndPoint = m_HitInfo.point;
                    CalculatePath(StartPoint, EndPoint);
                }
                IsCalulatePath = !IsCalulatePath;
            }


            
        }
    }
    List<Vector3> Vect3 = new List<Vector3>();
    private  void CalculatePath(Vector3 start ,Vector3 end)
    {
        NavMeshPath TempPath = new NavMeshPath();
        m_Agent.CalculatePath(start, TempPath);
        NavMesh.CalculatePath(start, end, NavMesh.AllAreas, TempPath);
        Debug.Log(TempPath.corners.Length);
        //StartCoroutine(Delay(new List<Vector3>(TempPath.corners)));
        foreach (var item in TempPath.corners)
        {
            Vect3.Add(item);
        }
        if (TempPath.status == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("当前路径点无法到达！");
            return;
        }
        if ((TempPath.corners[TempPath.corners.Length-1]- EndPoint).sqrMagnitude >=1)
        {
            Debug.Log("还未完全寻到终点");
            Debug.Log((TempPath.corners[TempPath.corners.Length - 1] - EndPoint).sqrMagnitude);
            CalculatePath(TempPath.corners[TempPath.corners.Length - 1], EndPoint);
        }
        else
        {
            StartCoroutine(Delay(Vect3));
        }
    }
    IEnumerator Delay(List<Vector3> v3)
    {
        _lineRenderer.positionCount += v3.Count;
        for (int i = 0; i < v3.Count; i++)
        {
            _lineRenderer.SetPosition(i, v3[i]);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
