using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : BaseUI
{
    Button saveBtn;
    Button loadBtn;
    Button giveUpBtn;
    Button titleMoveBtn;
    public static Slider backSlider;
    public static Slider effectSlider;
    

    public void SaveBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameDB.Save("PlayerInfo.json");
    }

    public override void SetActive(bool path)
    {
        base.SetActive(path);

        if(GameDB.currSceneType == SceneType.TitleScene)
        {
            saveBtn.gameObject.SetActive(true);
            loadBtn.gameObject.SetActive(true);
            giveUpBtn.gameObject.SetActive(false);
            titleMoveBtn.gameObject.SetActive(true);
        }
        else if(GameDB.currSceneType == SceneType.LobbyScene)
        {
            saveBtn.gameObject.SetActive(false);
            loadBtn.gameObject.SetActive(false);
            giveUpBtn.gameObject.SetActive(false);
            titleMoveBtn.gameObject.SetActive(true);
        }
        else if (GameDB.currSceneType == SceneType.GameScene)
        {
            saveBtn.gameObject.SetActive(false);
            loadBtn.gameObject.SetActive(false);
            giveUpBtn.gameObject.SetActive(true);
            titleMoveBtn.gameObject.SetActive(false);
        }
    }

    public void TitleMoveBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        SceneMng.Instance.EnableDelay(1f, SceneType.TitleScene);

        UIMng.Instance.FadeOutDelay(0f);

      

    }

    public void GiveUpBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameScene.isState = false;
        SceneMng.Instance.EnableDelay(1.0f, SceneType.LobbyScene);

        
        UIMng.Instance.FadeOut(1);

       
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();
        gameObject.SetActive(false);

    }


    public void LoadBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameDB.Load("PlayerInfo.json");
        GameDB.player.GetPlayerInfo.IsUpdate = false;
      
        UIInventory i = GameObject.FindObjectOfType<UIInventory>(true);
        i.SetUITab(ItemCategory.ALL);
        i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);

        UICharacterInventory cinven = GameObject.FindObjectOfType<UICharacterInventory>(true);
        cinven.SetUITab(Job.ALL);
        cinven.SetItemList(GameDB.GetCharList((int)JobBit.ALL));
  
    }

    #region //추상 클래스 정의부
    public override void Init()
    {
        Transform t = transform.Find("MenuBox/VerticalLayout/SaveBox");
        
        if(t != null)
        {
            saveBtn = t.GetComponent<Button>();
            saveBtn.onClick.AddListener(SaveBtn);

        }
        t = transform.Find("MenuBox/VerticalLayout/LoadBox");
        if (t!= null)
        {
            loadBtn = t.GetComponent<Button>();
            loadBtn.onClick.AddListener(LoadBtn);
        }

        t = transform.Find("MenuBox/VerticalLayout/TitleMoveBox");
        if (t != null)
        {
            titleMoveBtn = t.GetComponent<Button>();
            titleMoveBtn.onClick.AddListener(TitleMoveBtn);
        }

        t = transform.Find("MenuBox/VerticalLayout/GiveUpBox");
        if (t != null)
        {
            giveUpBtn = t.GetComponent<Button>();
            giveUpBtn.onClick.AddListener(GiveUpBtn);
        }

        t = transform.Find("MenuBox/SoundBox/BackSlider");
        if (t != null)
        {
            backSlider = t.GetComponent<Slider>();
            if(PlayerPrefs.HasKey("backValue"))
            {
                backSlider.value = PlayerPrefs.GetFloat("backValue");
            }
        }

            

        t = transform.Find("MenuBox/SoundBox/EffectSlider");
        if (t != null)
        {
            effectSlider = t.GetComponent<Slider>();
            if (PlayerPrefs.HasKey("effectValue"))
            {
                effectSlider.value = PlayerPrefs.GetFloat("effectValue");
            }
        }
            



    }
    public override void Run()
    {
        if (gameObject.activeSelf == false)
            return;

        if(effectSlider != null)
            PlayerPrefs.SetFloat("effectValue", effectSlider.value);
        if (backSlider != null)
        {
            PlayerPrefs.SetFloat("backValue", backSlider.value);
            AudioMng.Instance.SetBackVolume(backSlider.value);
        }
            

        

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

}
