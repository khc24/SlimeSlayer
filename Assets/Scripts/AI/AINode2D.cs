using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AINode2DType
{
    None,
    Wall
}

public class AINode2D : MonoBehaviour

{

    public AINode2DType AInode2DType;
    public SpriteRenderer sprite;
  

    public int row;
    public int col;

    public int gCost;
    public int hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public AINode2D parents;


    public void Init()
    {
        sprite = GetComponent<SpriteRenderer>(); ;
      
    }



    public void OnReset()
    {
        AInode2DType = AINode2DType.None;
        parents = null;
        hCost = 0;
        gCost = 0;
        sprite.color = Color.white;
    }

    public void OnWallCheck()
    {   
        Collider2D[] coll2D = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        SetNodeType(AINode2DType.None);


        foreach (var value in coll2D)
        {
            if (value.tag == "OutWall" || value.tag == "InWall")
            {
                SetNodeType(AINode2DType.Wall);
            }
        }

    }

    public bool OnCheck(string s = "")
    {
        if (s == "")
            return false;
        
            

        Collider2D coll = Physics2D.OverlapBox(transform.position, new Vector2(1,1), 0, 1 << LayerMask.NameToLayer(s));

        if (coll != null)
            return true;
        return false;
    }

    public bool OnCheckUnit(Unit targetUnit = null)
    {
        if (targetUnit == null  )
            return false;


        Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, 1 << targetUnit.gameObject.layer);

        foreach(var value in coll)
        {
          Unit u = value.GetComponent<Unit>();
            if(u != null)
            {
                if (u == targetUnit)
                {
                    return true;
                }
                    
            }
        }

        return false;
    }




    public bool pathCheck(Unit my, Unit targetUnit = null)
    {
        if(my == null || targetUnit == null)
            return false;

        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 0.3f, 1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("NPC")
                                                                                    | 1 << LayerMask.NameToLayer("Item"));

        
        foreach(var value in coll)
        {
            Unit target = value.GetComponent<Unit>();
            if (target != null)
            {
                if (my != target && targetUnit != target)
                {
                    return true;
                }
                    
            }
        }
        return false;
    }

    public bool pathCheck2(int myId, Unit targetUnit = null)
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 0.3f, 1 << LayerMask.NameToLayer("Monster"));

        foreach (var value in coll)
        {
            AIPath2D target = value.GetComponent<AIPath2D>();
            if (target != null)
            {
                if (myId != target.myID)
                    return true;
            }
        }
        return false;
    }


    public void SetRC(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetNodeType(AINode2DType AInode2DType)
    {
        this.AInode2DType = AInode2DType;
        if (this.AInode2DType == AINode2DType.Wall)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    public void SetGCost(int gcost)
    {
        gCost = gcost;
    }

    public void SetHCost(int hcost)
    {
        hCost = hcost;
    }

 

}
