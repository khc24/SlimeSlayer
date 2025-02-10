using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapIcon : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Image icon;
    private bool targetState = false;

    public Vector3 TargetPos
    {
        get { return target.position; }
    }

    
    public void Init()
    {
        icon = GetComponent<Image>();
    }

    public void SetColor(Color color)
    {
        icon.color = color;
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
            targetState = true;

        this.target = target;
    }



    public void UpdatePos(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void iconDestroy()
    {
        Destroy(gameObject);
    }

    public void Run()
    {
        
        if(target != null)
            transform.localPosition = MinimapHelper.WorldPosToMapPos(target.position);
    }
}
