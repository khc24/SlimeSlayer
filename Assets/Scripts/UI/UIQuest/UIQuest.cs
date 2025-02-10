using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIQuest : BaseUI
{

    UIQuestContents questContents;

    UIQuestPopup questPopup;

    QuestInputEventHandler eventHandler;
    public static TMP_Text moneyText;

    public void SetQuestList()
    {
        if (questContents != null) questContents.SetQuestList();
    }

    #region //추상 함수 정의부
    public override void Init()
    {

        questContents = UtilHelper.Find<UIQuestContents>(transform, "UIQuestBox/UIQuestContents", true, true);
        if (questContents != null)
            if (!questContents.isInit)
                questContents.Init();

        questPopup = UtilHelper.Find<UIQuestPopup>(transform, "UIQuestBox/UIQuestPopup", true, false);
        if (questContents != null)
            if (!questContents.isInit)
                questContents.Init();

        eventHandler = GetComponent<QuestInputEventHandler>();
        if (eventHandler != null) eventHandler.Init();

        moneyText = UtilHelper.Find<TMP_Text>(transform, "UIQuestBox/MoneyBox/MoneyText");
        
    }

    public override void Run()
    {
 
        if (questContents != null)
            questContents.Run();
    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

    
}
