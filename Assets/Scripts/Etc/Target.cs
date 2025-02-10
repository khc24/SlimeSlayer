using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Transform targetBoxRoot;
    Animator animator;

    [SerializeField]
    public Transform target; // x��ǥ�� �ٶ� Ÿ��
    

    private Vector3 targetPos; // Ÿ������ �̵��ϱ� ���� ���Ͱ��� ����
    private float angle; // ȸ������ �����ϴ� ����
    [SerializeField]
    Transform center; // ȸ����Ű���� gameObject�� Transform�� �޾ƿ��� ����

    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    private List<int> sortingOrder = new List<int>();

    public bool isdepthTarget = true;


    public void LookAt2D()
    {
        if (target == null || center == null)
            return;
        targetPos = target.position - center.position;

        // Atan2 = ���ʼ����� ������� �󸶴� ȸ���ߴ��� ���� �����ش�.
        // Atan�� ��ź��Ʈ�� ����/�غ��� ��ź��Ʈ�� ���� ������ ���Ѵ�.
        // Mathf.Rad2Deg : 1 ���� ���� ��ȯ�Ѵ� = 57.296�� ,  180��/����
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
