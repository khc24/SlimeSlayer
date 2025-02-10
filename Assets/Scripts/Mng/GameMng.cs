using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 유일하게 업데이트 함수를 사용하는 최고위 함수
public class GameMng : Mng<GameMng>
{

    Dictionary<int, MonoBehaviour> MngDic = new Dictionary<int, MonoBehaviour>();

    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.GameMng) != (int)MngType.GameMng)
            return;
    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }

    public override void Init()
    {
        mngType = MngType.GameMng;
        
    }


    public override void OnActive()
    {
       // GameDB.MngEnabled += (int)MngType.GameMng;
    }
    public override void OnDeactive()
    {
       // GameDB.MngEnabled -= (int)MngType.GameMng;
    }
    public override void OnGameEnable()
    {
        //GameDB.MngEnabled += (int)MngType.GameMng;
    }
    public override void OnGameDisable()
    {
        //GameDB.MngEnabled -= (int)MngType.GameMng;
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


    public void get()
    {
        
        DataManager.Load(TableType.PLAYERTABLE);
        DataManager.Load(TableType.MONSTERTABLE);
        DataManager.Load(TableType.ITEMTABLE);
        DataManager.Load(TableType.SKILLTABLE);
        DataManager.Load(TableType.NPCTABLE);
        DataManager.Load(TableType.QUESTTABLE);
        DataManager.Load(TableType.QUESTTITLETABLE);
        DataManager.Load(TableType.NONETABLE);
        QuestDataManager.LoadLowAll();

         Sprite[] icons = Resources.LoadAll<Sprite>("Sprite/Effect/SkillIcons");

        for(int iconsNum = 0; iconsNum < icons.Length; iconsNum++)
        {
            int keyNum = iconsNum + 1;
            if (!DataManager.skillIcons.ContainsKey(keyNum))
                DataManager.skillIcons.Add(keyNum, icons[iconsNum]);
        }
        


        GameDB.itemAllUpdate();
        GameDB.charAllUpdate();
        



        MngDic.Add((int)MngType.GameMng, GameMng.Instance);
        MngDic.Add((int)MngType.UIMng,UIMng.Instance);
        MngDic.Add((int)MngType.SceneMng, SceneMng.Instance);
        MngDic.Add((int)MngType.UnitMng, UnitMng.Instance);
        MngDic.Add((int)MngType.TargetMng, TargetMng.Instance);
        MngDic.Add((int)MngType.AIPathMng, AIPathMng.Instance);
        MngDic.Add((int)MngType.ControlMng, ControlMng.Instance);
        MngDic.Add((int)MngType.AudioMng, AudioMng.Instance);

        if (GameDB.charDic.Count <= 0)
        {

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

                GameDB.SetCharInfo(1,1, GameDB.userInfo.uniqueCount++,true,new int[] {0,0,0} , tempSaveSkillList);
                GameDB.SetCharInfoUpdate(GameDB.userInfo.uniqueCount - 1);
                GameDB.userInfo.jobType = GameDB.charDic[GameDB.userInfo.uniqueCount - 1].jobType;
                GameDB.userInfo.SetCharUniqueID = GameDB.charDic[GameDB.userInfo.uniqueCount - 1].uniqueID;

                UIInfoBox box = UIMng.Instance.Get<UIInfoBox>(UIType.UIInfoBox);
                box.EquipItem(GameDB.GetChar(GameDB.userInfo.GetCharUniqueID));
        }


        SceneMng.Instance.AddScene<LobbyScene>(SceneType.LobbyScene);
        SceneMng.Instance.AddScene<GameScene>(SceneType.GameScene);
        SceneMng.Instance.AddScene<BossScene>(SceneType.BossScene);
        SceneMng.Instance.AddScene<TitleScene>(SceneType.TitleScene);
       
        ControlMng.Instance.SetEnable(false);


        SceneMng.Instance.EnableDelay(1f, SceneType.TitleScene);

        UIMng.Instance.FadeOutDelay(0f);
        UIMng.Instance.SetMoney(GameDB.money);
    }

    public void selectRun(int bitNumber)
    {
        if ((bitNumber & 1) == 1)
            GameMng.Instance.Run();
        if ((bitNumber & 2) == 2)
            UIMng.Instance.Run();
        if ((bitNumber & 4) == 4)
            SceneMng.Instance.Run();
        if ((bitNumber & 8) == 8)
            UnitMng.Instance.Run();
        if ((bitNumber & 16) == 16)
            TargetMng.Instance.Run();
        if ((bitNumber & 32) == 32)
            AIPathMng.Instance.Run();
        if ((bitNumber & 64) == 64)
            ControlMng.Instance.Run();
        if ((bitNumber & 128) == 128)
            AudioMng.Instance.Run();
    }

    public void selectLateRun(int bitNumber)
    {
        if ((bitNumber & 1) == 1)
            GameMng.Instance.LateRun();
        if ((bitNumber & 2) == 2)
            UIMng.Instance.LateRun();
        if ((bitNumber & 4) == 4)
            SceneMng.Instance.LateRun();
        if ((bitNumber & 8) == 8)
            UnitMng.Instance.LateRun();
        if ((bitNumber & 16) == 16)
            TargetMng.Instance.LateRun();
        if ((bitNumber & 32) == 32)
            AIPathMng.Instance.LateRun();
        if ((bitNumber & 64) == 64)
            ControlMng.Instance.LateRun();
        if ((bitNumber & 128) == 128)
            AudioMng.Instance.LateRun();
    }

    public void enableCheck()
    {
        foreach(var value in MngDic)
        {
            if (value.Value.isActiveAndEnabled == true)
            {
                MngType tempMngType = (MngType)value.Key;
                enableChang(tempMngType,true);
            }
            else
            {

                MngType tempMngType = (MngType)value.Key;
                
                enableChang(tempMngType, false);
            }
                
        }
    }


    // Update is called once per frame
    void Update()
    {

        enableCheck();
        selectRun(255);
       
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        selectLateRun(64);
    }

}
