using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShop : BaseUI
{


    UIItemBuy itemBuy;
    UICharacterBuy charBuy;

    UIItemSell itemSell;
    UICharacterSell charSell;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    ShopInputEventHandler eventHandler;

    public static TMP_Text moneyText;


    public void SetUITab()
    {

        itemBuy.SetUITab(ItemCategory.ALL);
        itemBuy.SetItemList(GameDB.GetAllItems((int)ItemBitCategory.ALL));

        itemSell.SetUITab(ItemCategory.ALL);
        itemSell.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);

        charBuy.SetUITab(Job.ALL);
        charBuy.SetItemList(GameDB.GetCharAllList((int)JobBit.ALL));
        charSell.SetUITab(Job.ALL);
        charSell.SetItemList(GameDB.GetCharList((int)JobBit.ALL));

    }



    #region //추상 함수 정의부
    public override void Init()
    {

        itemBuy = UtilHelper.Find<UIItemBuy>(transform, "UIShopBox/UIItemBuy", true, true);
        if (itemBuy != null)
        {
            if (!itemBuy.isInit)
                itemBuy.Init();

        }

        charBuy = UtilHelper.Find<UICharacterBuy>(transform, "UIShopBox/UICharacterBuy", true, true);
        if (charBuy != null)
        {
            if (!charBuy.isInit)
                charBuy.Init();
        }


        itemSell = UtilHelper.Find<UIItemSell>(transform, "UIShopBox/UIItemSell", true, true);
        if (itemSell != null)
        {
            if (!itemSell.isInit)
                itemSell.Init();
        }

        charSell = UtilHelper.Find<UICharacterSell>(transform, "UIShopBox/UICharacterSell", true, true);
        if (charSell != null)
        {

            if (!charSell.isInit)
                charSell.Init();
        }


        itemPopup = UtilHelper.Find<UIItemPopup>(transform, "UIShopBox/UIItemPopup", true, false);
        if (itemPopup != null)
            if (!itemPopup.isInit)
                itemPopup.Init();

        charPopup = UtilHelper.Find<UICharacterPopup>(transform, "UIShopBox/UICharacterPopup", true, false);
        if (charPopup != null)
            if (!charPopup.isInit)
                charPopup.Init();




        eventHandler = GetComponent<ShopInputEventHandler>();
        moneyText = UtilHelper.Find<TMP_Text>(transform, "UIShopBox/MoneyBox/MoneyText");

 

    }

    public override void Run()
    {
        if (itemBuy != null && itemBuy.isInit != false)
            itemBuy.Run();
        if (itemSell != null && itemSell.isInit != false)
            itemSell.Run();
        if (charBuy != null && charBuy.isInit != false)
            charBuy.Run();
        if (charSell != null && charSell.isInit != false)
            charSell.Run();
    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion



}
