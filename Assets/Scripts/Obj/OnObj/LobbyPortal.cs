using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIPath2D))]
public class LobbyPortal : Unit
{
    int uniqueID;
    int spawnID;
    int tableID;
    int LV = 0;
    //public SceneType currScene = SceneType.None;
    public AIPath2D path;


    public override void Init()
    {
        base.Init();
        unitType = UnitType.None;
        targetType = TargetType.On;

        path = GetComponent<AIPath2D>();
        if (path != null) path.Init();

        getModel.getTarget.isdepthTarget = false;

    }

    public void OnQuest()
    {
        AudioMng.Instance.PlayUI("UI_Open");

        UnitMng.Instance.Pause();

        Invoke("delayQuest", 0.1f);
    }

    private void delayQuest()
    {
        
        UIMng.Instance.Get<UIDungeon>(UIType.UIDungeon).SetActive(true);
    }

    public override void setInfo(int spawnID, int tableID, int LV)
    {

        this.spawnID = spawnID;
        this.tableID = tableID;
        this.LV = LV;


        setUpdate();
    }

    public override void setUpdate()
    {
        

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
    #endregion _추상함수목록_
}
