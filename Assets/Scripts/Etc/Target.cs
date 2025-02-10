using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Transform targetBoxRoot;
    Animator animator;

    [SerializeField]
    public Transform target; // x좌표가 바라볼 타겟
    

    private Vector3 targetPos; // 타켓으로 이동하기 위한 벡터값을 저장
    private float angle; // 회전값을 저장하는 변수
    [SerializeField]
    Transform center; // 회전시키려는 gameObject의 Transform을 받아오는 변수

    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    private List<int> sortingOrder = new List<int>();

    public bool isdepthTarget = true;


    public void LookAt2D()
    {
        if (target == null || center == null)
            return;
        targetPos = target.position - center.position;

        // Atan2 = 시초선에서 동경까지 얼마다 회전했는지 값을 구해준다.
        // Atan은 역탄젠트로 높이/밑변에 역탄젠트를 통해 각도를 구한다.
        // Mathf.Rad2Deg : 1 라디안 값을 반환한다 = 57.296도 ,  180도/파이
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        center.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
    }



   


    public Vector3 getTargetPosDir
    {
        get { return targetPos; }
    }

    public Transform getTargetBoxRoot
    {
        get { return targetBoxRoot; }
    }

    public Transform getCenter
    {
        get { return center; }
    }

    public Animator getAnimator
    {
        get { return animator; }
    }


    public Transform setTarget
    {
        set { target = value; }
    }

    public void Init()
    {
        Transform t = transform.Find("TargetBoxRoot");
        if (t != null)
        {
            targetBoxRoot = t;
            animator = t.GetComponent<Animator>();  
        }
            
        t = transform.Find("root/Center");
        if(t!=null) center = t;

        renderers.AddRange(
            GetComponentsInChildren<SpriteRenderer>(true));

        for (int i = 0; i < renderers.Count; ++i)
            sortingOrder.Add(renderers[i].sortingOrder);


    }


    


    public void setActive(bool path = false)
    {
        
        if (targetBoxRoot != null)
        {
            targetBoxRoot.gameObject.SetActive(path);
        } 

    }

    public void SetSortingOrder(int order)
    {
        for (int i = 0; i < renderers.Count; ++i)
        {
            renderers[i].sortingOrder = order + sortingOrder[i];
        }
    }

 

}
