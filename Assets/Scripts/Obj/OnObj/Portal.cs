using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIPath2D))]
public class Portal : Unit
{
    int uniqueID;
    int spawnID;
    int tableID;
    int LV = 0;
   
    public AIPath2D path;


    public override void Init()
    {
        base.Init();
        unitType = UnitType.None;

        if (GameDB.currSceneType == SceneType.GameScene)
            targetType = TargetType.None;
        else
            targetType = TargetType.On;

        path = GetComponent<AIPath2D>();
        if (path != null) path.Init();

        getModel.getTarget.isdepthTarget = false;

    }

    public  void OnQuest()
    {
        AudioMng.Instance.PlayUI("UI_Open");

        if(sceneType != SceneType.None)
        {
            if(sceneType == SceneType.GameScene)
            {
               

                GameScene.stageLv = LV;
                SceneMng.Instance.EnableDelay(1.0f, sceneType, LV);
            }
            else
            {
                SceneMng.Instance.EnableDelay(1.0f, sceneType, LV);
            }
            
        }
        

      
        UIMng.Instance.FadeOut(1);

        
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();
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
        gameObject.tag = DataManager.ToS(TableType.NONETABLE, tableID, "TAG");

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
