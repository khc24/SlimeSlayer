using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlMng : Mng<ControlMng>
{
private UIIngame uiIngame;
private Unit player;
//private Unit readTarget;
private CameraMove cameraMove;

    #region // Mng 추상 메소드 정의부

    

    public override void Run()
    {

        if ((GameDB.MngEnabled & (int)MngType.ControlMng) != (int)MngType.ControlMng)
            return;

        if(GameDB.player != null && GameDB.GetChar(GameDB.userInfo.GetCharUniqueID) != null)
        if(!GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).IsUpdate || !GameDB.player.GetPlayerInfo.IsUpdate)
        {
                
                if (GameDB.player.GetPlayerInfo.model != GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).model)
                {
                    // 새 모델과 이전 모델이 다르면 모델을 새로 생성한다.
                    Player p = Resources.Load<Player>("Prefab/Unit/Player" + GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).model);
                    p = Instantiate(p, GameDB.player.transform.position, GameDB.player.transform.rotation);
                    p.Init();
                    // 새로 모델에 이전 모델을 스폰 아이디를 받아 넣는다.
                    p.setInfo(GameDB.player.GetPlayerInfo.spawnID, 0, 0);

                    // 유닛 매니저가 가지고 있는 유닛 사전 목록에서 이전 모델을 지우고 새 모델을 값을 넣는다.
                    UnitMng.Instance.ChangeUnit(p.GetPlayerInfo.spawnID, p);
                    
                    Destroy(GameDB.player.gameObject);
                    GameDB.player = p;
                }

                GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).IsUpdate = true;

                if (GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).currHp <= 0)
                {
                    GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).currHp = GameDB.GetChar(GameDB.userInfo.GetCharUniqueID).lastState[2];
                }


                GameDB.player.SetPlayerInfo = GameDB.GetChar(GameDB.userInfo.GetCharUniqueID);
                CameraMove moveCamera = GameObject.FindObjectOfType<CameraMove>();
                moveCamera.setTarget = GameDB.player.transform;
                UIRPGBackground back = GameObject.FindObjectOfType<UIRPGBackground>();
                if (back != null)
                    back.player = GameDB.player.transform;

                GameDB.player.changeHpBar();
                

            }



        switch (GameDB.currSceneType)
        {
            case SceneType.None:
                {

                }
                break;
            case SceneType.LobbyScene:
                {
                    if (player == null)
                    {
                        GetPlayer();
                        return;
                    }


                    foreach (var value in UnitMng.Instance.getUnitDic)
                    {

                        value.Value.Run();
                    }

                  



                    if (uiIngame.Dir2D != Vector2.zero)
                    {
               
                        GameDB.targetRead = false;
                        GameDB.player.path.path.Clear();
                        player.Move(uiIngame.Dir2D);

                    }
                    else
                    {
                            if (GameDB.targetRead && player.getModel.getTarget.target != null)
                            {
                                player.Chase();

                                Collider2D[] coll2D = Physics2D.OverlapCircleAll(player.getModel.getTarget.getCenter.position, 0.6f);

                                foreach (var value in coll2D)
                                {

                                    Unit tempUnit = value.GetComponent<Unit>();
                                    Unit readTarget = null;

                                    if (GameDB.targetUnit != null)
                                        readTarget = GameDB.targetUnit.GetComponent<Unit>();


                                    if (tempUnit != null && readTarget != null)
                                    {

                                        if (tempUnit == readTarget)
                                        {
                                            

                                        readTarget.SendMessage("OnQuest", SendMessageOptions.DontRequireReceiver);

                                            GameDB.targetRead = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                            GameDB.targetRead = false;
                            player.Move(uiIngame.Dir2D);
                            }

                        
                    }


                }
                break;
            case SceneType.GameScene:
                {
                    if (player == null)
                    {
                        GetPlayer();
                        return;
                    }


                    int monsterCount = 0;
                    int portalCount = 0;
                    foreach(var value in UnitMng.Instance.getUnitDic)
                    {
                        
                        value.Value.Run();
                        if(value.Value.gameObject.tag == "Monster")
                            monsterCount++;
                        if (value.Value.gameObject.tag == "Portal")
                            portalCount++;

                    }

                    if (GameScene.isState && UnitMng.isUnitcreateComplete)
                    {
                        if(monsterCount == 0 && portalCount == 0 && GameScene.isClear == true && player.isDie == false)
                        {

                            AudioMng.Instance.PlayUI("UI_Clear");
                            UIMng.Instance.SetEventSystme(false);
                            UIJoystick j = GameObject.FindObjectOfType<UIJoystick>(true);
                            j.touchCansle();
                            UnitMng.isUnitcreateComplete = false;
                            GameScene.isClear = false;

                            Object g = Resources.Load("Prefab/UI/UIClear");
                            Instantiate(g);
                            UnitMng.Instance.PauseAll();
                            
                            UIMng.Instance.SetEventSystme(true);

                        }
                        else if(monsterCount == 0 && portalCount >= 1 && player.isDie == false)
                        {
                            AudioMng.Instance.PlayUI("UI_Open");
                            UnitMng.isUnitcreateComplete = false;

                            Door door = FindObjectOfType<Door>(true);

                            if(door != null)
                            {
                                door.coll2D.enabled = false;
                                door.animator.SetTrigger("Open");
                            }

                            AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallCreate");
                            foreach(var value in UnitMng.Instance.getUnitDic)
                            {
                                if (value.Value.gameObject.tag == "Portal")
                                    value.Value.targetType = TargetType.On;
                            }
                        }
                        
                        else if(player.isDie)
                        {
                            AudioMng.Instance.PlayUI("UI_Fail");
                            UIMng.Instance.SetEventSystme(false);
                            UIJoystick j = GameObject.FindObjectOfType<UIJoystick>(true);
                            j.touchCansle();

                            UnitMng.isUnitcreateComplete = false;
                            GameScene.isClear = false;

                            Object g = Resources.Load("Prefab/UI/UIFail");
                            Instantiate(g);
                            UnitMng.Instance.PauseAll();
                            
                            
                            UIMng.Instance.SetEventSystme(true);

                            player.isDie = true;

                            
                        }
                    }



                    if (uiIngame.Dir2D != Vector2.zero)
                    {
                        GameDB.targetRead = false;
                        player.Move(uiIngame.Dir2D);

                    }
                    else
                    {
                        if (GameDB.isEnemy == true)
                        {
                            GameDB.targetRead = false;
                            player.Move(uiIngame.Dir2D);
                        
                            if(UnitMng.pause == false)
                                player.Attack();
                        }
                        else
                        {


                            if (GameDB.targetRead && player.getModel.getTarget.target != null)
                            {
                                player.Chase();

                                Collider2D[] coll2D = Physics2D.OverlapCircleAll(player.getModel.getTarget.getCenter.position, 0.6f);

                                foreach (var value in coll2D)
                                {
                                   
                                    Unit tempUnit = value.GetComponent<Unit>();
                                    Unit readTarget = null;
                                    
                                    if (GameDB.targetUnit != null)
                                        readTarget = GameDB.targetUnit.GetComponent<Unit>();

                                    
                                    if (tempUnit != null && readTarget != null)
                                    {
                                       
                                        if (tempUnit == readTarget)
                                        {
                                            readTarget.SendMessage("OnQuest", SendMessageOptions.DontRequireReceiver);
                                            GameDB.targetRead = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                GameDB.targetRead = false;
                                player.Move(uiIngame.Dir2D);
                            }

                        }
                    }

                }
                break;
            case SceneType.BossScene:
                {

                }
                break;
        }
    }

    public override void FixRun()
    {
        
    }

    public override void LateRun()
    {
        if ((GameDB.MngEnabled & (int)MngType.ControlMng) != (int)MngType.ControlMng)
            return;


        switch (GameDB.currSceneType)
        {
            case SceneType.None:
                {

                }
                break;
            case SceneType.LobbyScene:
                {
                    if (player == null)
                        return;

                    cameraMove.LateRun();
                }
                break;
            case SceneType.GameScene:
                {
                    if (player == null)
                        return;

                    cameraMove.LateRun();


                }
                break;
            case SceneType.BossScene:
                {

                }
                break;
        }
    }

    public override void Init()
    {
        mngType = MngType.ControlMng;
        uiIngame = UIMng.Instance.Get<UIIngame>(UIType.UIIngame);
       
    }

    public override void OnActive()
    {
      
        
    }
    public override void OnDeactive()
    {
       
        
    }
    public override void OnGameEnable()
    {
        
        
        GetPlayer();
        setCamer();


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


    public void setCamer()
{

    cameraMove = FindObjectOfType<CameraMove>();
    if (player != null && cameraMove != null)
        cameraMove.setTarget = player.transform;
}

public void GetPlayer()
{
    player = UnitMng.Instance.GetPlayer();

}



}
