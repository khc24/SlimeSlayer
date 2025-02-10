using System.Collections;
using System.Collections.Generic;
using UnityEngine;







[System.Serializable]
public class NPCInfo
{
    public int spawnID;
    public int tableID;
    public int level;
    public string name;

}


[RequireComponent(typeof(AIPath2D))]
public class NPC : Unit
{




    
    public AIPath2D path;



    private bool damageState = false;
    private float damageElap = 0;
    private float damageTime = 1;
    private bool isDead = false;

    UIDialog dialog;

    [SerializeField]
    protected NPCInfo npcInfo = new NPCInfo();


   
    

    public override void setInfo(int spawnID, int tableID, int LV)
    {

        npcInfo.spawnID = spawnID;
        npcInfo.tableID = tableID;
        npcInfo.name = DataManager.ToS(TableType.NPCTABLE, tableID, "NAME");
        
    }

    

    public override void setUpdate()
    {
        dialog = UIMng.Instance.UIDialogCreate(dialogPivot, npcInfo.name);
        

    }



    public override void Init()
    {

        base.Init();
        unitType = UnitType.NPC;
        targetType = TargetType.On;

        path = GetComponent<AIPath2D>();
        if (path != null) path.Init();

        getModel.getTarget.isdepthTarget = false;

        dialogPivot = transform.Find("DialogPivot");

        

    }



    


   

    #region _추상함수목록_
    public override void Idle()
    {

    }
    public override void Move()
    {

    }

    public override void Move(Vector2 dir)

    {

    }

    public override void Attack()
    {

    }

    public override void Attack(Vector2 dir)
    {

    }

    public override void Patrol()
    {

    }

    public override void Chase()
    {

    }

    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }

    #endregion 기능 함수 목록


}
