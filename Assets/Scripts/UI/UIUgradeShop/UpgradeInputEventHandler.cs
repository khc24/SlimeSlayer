using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeInputEventHandler : MonoBehaviour
{

    UIItemUpgradeInventory upItemInventory;
    UICharacterUpgradeInventory upCharInventory;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    Transform UIEffectShow;

    Button takeOnCharInven;
    Button takeOffCharInven;
    
    // 클릭한 아이템 정보
    public static ItemInfo selected;
    public static PlayerInfo charSelected;

    public void Init()
    {
        upItemInventory = GetComponentInChildren<UIItemUpgradeInventory>(true);
        if (upItemInventory != null)
        {
            upItemInventory.UITabClickEvent(UITabOnClick);
            upItemInventory.UIInvenItemClickEvent(UIInvenItemOnClick);
        }

        upCharInventory = GetComponentInChildren<UICharacterUpgradeInventory>(true);
        if (upCharInventory != null)
        {
            upCharInventory.UITabClickEvent(UITabOnClick);
            upCharInventory.UIInvenItemClickEvent(UIInvenItemOnClick);
        }


        itemPopup = GetComponentInChildren<UIItemPopup>(true);

        if (itemPopup != null)
        {
            itemPopup.SetOnUpgradeDelegate(OnClickItemUpgrade);
        }

        charPopup = GetComponentInChildren<UICharacterPopup>(true);

        if (charPopup != null)
        {
            charPopup.SetOnUpgradeDelegate(OnClickItemUpgrade);
        }




        takeOnCharInven = UtilHelper.FindButton(upItemInventory.transform, "OnCharInven", OnCharInventory);
        takeOffCharInven = UtilHelper.FindButton(upCharInventory.transform, "OffCharInven", OffCharInventory);

        UIEffectShow = transform.Find("UIEffectShow");

    }

    public void OnCharInventory()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        upCharInventory.SetActive(true);
        charPopup.SetActive(true);
    }

    public void OffCharInventory()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        upCharInventory.SetActive(false);
        charPopup.SetActive(false);
    }


    public void UITabOnClick(UICharacterTab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        JobBit bitCategory = JobBit.ALL;
        System.Enum.TryParse<JobBit>(uiTab.JobBitCategory.ToString(), out bitCategory);

        upCharInventory.SetUITab(uiTab.JobCategory);
        upCharInventory.SetItemList(GameDB.GetCharList((int)bitCategory));
    }

    public void UITabOnClick(UITab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        ItemBitCategory bitCategory = ItemBitCategory.ALL;
        System.Enum.TryParse<ItemBitCategory>(uiTab.BitCategory.ToString(), out bitCategory);

        upItemInventory.SetUITab(uiTab.Category);
        upItemInventory.SetItemList(GameDB.GetItems((int)bitCategory), (int)GameDB.userInfo.jobType);
    }

    public void UIInvenItemOnClick(PlayerInfo info)
    {
        if (info == null)
        {
            charSelected = info;
            return;
        }
        AudioMng.Instance.PlayUI("UI_Button");
        PopupOpen(info);


        if (charSelected != null) charSelected.checkbox = false;
        charSelected = info;
        charSelected.checkbox = true;

        upCharInventory.SetItemList(GameDB.GetCharList((int)upCharInventory.currBitTab));
        
    }


    public void UIInvenItemOnClick(ItemInfo info)
    {
        if (info == null)
        {
            selected = info;
            return;
        }
        AudioMng.Instance.PlayUI("UI_Button");
    
        if (info != null && info.sprite != null)
        {
            PopupOpen(info);
        }

        if (selected != null) selected.checkbox = false;
        selected = info;
        selected.checkbox = true;
        
        upItemInventory.SetItemList(GameDB.GetItems((int)upItemInventory.currBitTab), (int)GameDB.userInfo.jobType);


    }

    public void OnClickItemUpgrade(ItemInfo info)
    {
        if (info == null || selected == null)
            return;

        int tempInt = 0;

        tempInt = info.grade * 1000;
        tempInt += (info.grade * 100) * (info.level - 1);


        int tempGamble = 0;
        tempGamble = 100 - info.level;

        

        int checkNum = Random.Range(1,101);
        
        bool checkGamble = false;
        if(checkNum <= tempGamble)
        {
            checkGamble = true;
        }



        if (info.level >= 100)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UIText");
            temp = Instantiate(temp, itemPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, itemPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else if (tempInt > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, itemPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, itemPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else if(checkGamble)
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            // 머니 업데이트
            GameDB.money -= tempInt;
            UIMng.Instance.SetMoney(GameDB.money);
 
            info.level++;
            GameDB.SetItemInfoUpdate(info.uniqueID);
            PopupOpen(info);
            upItemInventory.SetItemList(GameDB.GetItems((int)upItemInventory.currBitTab), (int)GameDB.userInfo.jobType);

            if(info.equip == true)
                GameDB.SetCharInfoUpdate(info.equipCharacter);



            // 업그레이드 퀘스트 체크(아이템 업그레이드)

            int questItemTableID = info.tableID + 20000000;

            GameDB.UpdateCount(QuestType.UPGRADE, questItemTableID, info.level);

            //



            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UpgradeClear");
            temp = Instantiate(temp, itemPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, itemPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            GameDB.money -= tempInt;
            UIMng.Instance.SetMoney(GameDB.money);
            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UpgradeFail");
            temp = Instantiate(temp, itemPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, itemPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
    }

    public void OnClickItemUpgrade(PlayerInfo info)
    {
        if (info == null || charSelected == null)
            return;

        int tempInt = 0;

        tempInt = info.grade * 1000;
        tempInt += (info.grade * 100) * (info.level - 1);


        int tempGamble = 0;
        tempGamble = 100 - info.level;



        int checkNum = Random.Range(1, 101);

        bool checkGamble = false;
        if (checkNum <= tempGamble)
        {
            checkGamble = true;
        }



        if(info.level >= 100)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UIText");
            temp = Instantiate(temp, charPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, charPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else if (tempInt > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, charPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, charPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else if (checkGamble)
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            // 머니 업데이트
            GameDB.money -= tempInt;
            UIMng.Instance.SetMoney(GameDB.money);

            info.level++;
            GameDB.SetCharInfoUpdate(info.uniqueID);
            PopupOpen(info);
            upCharInventory.SetItemList(GameDB.GetCharList((int)upCharInventory.currBitTab));


            // 업그레이드 퀘스트 체크(캐릭터 업그레이드)

            int questItemTableID = info.tableID + 10000000;

            GameDB.UpdateCount(QuestType.UPGRADE, questItemTableID, info.level);

            //



            Vector3 v = new Vector3(-150f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UpgradeClear");
            temp = Instantiate(temp, charPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, charPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            GameDB.money -= tempInt;
            UIMng.Instance.SetMoney(GameDB.money);
            Vector3 v = new Vector3(-150f,50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/UpgradeFail");
            temp = Instantiate(temp, charPopup.takeOnUpgrade.transform.position + v, Quaternion.identity, charPopup.takeOnUpgrade.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
    }

    public void PopupOpen(PlayerInfo info)
    {


        if (info == null)
            return;

        
        charPopup.Open(info, false, false, false, true);

    }

    public void PopupOpen(ItemInfo info)
    {
        if (info == null)
            return;


        itemPopup.Open(info, false, false, false, false,false, true);
    }


}
