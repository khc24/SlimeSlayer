using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public UnitType unitType = UnitType.None;
    public SceneType sceneType = SceneType.None;
    public string unitName = "";
    public int tableID = 0;
    public int spawnID = -1;
    public int LV = 1;

    public void Create()
    {
      

        switch(unitType)
        {
            case UnitType.None:
                {
                    unitName = DataManager.ToS(TableType.NONETABLE, tableID, "NAME");
                    spawnID = UnitMng.Instance.AddAsync(unitType, tableID, LV, transform.position, transform.rotation);
                }
                break;
            case UnitType.Player:
                {
                   PlayerInfo info =  GameDB.GetChar(GameDB.userInfo.GetCharUniqueID);
                    
                    spawnID = UnitMng.Instance.AddAsync(unitType, info.tableID, info.level, transform.position, transform.rotation);
                }
                break;
            case UnitType.Monster:
                {
                    spawnID = UnitMng.Instance.AddAsync(unitType, tableID, LV, transform.position, transform.rotation);
                }
                break;
            case UnitType.NPC:
                {
                    spawnID = UnitMng.Instance.AddAsync(unitType, tableID, LV, transform.position, transform.rotation);
                }
                break;
            case UnitType.Item:
                {
                    spawnID = UnitMng.Instance.AddAsync(unitType, tableID, LV, transform.position, transform.rotation);
                }
                break;
        }
       
    }

   


    public bool IsCreated
    {
        get
        {
            if(unitName == "Portal")
            {
                if(UnitMng.Instance.Contains(spawnID))
                {
                    UnitMng.Instance.getUnitDic[spawnID].sceneType = sceneType;
                }
            }

            return UnitMng.Instance.Contains(spawnID);
        }
    }
}
