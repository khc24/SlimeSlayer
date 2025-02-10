using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillPop : BaseUI
{
    public SaveSkill skillInfo;

    new public TMP_Text name;
    new public TMP_Text Level;
    public TMP_Text explain;
    

    public Image icon;

    public Color maxColor = new Color(0.2f, 0.8f, 0.4f);


    public void Open(SaveSkill info)
    {
        if (info == null)
            return;

        skillInfo = info;
        
        name.text = DataManager.ToS(TableType.SKILLTABLE,info.tableID,"NAME");
        if (info.level >= 100) Level.color = maxColor;
        else Level.color = Color.black;
        Level.text = "LV. " + info.level;
        icon.sprite = DataManager.skillIcons[info.tableID];
        icon.gameObject.SetActive(true);
        explain.text = DataManager.ToS(TableType.SKILLTABLE, info.tableID, "EXPLAIN");
        explain.text += "\n\n";
        explain.text += "* 발사체 갯수 " + DataManager.ToS(TableType.SKILLTABLE, info.tableID, "NUMBER") + "\n";
        explain.text += "* 공격력 " + (DataManager.ToI(TableType.SKILLTABLE, info.tableID, "ATTACK") +
                        (info.level * DataManager.ToI(TableType.SKILLTABLE, info.tableID, "ATTACKPERCENT"))) + "%";

        if(!string.IsNullOrEmpty(DataManager.ToS(TableType.SKILLTABLE,info.tableID,"BEFORE")))
        {
            string beforStr = DataManager.ToS(TableType.SKILLTABLE, info.tableID, "BEFORE");
            int skillKey = 0;
            int skillValue = 0;
            string[] beforStr2 = beforStr.Split('@');
            int.TryParse(beforStr2[0], out skillKey);
            int.TryParse(beforStr2[1], out skillValue);

            explain.text += "\n\n 선행 스킬 : " + DataManager.ToS(TableType.SKILLTABLE, skillKey, "NAME") + " LV. " + skillValue;
        }
        

        gameObject.SetActive(true);


    }

   



    #region //추상 함수 정의부
    public override void Init()
    {
        Transform t = transform.Find("Frontground");
        name = UtilHelper.Find<TMP_Text>(t, "Name", false, true);
        Level = UtilHelper.Find<TMP_Text>(t, "Level", false, true);
        icon = UtilHelper.Find<Image>(t, "Icon", false, false);
        explain = UtilHelper.Find<TMP_Text>(t, "ExplainBack/Explain", false, true);
        


        isInit = true;

    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
        name.text = "";
        Level.text = "LV. 0";
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        explain.text = "";
        

        gameObject.SetActive(false);
    }

    #endregion

    

}

