using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIIngame : BaseUI
{
    private UIJoystick joystick;
    private UIPlayerHP playerHP;
   
    private UIInfoButton infoButton;
    private UIMinimap miniMap;
    private UIHpMng hpMng;
    private Transform view;
    public TMP_Text getGold;
    public UIMiniPopup miniPopup;


    public Sprite SetBackImage
    {
        set { if (miniMap != null) miniMap.SetBackImage = value; }
    }
    public void SetGetGold(int gold)
    {
        getGold.text = string.Format("{0:#,0}", gold) + " GOLD";
    }

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {
        joystick = GetComponentInChildren<UIJoystick>(true);
        if (joystick != null)
            joystick.Init();
        

        infoButton = GetComponentInChildren<UIInfoButton>(true);
        if (infoButton != null)
            infoButton.Init();

        playerHP = GetComponentInChildren<UIPlayerHP>(true);
        if (playerHP != null)
        {
            playerHP.Init();
            
        }
        Transform t;
        miniMap = GetComponentInChildren<UIMinimap>(true);
        if (miniMap != null)
        {
            t = transform.Find("UIMiniMap/Mask/Player/ViewRoot");
            if (t != null)
                view = t;
            miniMap.Init();   
        }

        hpMng = GetComponentInChildren<UIHpMng>(true);

        t = transform.Find("GetGold");
        if (t != null)
        {
            getGold = t.GetComponent<TMP_Text>();
            getGold.gameObject.SetActive(false);

        }

        miniPopup = GetComponentInChildren<UIMiniPopup>(true);
        if (miniPopup != null) miniPopup.Init();

    }

    public void Open(bool clear)
    {
        if(miniPopup != null) miniPopup.Open(clear);
    }



    public UIHp UIHpCreate(Transform pivot, ShareInfo info)
    {
        return hpMng.UIHpCreate(pivot, info);
    }

    public override void Run()
    {
        if(gameObject.activeSelf != false)

        miniMap.Run();
        hpMng.Run();

       if ( GameDB.player != null)
        {
            float hp = GameDB.player.GetHP();
            
              playerHP.setValue(hp);
        }
    }
    public override void Open()
    {
    }
    public override void Close()
    {
    }
    #endregion BASEUI로부터 상속받은 함수목록

    public void touchCansle()
    {
        joystick.touchCansle();
    }


    public void modeChange()
    {
        if(GameDB.currSceneType == SceneType.GameScene)
        {
            playerHP.SetActive(true);
            infoButton.SetActive(true);
            getGold.gameObject.SetActive(true);
            
            
        }
        else if (GameDB.currSceneType == SceneType.BossScene)
        {
            playerHP.SetActive(true);
            infoButton.SetActive(true);
            getGold.gameObject.SetActive(false);
        }
        else if (GameDB.currSceneType == SceneType.LobbyScene)
        {
            playerHP.SetActive(false);
            infoButton.SetActive(true);
            getGold.gameObject.SetActive(false);
        }
        else if (GameDB.currSceneType == SceneType.TitleScene)
        {
            playerHP.SetActive(false);
            infoButton.SetActive(false);
            getGold.gameObject.SetActive(false);
        }
    }


 

    public Vector2 Dir2D
    {
        get
        {
            if (joystick == null)
                return Vector2.zero;

            if(joystick.Dir2D != Vector2.zero && view != null)
            {
                float angleZ = Mathf.Atan2(
                        joystick.Dir2D.y,
                        joystick.Dir2D.x) * Mathf.Rad2Deg;
               
                view.eulerAngles = new Vector3(0, 0, angleZ);
            }
            return joystick.Dir2D;
        }
    }

    
    public void MinimapSetUpdate()
    {
        miniMap.SetUpdate();
    }

    public void MinimapSet(bool state = false)
    {
        miniMap.SetActive(state);
    }

}
