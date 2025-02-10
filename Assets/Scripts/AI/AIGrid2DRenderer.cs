using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGrid2DRenderer : BaseAIPath
{
    public int node2DCount = 30;

    public AINode2D node2DPrefab;

    public AINode2D[,] node2Ds;

 
    public AINode2D ClickNode2D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return FindNode2D(ray.origin);
    }



    public AINode2D FindNode2D(Vector3 pos)
    {


        int col = (int)(pos.x + node2DCount / 2);
        int row = (int)(pos.y + node2DCount / 2);
        
        if (CheckNode2D(row, col))
            return node2Ds[row, col];

        return null;
    }

    // back 노드 프리팹을 생성하는 함수
    void CreateGrid2D()
    {
        for (int r = 0; r < node2DCount; ++r)
        {
            for (int c = 0; c < node2DCount; ++c)
            {
                AINode2D node2D = Instantiate(node2DPrefab,
                                            new Vector3(c + 0.5f - node2DCount / 2,
                                                                 r + 0.5f - node2DCount / 2,
                                                                 0),
                                                                Quaternion.identity, transform);
                node2D.Init();
                node2D.SetRC(r, c);
                node2D.name = string.Format($"{r},{c}");
                node2Ds[r, c] = node2D;
            }
        }
    }

    public void OnReset()
    {
        foreach (AINode2D value in node2Ds)
        {
            value.OnReset();
        }
    }

    public void WallCreate()
    {
        foreach (AINode2D value in node2Ds)
        {
            if(value != null)
                value.OnWallCheck();   
        }
    }

    public List<AINode2D> tempWallCreate(Unit my, Unit targetUnit)
    {
        List<AINode2D> tempList = new List<AINode2D>();
        foreach (AINode2D value in node2Ds)
        {
            if (value != null)
            {
                if(value.pathCheck(my, targetUnit))
                {
                    tempList.Add(value);
                }
            }
                
        }

        return tempList;
    }

    public void WallDestory()
    {
        
        foreach (AINode2D value in node2Ds)
        {
           
            if (value != null)
                Destroy(value.gameObject);
        }
    }

    // 배열의 범위를 벗어났는지 체크하기 위한 코드
    public bool CheckNode2D(int row, int col)
    {
        if (row < 0 || col < 0 || row >= node2DCount || col >= node2DCount)
            return false;

        return true;
    }

    public AINode2D[] GetNeighbours(AINode2D node)
    {
        List<AINode2D> neighbours = new List<AINode2D>();

        for (int r = -1; r < 2; ++r)
        {
            for (int c = -1; c < 2; ++c)
            {
                if (r == 0 && c == 0)
                    continue;

                int currRow = node.row + r;
                int currCol = node.col + c;

                if (CheckNode2D(currRow, currCol))
                    neighbours.Add(node2Ds[currRow, currCol]);
            }
        }

        return neighbours.ToArray();
    }



    #region BaseAIPath로부터 상속받은 함수목록
    public override void Init()
    {

        node2Ds = new AINode2D[node2DCount, node2DCount];
        node2DPrefab = Resources.Load<AINode2D>("Prefab/AI/AINode2D");
       
    }
    public override void Run()
    {
    }
    public override void Open()
    {
    }
    public override void Close()
    {
    }
    #endregion BASEUI로부터 상속받은 함수목록
}

