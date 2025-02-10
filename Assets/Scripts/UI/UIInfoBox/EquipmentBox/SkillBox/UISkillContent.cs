using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISkillContent : BaseUI
{
    SaveSkill skillInfo;
    UISkillSlot skillSolot;
    
    TMP_Text skillName;
    TMP_Text skillLevel;

    Image iconImage;
    Image shadow;
    Image tabGuard;

    Button skillUp;
    Button skillOpen;
    Button skillClose;

    public Color maxColor = new Color(0.2f, 0.8f, 0.4f);

    private System.Action<SaveSkill> skillUpDelegate;
    

    public void SkillUpOnClick()
    {
        if (skillUpDelegate != null)
        {
            if (skillInfo != null)
                skillUpDelegate(skillInfo);
        }
    }

    public void SkillOpenOnClick()
    {
        if (GameDB.player == null || skillInfo == null)
            return;

        if(skillInfo.tableID == 5 || skillInfo.tableID == 8)
        {
            GameDB.player.GetPlayerInfo.isSkill1 = true;
        }
        else if (skillInfo.tableID == 6 || skillInfo.tableID == 9)
        {
            GameDB.player.GetPlayerInfo.isSkill2 = true;
        }
        else if (skillInfo.tableID == 7 || skillInfo.tableID == 10)
        {
            GameDB.player.GetPlayerInfo.isSkill3 = true;
        }

        skillClose.gameObject.SetActive(true);
        
    }

    public void SkillCloseOnClick()
    {
        if (GameDB.player == null || skillInfo == null)
            return;

        if (skillInfo.tableID == 5 || skillInfo.tableID == 8)
        {
            GameDB.player.GetPlayerInfo.isSkill1 = false;
        }
        else if (skillInfo.tableID == 6 || skillInfo.tableID == 9)
        {
            GameDB.player.GetPlayerInfo.isSkill2 = false;
        }
        else if (skillInfo.tableID == 7 || skillInfo.tableID == 10)
        {
            GameDB.player.GetPlayerInfo.isSkill3 = false;
        }

        skillClose.gameObject.SetActive(false);

    }


    public void SetSkillUpDelegate(System.Action<SaveSkill> function)
    {
        skillUpDelegate = function;
    }


    public void SetPointerEnterDelegate(System.Action<SaveSkill> function)
    {
        if (skillSolot != null) skillSolot.SetPointerEnterDelegate(function);
    }

    public void SetPointerExitDelegate(System.Action<SaveSkill> function)
    {
        if (skillSolot != null) skillSolot.SetPointerExitDelegate(function);
    }

    public void SetInfo(SaveSkill info)
    {
        if (info == null)
        {

            skillInfo = info;
            if (skillSolot != null) skillSolot.SetInfo(info);

            skillName.text = "";
            
            skillLevel.text = "LV. " + 0;

            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
            skillUp.interactable = false;
            skillOpen.interactable = false;
            skillClose.interactable = false;
            shadow.gameObject.SetActive(false);
            
            return;
        }

        skillInfo = info;

        if (GameDB.player != null)
        {
            skillOpen.interactable = true;
            skillClose.interactable = true;

            if (skillInfo.tableID == 5 || skillInfo.tableID == 8)
            {
                if(GameDB.player.GetPlayerInfo.isSkill1 == true)
                {
                    skillClose.gameObject.SetActive(true);
                }
                else
                {
                    skillClose.gameObject.SetActive(false);
                }
            }
            else if (skillInfo.tableID == 6 || skillInfo.tableID == 9)
            {
                if (GameDB.player.GetPlayerInfo.isSkill2 == true)
                {
                    skillClose.gameObject.SetActive(true);
                }
                else
                {
                    skillClose.gameObject.SetActive(false);
                }
            }
            else if (skillInfo.tableID == 7 || skillInfo.tableID == 10)
            {
                if (GameDB.player.GetPlayerInfo.isSkill3 == true)
                {
                    skillClose.gameObject.SetActive(true);
                }
                else
                {
                    skillClose.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            skillOpen.interactable = false;
            skillClose.interactable = false;
        }

        int maxLevel = DataManager.ToI(TableType.SKILLTABLE, info.tableID, "MAXLEVEL");

        
        if (skillSolot != null) skillSolot.SetInfo(info);

        skillName.text = DataManager.ToS(TableType.SKILLTABLE, skillInfo.tableID, "NAME");



        if (skillInfo.level >= maxLevel) skillLevel.color = maxColor;
        else skillLevel.color = Color.black;
        skillLevel.text = "LV. "  + skillInfo.level;

        
        iconImage.sprite = DataManager.skillIcons[skillInfo.tableID];
        iconImage.gameObject.SetActive(true);

        
            if(GameDB.currSceneType != SceneType.GameScene)
            {
             string beforStr = DataManager.ToS(TableType.SKILLTABLE, info.tableID, "BEFORE");

             


                if(skillInfo.level >= maxLevel)
                 {
                    skillUp.interactable = false;
                 }
                else if(string.IsNullOrEmpty(beforStr))
                {
                    skillUp.interactable = true;
                }
                else
                {
                int skillKey = 0;
                int skillValue = 0;
                    string[] beforStr2 = beforStr.Split('@');
                int.TryParse(beforStr2[0], out skillKey);
                int.TryParse(beforStr2[1], out skillValue);

                foreach (var value in GameDB.player.GetPlayerInfo.skillList)
                {
                    if(value.tableID == skillKey)
                        if(value.level >= skillValue)
                            skillUp.interactable = true;
                }

            }
                
            }
            else
            {
                skillUp.interactable = false;
            }
        

               if(GameDB.player != null)
                {
                if (GameDB.player.GetPlayerInfo.skillPoint <= 0)
                skillUp.interactable = false;
                }

    
    }

    public void SetRayCast(bool path, SaveSkill saveSkill = null)
    {
        if (skillSolot != null) skillSolot.SetRayCast(path, saveSkill);
    }

    public void SetTabGuard(bool path)
    {
        if (tabGuard != null) tabGuard.gameObject.SetActive(path);
    }


    #region //추상 함수 정의부

    public override void Init()
    {
        skillSolot = GetComponentInChildren<UISkillSlot>(true);
        skillSolot.Init();

        skillName = UtilHelper.Find<TMP_Text>(transform, "Name");
        skillLevel = UtilHelper.Find<TMP_Text>(transform, "LevelText");

        iconImage = UtilHelper.Find<Image>(transform, "Icon");
        shadow = UtilHelper.Find<Image>(transform, "Shadow");
        tabGuard = UtilHelper.Find<Image>(transform, "TabGuard", false, false);


        skillUp = UtilHelper.FindButton(transform, "SkillUp", SkillUpOnClick);
        skillOpen = UtilHelper.FindButton(transform, "SkillOpen", SkillOpenOnClick);
        skillClose = UtilHelper.FindButton(transform, "SkillClose", SkillCloseOnClick);

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
