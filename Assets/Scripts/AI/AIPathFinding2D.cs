using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode2DComparer : IComparer<AINode2D>
{
    public int Compare(AINode2D left, AINode2D right)
    {
        if (left.fCost < right.fCost)
            return -1;
        else if (left.fCost > right.fCost)
            return 1;
        else
        {
            if (left.hCost < right.hCost)
                return -1;
            else if (left.hCost > right.hCost)
                return 1;

        }
        return 0;
    }
}

public class AIOrder
{
    public AIPath2D seeker;
    public AIPath2D target;
    public List<AINode2D> subWall;
}


public class AIPathFinding2D : BaseAIPath
{
    public List<AIOrder> AIorderList = new List<AIOrder>();
    

    public AIGrid2DRenderer grid;

    public AINode2DComparer AInode2DComparer = new AINode2DComparer();

    public List<AINode2D> openedNode2D = new List<AINode2D>();

    public List<AINode2D> closedNode2D = new List<AINode2D>();

    public List<AINode2D> pathList = new List<AINode2D>();

    public AIPath2D seeker;
    public AIPath2D target;
    public List<AINode2D> subWall = new List<AINode2D>();

    public AINode2D start;
    public AINode2D end;

    public AINode2D targetNode2D;
    public AINode2D prevNode2D;
    public AINode2D currNode2D;

    public bool execute = false;


    public int GetDistance(AINode2D a, AINode2D b)
    {
        int x = Mathf.Abs(a.col - b.col);
        int y = Mathf.Abs(a.row - b.row);

        return 14 * Mathf.Min(x, y) + Mathf.Abs(x - y) * 10;
    }


    public void Find(AIPath2D sk, AIPath2D t, List<AINode2D> s = null)
    {

        
        if (grid.FindNode2D(sk.getPos) == grid.FindNode2D(t.getPos))
        {
            return;
        }

        if (grid.FindNode2D(sk.getPos) == null || grid.FindNode2D(t.getPos) == null)
            return;


        if(s != null)
        {
            List<AIOrder> tempOrderList = new List<AIOrder>();
            for (int i = 0; i < AIorderList.Count; i++)
            {
                if (AIorderList[i].seeker.myID != sk.myID)
                {
                    tempOrderList.Add(AIorderList[i]);
                }
            }

            AIorderList.Clear();
            AIorderList.AddRange(tempOrderList);
        }
        else
        {
            for (int i = 0; i < AIorderList.Count; i++)
            {
                if (AIorderList[i].seeker.myID == sk.myID)
                {
                    return;
                }
            }
        }



        AIOrder newOrder = new AIOrder();
        
        newOrder.seeker = sk;
        newOrder.target = t;
        newOrder.subWall = new List<AINode2D>();
        if(s != null)
            newOrder.subWall.AddRange(s);
        
        AIorderList.Add(newOrder);

        
        if (execute == false)
        {
          
            execute = true;
            this.seeker = AIorderList[0].seeker;
            this.target = AIorderList[0].target;
            this.subWall.Clear();
            this.subWall.AddRange(AIorderList[0].subWall);
            AIorderList.RemoveAt(0);
            
            StartCoroutine(IEFind(this.seeker, this.target, this.subWall));
           
        }
        
    }


    public IEnumerator IEFind(AIPath2D seeker, AIPath2D target, List<AINode2D> subWall = null)
    {
        int time = 0;
        OnReset();

        if (seeker == null || target == null)
            yield break;

        AINode2D startNode = grid.FindNode2D(seeker.getPos);
        AINode2D endNode = grid.FindNode2D(target.getPos);
        
        if (subWall != null)
        {
            foreach(var value in subWall)
            {
                closedNode2D.Add(value);
            }
        }
            

        if (startNode == null || endNode == null)
        {
           
            if (AIorderList.Count > 0)
            {
               
                AIPath2D orderSeeker = AIorderList[0].seeker;
                AIPath2D orderTarget = AIorderList[0].target;
                List<AINode2D> orderSubWall = new List<AINode2D>();
                orderSubWall.AddRange(AIorderList[0].subWall);
                AIorderList.RemoveAt(0);
                StartCoroutine(IEFind(orderSeeker, orderTarget, orderSubWall));
            }
            else
            {
               
                execute = false;
            }
        
            yield break;
        }

      
        startNode.parents = null;
        endNode.parents = null;

        // ������ ����
        start = startNode;
        currNode2D = startNode;
        prevNode2D = startNode;
        end = endNode;

        currNode2D.SetGCost(0);
        currNode2D.SetHCost(GetDistance(start, end));

        do
        {
            AINode2D[] neighbours = grid.GetNeighbours(currNode2D);

            for (int i = 0; i < neighbours.Length; ++i)
            {
                // ���� ������� ����Ʈ�� �� �ִ� ���� �����ϵ��� ó��
                if (neighbours[i].AInode2DType == AINode2DType.Wall)
                    continue;

                if (closedNode2D.Contains(neighbours[i]))
                    continue;

                if (neighbours[i].col == (currNode2D.col - 1) && neighbours[i].row == (currNode2D.row + 1))
                {

                    if (grid.CheckNode2D(currNode2D.row, currNode2D.col - 1))
                    {
                        if (grid.node2Ds[currNode2D.row, currNode2D.col - 1].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }

                    }
                    if (grid.CheckNode2D(currNode2D.row + 1, currNode2D.col))
                    {
                        if (grid.node2Ds[currNode2D.row + 1, currNode2D.col].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                }
                else if (neighbours[i].col == (currNode2D.col + 1) && neighbours[i].row == (currNode2D.row + 1))
                {

                    if (grid.CheckNode2D(currNode2D.row, currNode2D.col + 1))
                    {
                        if (grid.node2Ds[currNode2D.row, currNode2D.col + 1].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }

                    }
                    if (grid.CheckNode2D(currNode2D.row + 1, currNode2D.col))
                    {
                        if (grid.node2Ds[currNode2D.row + 1, currNode2D.col].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                }
                else if (neighbours[i].col == (currNode2D.col - 1) && neighbours[i].row == (currNode2D.row - 1))
                {

                    if (grid.CheckNode2D(currNode2D.row, currNode2D.col - 1))
                    {
                        if (grid.node2Ds[currNode2D.row, currNode2D.col - 1].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                    if (grid.CheckNode2D(currNode2D.row - 1, currNode2D.col))
                    {
                        if (grid.node2Ds[currNode2D.row - 1, currNode2D.col].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                }
                else if (neighbours[i].col == (currNode2D.col + 1) && neighbours[i].row == (currNode2D.row - 1))
                {

                    if (grid.CheckNode2D(currNode2D.row, currNode2D.col + 1))
                    {
                        if (grid.node2Ds[currNode2D.row, currNode2D.col + 1].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                    if (grid.CheckNode2D(currNode2D.row - 1, currNode2D.col))
                    {
                        if (grid.node2Ds[currNode2D.row - 1, currNode2D.col].AInode2DType == AINode2DType.Wall)
                        {
                            continue;
                        }
                    }
                }

                // ���س�忡�� �̿��������� gCost�� ���
                int gCost = currNode2D.gCost + GetDistance(currNode2D, neighbours[i]);

                // �̿���忡 ����� ���� ���� ����̰ų�, ������ ���� �ִ� ������ �� �۴ٸ�
                // gCost�� hCost�� ����
                if (openedNode2D.Contains(neighbours[i]) == false ||
                    gCost < neighbours[i].gCost)
                {
                    neighbours[i].SetGCost(gCost);
                    neighbours[i].SetHCost(GetDistance(neighbours[i], endNode));

                    // �̿������ �θ� ������ ���س��� ����
                    neighbours[i].parents = currNode2D;

                    // ���³�� ��Ͽ� ��ϵ��� ���� ���¶�� ���³�帮��Ʈ�� ���
                    if (openedNode2D.Contains(neighbours[i]) == false)
                        openedNode2D.Add(neighbours[i]);

                }
            }

            closedNode2D.Add(currNode2D);

            // ���³�帮��Ʈ�� �ִ� ��尡 ���س��� �����Ǵ� ��쵵 �ֱ� ������
            // ���³�帮��Ʈ�� ����� �ִٸ� ����
            if (openedNode2D.Contains(currNode2D))
                openedNode2D.Remove(currNode2D);

            // ���¸�Ͽ� �����Ͱ� �ִٸ� �ּ� ��밪�� ���� ��带 ã�´�.
            if (openedNode2D.Count > 0)
            {
                // ������������ �����մϴ�.
                openedNode2D.Sort(AInode2DComparer);

                // ���� ���س�带 �����մϴ�.
                prevNode2D = currNode2D;

                // ���� ���س�带 ���³�帮���� �ִ� �ּ� ��밪���� ����
                currNode2D = openedNode2D[0];

                if (currNode2D == endNode)
                {
                    
                    pathList.AddRange(RetracePath(endNode));
                    // ĳ���Ͱ� ��ġ�� ���� ��尪�� ����
                    pathList.RemoveAt(0);

                    
                    seeker.SetPath(pathList);
                    break;
                    
                }
            }

            if(time < 50)
            {
                time++;
            }
            else
            {
                time = 0;
                yield return null;
            }
                

        } while (openedNode2D.Count > 0);

       

        if (AIorderList.Count > 0)
        {
            AIPath2D orderSeeker = AIorderList[0].seeker;
            AIPath2D orderTarget = AIorderList[0].target;
            List<AINode2D> orderSubWall = new List<AINode2D>();
            orderSubWall.AddRange(AIorderList[0].subWall);
            AIorderList.RemoveAt(0);
            StartCoroutine(IEFind(orderSeeker, orderTarget, orderSubWall));
        }
        else
        {
            execute = false;
        }
    }

    List<AINode2D> RetracePath(AINode2D node)
    {
        List<AINode2D> nodes = new List<AINode2D>();

        while (node != null)
        {

            nodes.Add(node);
            node = node.parents;


        }
        nodes.Reverse();
        return nodes;
    }

    public void OnReset()
    {
        openedNode2D.Clear();
        closedNode2D.Clear();
        start = null;
        end = null;
        pathList.Clear();
    }


    #region BaseAIPath�κ��� ��ӹ��� �Լ����
    public override void Init()
    {
        grid = GameObject.FindObjectOfType<AIGrid2DRenderer>();
        
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
    #endregion BASEUI�κ��� ��ӹ��� �Լ����
}
