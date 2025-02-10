using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UITitle : BaseUI
{
    
    Button newGameBtn;
    Button continueBtn;
    Button saveBtn;
    Button loadBtn;
    Button closeBtn;

    bool isStart = true;
    bool isStart2 = true;
    public Animator ani;
    Transform shield;
    RectTransform imagePos;
    
    RectTransform menuPos;

    int newID = 0;
   
    public override void OnDeactive()
    {
        shield.gameObject.SetActive(true);
    }

    public override void OnActive()
    {
        if(isStart)
        {
            continueBtn.gameObject.SetActive(false);
            saveBtn.gameObject.SetActive(false);
            isStart = false;
        }
        else
        {
            continueBtn.gameObject.SetActive(true);
            saveBtn.gameObject.SetActive(true);
        }

        if(File.Exists(GameDB.GetPath("PlayerInfo.json")))
        {
            loadBtn.gameObject.SetActive(true);
        }
        else
        {
            loadBtn.gameObject.SetActive(false);
        }
                

    }

    public void NewGameBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameDB.money = 10000;
        UIMng.Instance.SetMoney(GameDB.money);

        //if(!isStart2)
        //{
           
            
        //}
        GameDB.charDic.Clear();
        GameDB.itemDic.Clear();
        GameDB.currQuestDic.Clear();

        if (GameDB.charDic.Count <= 0)
        {
            GameDB.userInfo.uniqueCount = 500;

            List<SaveSkill> tempSaveSkillList = new List<SaveSkill>();
            string skillStr = DataManager.ToS(TableType.PLAYERTABLE, 1, "SKILLLIST");
            string[] skillList = skillStr.Split('|');

            for (int skillNum = 0; skillNum < skillList.Length; skillNum++)
            {
                SaveSkill tempSaveSkill = new SaveSkill();
                string[] eachSkill = skillList[skillNum].Split('@');
                int tempInt = 0;
                if (int.TryParse(eachSkill[0], out tempInt))
                {
                    tempSaveSkill.tableID = tempInt;

                    if (int.TryParse(eachSkill[1], out tempInt))
                    {
                        tempSaveSkill.level = tempInt;
                        tempSaveSkillList.Add(tempSaveSkill);
                    }
                }
            }

            GameDB.SetCharInfo(1, 1, GameDB.userInfo.uniqueCount++, true, new int[] { 0, 0, 0 }, tempSaveSkillList);
            
            GameDB.SetCharInfoUpdate(GameDB.userInfo.uniqueCount - 1);
            GameDB.userInfo.jobType = GameDB.charDic[GameDB.userInfo.uniqueCount - 1].jobType;
            GameDB.userInfo.SetCharUniqueID = GameDB.charDic[GameDB.userInfo.uniqueCount - 1].uniqueID;

         
        }


        SceneMng.Instance.EnableDelay(1f, SceneType.LobbyScene);

        UIMng.Instance.FadeOutDelay(0f);

        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
   
    }

    public void ContinueBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        SceneMng.Instance.EnableDelay(1f, SceneType.LobbyScene);

        UIMng.Instance.FadeOutDelay(0f);

        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
    }

    public void SaveBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameDB.Save("PlayerInfo.json");
    }


    public void LoadBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        GameDB.Load("PlayerInfo.json");
        
       

        UIInventory i = GameObject.FindObjectOfType<UIInventory>(true);
        i.SetUITab(ItemCategory.ALL);
        i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);

        UICharacterInventory cinven = GameObject.FindObjectOfType<UICharacterInventory>(true);
        cinven.SetUITab(Job.ALL);
        cinven.SetItemList(GameDB.GetCharList((int)JobBit.ALL));

        SceneMng.Instance.EnableDelay(1f, SceneType.LobbyScene);

        UIMng.Instance.FadeOutDelay(0f);

        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);


    }

    public void CloseBtn()
    {
        AudioMng.Instance.PlayUI("UI_Button");

        Application.Quit();
    }


    public void PosChange()
    {
        imagePos.anchoredPosition = new Vector2(0, -400);
        menuPos.anchoredPosition = new Vector2(-1200, -200);
    }


    #region //추상 클래스 정의부
    public override void Init()
    {

       
        
        VerticalLayoutGroup v = GetComponentInChildren<VerticalLayoutGroup>(true);
        ani = GetComponentInChildren<Animator>();
        if (v != null)
        {

            newGameBtn = UtilHelper.FindButton(v.transform, "NewGameBox", NewGameBtn);
            continueBtn = UtilHelper.FindButton(v.transform, "ContinueBox", ContinueBtn);
            saveBtn = UtilHelper.FindButton(v.transform, "SaveBox", SaveBtn); 
            loadBtn = UtilHelper.FindButton(v.transform, "LoadBox", LoadBtn);
            closeBtn = UtilHelper.FindButton(v.transform, "CloseBox", CloseBtn);
            shield = transform.Find("UILogoImage/TouchShield");
            Transform t = transform.Find("UILogoImage/Text (TMP)/Image");
            imagePos = t.GetComponent<RectTransform>();
            
            t = transform.Find("UILogoImage/Text (TMP)/MenuBox");
            menuPos = t.GetComponent<RectTransform>();
            


        }






    }
    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

}
