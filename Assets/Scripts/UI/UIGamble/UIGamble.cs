using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGamble : BaseUI
{
    UIItemGamblePopup itemGamblePopup;
    UICharGamblePopup charGamblePopup;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    UIGambler gambler;

    GambleInputEventHandler eventHandler;
    public static TMP_Text moneyText;

 

    #region //추상 함수 정의부
    public override void Init()
    {

        itemGamblePopup = UtilHelper.Find<UIItemGamblePopup>(transform, "UIGambleBox/UIItemGamblePopup", true, false);
        charGamblePopup = UtilHelper.Find<UICharGamblePopup>(transform, "UIGambleBox/UICharGamblePopup", true, false); 

        itemPopup = UtilHelper.Find<UIItemPopup>(transform, "UIGambleBox/UIItemPopup", true, false);
        if (itemPopup != null)
            if (!itemPopup.isInit)
                itemPopup.Init();

        charPopup = UtilHelper.Find<UICharacterPopup>(transform, "UIGambleBox/UICharacterPopup", true, false);
        if (charPopup != null)
            if (!charPopup.isInit)
                charPopup.Init();

        gambler = UtilHelper.Find<UIGambler>(transform, "UIGambleBox/UIGambler", true, true);
        if (gambler != null)
            if (!gambler.isInit)
                gambler.Init();




        eventHandler = GetComponent<GambleInputEventHandler>();
        moneyText = UtilHelper.Find<TMP_Text>(transform, "UIGambleBox/MoneyBox/MoneyText");


    }

    public override void Run()
    {
        if(eventHandler != null)
            eventHandler.Run();
    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion
}
