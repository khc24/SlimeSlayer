using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEquipmentBox : BaseUI
{
    UIInventory inventory;
    UICharacterInventory charInventory;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    UIEquipPanel equipPanel;
    UICharacterState characterState;

    UISkillBox skillBox;
    UISkillPop skillPopup;


    EquInputEventHandler eventHandler;

    static public TMP_Text moneyText;



    public void  SetUITab()
    {
        inventory.SetUITab(ItemCategory.ALL);
        inventory.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);

        charInventory.SetUITab(Job.ALL);
        charInventory.SetItemList(GameDB.GetCharList((int)JobBit.ALL));

    }

    public void EquipItem(PlayerInfo info)
    {
        characterState.EquipItem(info);
        equipPanel.TakeOffEquipmentAll();
        equipPanel.EquipItemAll(info.equipItemArray);
    }

    #region //추상 함수 정의부
    public override void Init()
    {
        inventory = UtilHelper.Find<UIInventory>(transform, "UIInventory", true, true);
        if (inventory != null)
        {
            if(!inventory.isInit)
                inventory.Init();

        }

        charInventory = UtilHelper.Find<UICharacterInventory>(transform, "UICharacterInventory", true, true);
        if (charInventory != null)
        {
            
            if (!charInventory.isInit)
                charInventory.Init();
        }


        itemPopup = UtilHelper.Find<UIItemPopup>(transform, "UIItemPopup", true, false);
        if(itemPopup != null)
            if(!itemPopup.isInit)
                itemPopup.Init();

        charPopup = UtilHelper.Find<UICharacterPopup>(transform, "UICharacterPopup", true, false);
        if (charPopup != null)
            if (!charPopup.isInit)
                charPopup.Init();


        equipPanel = UtilHelper.Find<UIEquipPanel>(transform, "UIEquipPanel", true, true);
        if (equipPanel != null)
            if (!equipPanel.isInit)
                equipPanel.Init();

        characterState = UtilHelper.Find<UICharacterState>(transform, "UICharacterState", true, true);
        if (characterState != null)
            if (!characterState.isInit)
                characterState.Init();

        skillBox = UtilHelper.Find<UISkillBox>(transform, "UISkillBox", true, false);
        if (skillBox != null)
            if (!skillBox.isInit)
                skillBox.Init();

        skillPopup = UtilHelper.Find<UISkillPop>(transform, "UISillPop", true, false);
        if (skillPopup != null)
            if (!skillPopup.isInit)
                skillPopup.Init();


        eventHandler = GetComponent<EquInputEventHandler>();
        if (eventHandler != null) eventHandler.Init();

        moneyText = UtilHelper.Find<TMP_Text>(transform, "MoneyBox/MoneyText");

        isInit = true;

    }



    public override void Run()
    {
        if (inventory != null && inventory.isInit != false)
            inventory.Run();
        if (charInventory != null && charInventory.isInit != false)
            charInventory.Run();
        if (skillBox != null && skillBox.isInit != false)
            skillBox.Run();
        if (eventHandler != null)
            eventHandler.Run();
        
    }

    public override void Open()
    {

    }

    public override void Close()
    {
        skillBox.gameObject.SetActive(false);
    }

    #endregion 

 

}
