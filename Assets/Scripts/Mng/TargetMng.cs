using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TargetMng : Mng<TargetMng>
{
    private List<Target> TargetList = new List<Target>();
    private List<Target> temp = new List<Target>();




    private Transform depthPivot;
    // Player 캐릭터
    private Target mainCharacter;

    private bool IsTarget = true;


    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.TargetMng) != (int)MngType.TargetMng)
            return;

        TargetSetting();

        if (IsTarget == false)
            return;

     

        if (mainCharacter == null)
        {
            
            for (int i = 0; i < TargetList.Count; i++)
            {

                TargetList[i].setTarget = null;
                TargetList[i].setActive(false);
            }

            IsTarget = false;
            GameDB.playerPos = null;
            GameDB.player = null;
            return;
        }

        List<Target> depthTarget = new List<Target>();

        foreach(var value in TargetList)
        {
            if(value.isdepthTarget == true)
            {
                depthTarget.Add(value);
            }
            
        }

        depthTarget.Sort(Comparison);
        depthTarget.Reverse();

        for (int i = 0; i < depthTarget.Count; ++i)
        {
            depthTarget[i].SetSortingOrder(i * 3);
        }

        
        mainCharacter.SetSortingOrder(depthTarget.Count * 3);
        



        temp.Clear();
        temp.AddRange(TargetList);
        TargetList.Clear();



        foreach (var value in temp)
        {
            if (GameDB.isEnemy == true && value.tag != "Monster")
            {
                value.setActive();
                value.enabled = false;
            }
            else TargetList.Add(value);

        }


        TargetList.Sort(Comparison);


        if (TargetList.Count == 0)
        {

            mainCharacter.setTarget = null;
            GameDB.targeting = null;
            GameDB.targetUnit = null;
            
        }
        else
        {
            if (GameDB.targetRead)
                return;
            for (int i = 0; i < TargetList.Count; i++)
            {
                TargetList[i].setTarget = GameDB.playerPos;
                if (i == 0)
                {
                    TargetList[i].setActive(true);
                    mainCharacter.setTarget = TargetList[i].getCenter;
                    GameDB.targeting = TargetList[i].getCenter;
                    Unit tempUnit = TargetList[i].GetComponent<Unit>();
                    if (tempUnit != null) GameDB.targetUnit = tempUnit; 

                }
                else TargetList[i].setActive();
            }
        }


    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }

    public override void Init()
    {
        mngType = MngType.TargetMng;
    }

    public override void OnActive()
    {
      
    }
    public override void OnDeactive()
    {
       
    }
    public override void OnGameEnable()
    {
       
    }
    public override void OnGameDisable()
    {
       
    }

    public override void SetActive(bool state)
    {
        if (state)
        {
            OnActive();
        }
        else
        {
            OnDeactive();
        }
        gameObject.SetActive(state);
    }
    public override void SetEnable(bool state)
    {
        if (state)
        {
            OnGameEnable();
        }
        else
        {
            OnGameDisable();
        }
        enabled = state;
    }
    #endregion

    int Comparison(Target left, Target right)
    {
        float leftDist = Vector3.Distance(depthPivot.position,
                            left.getCenter.position);
        float rightDist = Vector3.Distance(depthPivot.position,
                     right.getCenter.position);
        return leftDist.CompareTo(rightDist);
    }
    public void TargetSetting()
    {
        TargetList.Clear();
        TargetList.AddRange(UnitMng.Instance.GetTargetList());

       


        GameDB.isEnemy = false;
        mainCharacter = null;
        depthPivot = transform;
        depthPivot.position = Vector3.zero;

        temp.Clear();
       

        for (int i = 0; i < TargetList.Count; ++i)
        {

            TargetList[i].LookAt2D();

            if (TargetList[i].tag == "Player")
            {
                
                mainCharacter = TargetList[i];
                IsTarget = true;
                depthPivot = mainCharacter.getCenter;
                GameDB.playerPos = TargetList[i].getCenter.transform;
                
                GameDB.player = TargetList[i].GetComponent<Player>();
                
                
            }
            else
            {
                temp.Add(TargetList[i]);
            }

            if (TargetList[i].tag == "Monster")
            {
                
                GameDB.isEnemy = true;
                GameDB.targetRead = false;
            
            }

        }

     
        TargetList.Clear();
        TargetList.AddRange(temp);
        

    }



    
}
