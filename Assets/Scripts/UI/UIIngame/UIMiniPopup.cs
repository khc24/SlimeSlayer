using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIMiniPopup : BaseUI
{

    TMP_Text getGold;
    
    List<Button> buttonList = new List<Button>();


    public void Open(bool clear)
    {
       
        if (clear)
        {
           
            getGold.text = string.Format("{0:#,0}", GameDB.getGold) + "(사냥 보상)\n" + string.Format("{0:#,0}", GameDB.getGold / 2) + "(클리어 보상)";
            
            GameDB.money += (GameDB.getGold + GameDB.getGold /2);
            UIMng.Instance.SetMoney(GameDB.money);


        }
        else
        {
            
            getGold.text = string.Format("{0:#,0}", GameDB.getGold) + "(사냥 보상)";
            
            GameDB.money += GameDB.getGold;
            UIMng.Instance.SetMoney(GameDB.money);
        }

        gameObject.SetActive(true);
        
    }

    public void MoveScene()
    {
        AudioMng.Instance.PlayUI("UI_Button");

        SceneMng.Instance.EnableDelay(1.2f, SceneType.LobbyScene);

        // 1초동안 화면을 검게 만든다.
        UIMng.Instance.FadeOut(1);

        // 1초뒤에 로딩 ui를 보여준다.
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();
        Invoke("Close", 1f);
    }

    #region //추상 함수 정의 부

    public override void Init()
    {
        Transform t = transform.Find("Background/Window/GetGold");
        if(t != null) getGold = t.GetComponent<TMP_Text>();

        buttonList.AddRange(GetComponentsInChildren<Button>());

        if(buttonList.Count > 0)
        {
            foreach(var value in buttonList)
            {
                value.onClick.AddListener(MoveScene);
            }
        }

        gameObject.SetActive(false);

        isInit = true;

    }


    public override void Open()
    {
       
    }

    

    public override void Run()
    {

    }

    public override void Close()
    {
        getGold.text = "";
       
        gameObject.SetActive(false);
    }

#endregion

    
}
