using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPath2D : MonoBehaviour
{
    public static int uniqueID = 0;
    public AIPathFinding2D pathFinding2D;

    public List<AINode2D> path = new List<AINode2D>();
    public bool update = false;
    public float speed = 1;
    public int myID;
    
    public Vector3 getPos
    {
        get
        {
            if (gameObject == null)
                return Vector3.zero;
            Vector3 v = transform.position;
            v.y += 0.5f;
            return v;
        }
    }

    public virtual void Init()
    {
        myID = ++uniqueID;

    }

    public void SetPath(List<AINode2D> path)
    {
        this.path.Clear();
        this.path.AddRange(path);
    }
}

