using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoaderAsync))]
public class UnitMng : Mng<UnitMng>
{
    private CameraMove cameraMove;
    private LoaderAsync loader;
    private Dictionary<int, Unit> unitDic =
        new Dictionary<int, Unit>();

    

    private Dictionary<int, Unit> tempDic =
        new Dictionary<int, Unit>();

    // 게인신 로드 완료 후 1초 동안 플레이어가 공격할 수 없게 하는 변수

    public static bool pause = false;

    // 신의 기본 유닛이 모두 생성되었는지 체크한다.
   
    public static bool isUnitcreateComplete = false;

    public Dictionary<int, Unit> getUnitDic
    {
        get { return unitDic; }
    }

    // 프리팸 경로
    private readonly string path = "Prefab/Unit/";

  

    private static int spawnCount = 10;


    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.UnitMng) != (int)MngType.UnitMng)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {


            foreach (var value in unitDic)
            {
               
                Monster m = value.Value.GetComponent<Monster>();
                if (m != null)
                    m.currState = UnitState.Chase;
                

            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            
            foreach (var value in unitDic)
            {
                Monster m = value.Value.GetComponent<Monster>();
                if (m != null) m.currState = UnitState.Idle;

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
        mngType = MngType.UnitMng;
        loader = GetComponent<LoaderAsync>();
        cameraMove = GameObject.FindObjectOfType<CameraMove>();
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


    public  void Pause()
    {
        GameDB.isSkillOn = false;
        foreach (var value in unitDic)
        {
            value.Value.Pause();
            
        }
        pause = true;
    }
    public  void Resume()
    {
        GameDB.isSkillOn = true;
        foreach (var value in unitDic)
        {
            value.Value.Resume();
        }

        pause = false;
    }

    public int getCount
    {
        get { return unitDic.Count; }
    }


    public void ChangeUnit(int spawnID , Unit newUnit )
    {
        if(unitDic.ContainsKey(spawnID))
        {
            unitDic[spawnID] = newUnit;
        }
    }


    public int AddAsync(UnitType unitType ,int tableID, int LV, Vector3 position, Quaternion rotation)
    {
        
        TableType loadTable = TableType.None;

        switch((int)unitType)
        {
            case 0:
                {
                    loadTable = TableType.NONETABLE;
                }
                break;
            case 6:
                {
                    loadTable = TableType.PLAYERTABLE;
                }
                break;
            case 7:
                {
                    loadTable = TableType.MONSTERTABLE;
                }
                break;
            case 8:
                {
                    loadTable = TableType.NPCTABLE;
                }
                break;
            case 9:
                {
                    loadTable = TableType.ITEMTABLE;
                }
                break;
            case 10:
                {
                    loadTable = TableType.SKILLTABLE;
                }
                break;
        }
        int spawnID = 0;
        if (unitType == UnitType.Player)
        {
            spawnID = 1;
            AddAsync(unitType, spawnID, tableID, LV, path + unitType + DataManager.ToS(loadTable, tableID, "MODEL"), position, rotation);
        }
        else
        {
            spawnID = spawnCount;
            AddAsync(unitType, spawnID, tableID, LV, path + unitType + DataManager.ToS(loadTable, tableID, "MODEL"), position, rotation);
            spawnCount++;
        }
        return spawnID;
    }



    void Finished(int spawnID, int tableID, int LV, Unit unit, Vector3 position, Quaternion rotation)
    {

        
        // 비동기 로딩이 완료되었다면 캐릭터를 생성하고, 저장소에 저장
        unit = Instantiate(unit, position, rotation);
        
        unit.Init();
        unit.setInfo(spawnID, tableID, LV);
        unit.setUpdate();
     


        // 생성한 캐릭터를 등록
        if (unitDic.ContainsKey(spawnID) == false)
            unitDic.Add(spawnID, unit);


    }
    public void AddAsync(UnitType unitType, int spawnID, int tableID, int LV, string fileName, Vector3 position, Quaternion rotation)
    {
        loader.LoadAsync<Unit>(spawnID, tableID, LV, fileName, position, rotation, Finished);
    }

    // 캐릭터/스킬을 멈추는 함수 
    public void PauseAll()
    {
        foreach (var kValue in unitDic)
        {
            kValue.Value.Pause();
        }
    }
    // 캐릭터/스킬을 움직이게 하는 함수
    public void ResumeAll()
    {
        foreach (var kValue in unitDic)
        {
            kValue.Value.Resume();
        }
    }


    public void Del(int spawnID)
    {
        if (unitDic.ContainsKey(spawnID))
        {
            unitDic[spawnID].Destroy();
            unitDic.Remove(spawnID);
        }
    }

    void RemoveAll()
    {
        
        tempDic.Clear();

        foreach (var value in unitDic)
        {
            if (value.Value != null)
                tempDic.Add(value.Key, value.Value);
        }

        unitDic.Clear();

        foreach (var value in tempDic)
        {
            if (value.Value != null)
                unitDic.Add(value.Key, value.Value);
        }

    }

    public List<Unit> GetMinimapIcons()
    {
        RemoveAll();
        List<Unit> targetList = new List<Unit>();


        foreach (var value in unitDic)
        {
            if(value.Value.tag != "Player")
                targetList.Add(value.Value);
        }
        return targetList;
    }



    public void Clear()
    {
        spawnCount = 10;
        unitDic.Clear();
    }


    public bool Contains(int spawnID)
    {
        if (unitDic.ContainsKey(spawnID))
        {
            return true;
        }

        return false;
    }

    public Unit GetPlayer()
    {
        RemoveAll();

        GameDB.playerPos = null;
        foreach( var value in unitDic)
        {
            if(value.Value.unitType == UnitType.Player)
            {
                GameDB.playerPos = value.Value.getModel.getTarget.getCenter;
                cameraMove.setTarget = GameDB.playerPos;
                return value.Value;
            }
        }
        return null;
    }




    public bool targetInScreen(Vector3 tarPos)
    {
        if (Camera.main != null)
        {
            Vector3 viewPos = Camera.main.WorldToScreenPoint(tarPos);

            if (viewPos.x >= 0 && viewPos.x <= Camera.main.scaledPixelWidth && viewPos.y >= 0 && viewPos.y <= Camera.main.scaledPixelHeight)
                return true;
        }

        return false;


    }

    public List<Target> GetTargetList()
    {

        RemoveAll();
        List<Target> targetList = new List<Target>();


        foreach (var value in unitDic)
        {

            
            if (targetInScreen(value.Value.getModel.getTarget.getCenter.position) && value.Value.targetType == TargetType.On)
            {
                value.Value.getModel.getTarget.enabled = true;
                targetList.Add(value.Value.getModel.getTarget);
                value.Value.screenIn = true;
            }

            else
            {
                value.Value.getModel.getTarget.setActive(false);
                value.Value.getModel.getTarget.enabled = false;
                value.Value.screenIn = false;
            }


        }

        
            
       
        return targetList;
    }

}
