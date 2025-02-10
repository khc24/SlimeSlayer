using TMPro;

public class UIUpgradeShop : BaseUI
{


    UIItemUpgradeInventory upItemInventory;
    UICharacterUpgradeInventory upCharInventory;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    UpgradeInputEventHandler eventHandler;

    public static TMP_Text moneyText;


    public void SetUITab()
    {

        upItemInventory.SetUITab(ItemCategory.ALL);
        upItemInventory.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);


        upCharInventory.SetUITab(Job.ALL);
        upCharInventory.SetItemList(GameDB.GetCharList((int)JobBit.ALL));

    }



    #region //추상 함수 정의부
    public override void Init()
    {



        upItemInventory = UtilHelper.Find<UIItemUpgradeInventory>(transform, "UIUpgradeBox/UIItemUpgradeInventory", true, true);
        if (upItemInventory != null)
        {
            if (!upItemInventory.isInit)
                upItemInventory.Init();
        }

        upCharInventory = UtilHelper.Find<UICharacterUpgradeInventory>(transform, "UIUpgradeBox/UICharacterUpgradeInventory", true, true);
        if (upCharInventory != null)
        {

            if (!upCharInventory.isInit)
                upCharInventory.Init();
        }


        itemPopup = UtilHelper.Find<UIItemPopup>(transform, "UIUpgradeBox/UIItemPopup", true, true);
        if (itemPopup != null)
            if (!itemPopup.isInit)
                itemPopup.Init();

        charPopup = UtilHelper.Find<UICharacterPopup>(transform, "UIUpgradeBox/UICharacterPopup", true, true);
        if (charPopup != null)
            if (!charPopup.isInit)
                charPopup.Init();




        eventHandler = GetComponent<UpgradeInputEventHandler>();
        moneyText = UtilHelper.Find<TMP_Text>(transform, "UIUpgradeBox/MoneyBox/MoneyText");

    }

    public override void Run()
    {
        if (upItemInventory != null && upItemInventory.isInit != false)
            upItemInventory.Run();
        if (upCharInventory != null && upCharInventory.isInit != false)
            upCharInventory.Run();
    }

    public override void Open()
    {

    }

    public override void Close()
    {
        itemPopup.Close();
        charPopup.Close();
    }

    #endregion

}
