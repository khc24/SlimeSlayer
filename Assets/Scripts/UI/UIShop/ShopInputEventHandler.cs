using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopInputEventHandler : MonoBehaviour
{

    UIItemBuy itemBuy;
    UICharacterBuy charBuy;

    UIItemSell itemSell;
    UICharacterSell charSell;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    Transform UIEffectShow;


    Button takeOnCharBuy;
    Button takeOffCharBuy;
    Button takeOnCharSell;
    Button takeOffCharSell;
    

    public static ItemInfo selected;
    public static PlayerInfo charSelected;

    public void Init()
    {
        
        itemBuy = GetComponentInChildren<UIItemBuy>(true);
        if (itemBuy != null)
        {
            itemBuy.UITabClickEvent(UIShopTabOnClick);
            itemBuy.UIInvenItemClickEvent(UIShopItemOnClick);
        }

        
        itemSell = GetComponentInChildren<UIItemSell>(true);
        if (itemSell != null)
        {
            itemSell.UITabClickEvent(UITabOnClick);
            itemSell.UIInvenItemClickEvent(UIInvenItemOnClick);
        }

        charBuy = GetComponentInChildren<UICharacterBuy>(true);
        if (charBuy != null)
        {
            charBuy.UITabClickEvent(UIShopTabOnClick);
            charBuy.UIInvenItemClickEvent(UIShopItemOnClick);
        }

        charSell = GetComponentInChildren<UICharacterSell>(true);
        if (charSell != null)
        {
            charSell.UITabClickEvent(UITabOnClick);
            charSell.UIInvenItemClickEvent(UIInvenItemOnClick);
        }

        itemPopup = GetComponentInChildren<UIItemPopup>(true);

        if (itemPopup != null)
        {
            itemPopup.SetOnBuyDelegate(OnClickItemBuy);
            itemPopup.SetOnSellDelegate(OnClickItemSell);
        }

        charPopup = GetComponentInChildren<UICharacterPopup>(true);

        if (charPopup != null)
        {
            charPopup.SetOnBuyDelegate(OnClickCharBuy);
            charPopup.SetOnSellDelegate(OnClickCharSell);
        }

        takeOnCharBuy = UtilHelper.FindButton(itemBuy.transform, "OnCharBuy", OnCharBuy);
        takeOffCharBuy = UtilHelper.FindButton(charBuy.transform, "OffCharBuy", OffCharBuy);
        takeOnCharSell = UtilHelper.FindButton(itemSell.transform, "OnCharSell", OnCharSell);
        takeOnCharSell = UtilHelper.FindButton(charSell.transform, "OffCharSell", OffCharSell);


        UIEffectShow = transform.Find("UIEffectShow");
        
    }

    public void OnCharBuy()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        charBuy.SetActive(true);
    }

    public void OffCharBuy()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        charBuy.SetActive(false);
    }

    public void OnCharSell()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        charSell.SetActive(true);
    }

    public void OffCharSell()
    {
        AudioMng.Instance.PlayUI("UI_Button");
        charSell.SetActive(false);
    }


    public void UIShopTabOnClick(UICharacterTab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        JobBit bitCategory = JobBit.ALL;
        System.Enum.TryParse<JobBit>(uiTab.JobBitCategory.ToString(), out bitCategory);

        charBuy.SetUITab(uiTab.JobCategory);
        charBuy.SetItemList(GameDB.GetCharAllList((int)bitCategory));
    }

    public void UIShopItemOnClick(PlayerInfo info)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        if (info == null)
        {
            charSelected = info;
            return;
        }
        PopupOpen(info);


        charSelected = info;
    }

    public void UIShopTabOnClick(UITab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        ItemBitCategory bitCategory = ItemBitCategory.ALL;
        System.Enum.TryParse<ItemBitCategory>(uiTab.BitCategory.ToString(), out bitCategory);

        itemBuy.SetUITab(uiTab.Category);
        itemBuy.SetItemList(GameDB.GetAllItems((int)bitCategory));
    }

    public void UIShopItemOnClick(ItemInfo info)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        if (info == null)
        {
            selected = info;
            return;
        }

            if (info != null && info.sprite != null)
            {
                PopupOpen(info);
            }
        
        selected = info;
    }

    public void OnClickItemBuy(ItemInfo itemInfo)
    {
        
        if (GameDB.money < itemInfo.price || GameDB.money == 0)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, itemPopup.takeOnBuy.transform.position + v, Quaternion.identity,itemPopup.takeOnBuy.transform);
            if(UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
           
            AudioMng.Instance.PlayUI("UI_Shop");
            int ID = GameDB.userInfo.uniqueCount;

            GameDB.money -= itemInfo.price;
            GameDB.SetInfo(itemInfo.tableID, 1, ID++, false, 0);
            GameDB.userInfo.uniqueCount = ID;

            itemSell.SetUITab(itemInfo.category);
            itemSell.SetItemList(GameDB.GetItems((int)itemInfo.bitCategory), (int)GameDB.userInfo.jobType);
            charSell.SetActive(false);


            UIMng.Instance.SetMoney(GameDB.money);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyClear");

            Vector3 v = new Vector3(0, 5f, 0);
            temp = Instantiate(temp, itemPopup.takeOnBuy.transform.position + v, Quaternion.identity, itemPopup.takeOnBuy.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);

            // 구매 퀘스트 체크(아이템 구매)

            int questItemTableID = itemInfo.tableID + 20000;

            GameDB.UpdateCount(QuestType.SHOP, questItemTableID, 1);

            //

        }
        



    }

    public void OnClickItemSell(ItemInfo itemInfo)
    {
        
        if (itemInfo.equip)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
           
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/SellFail");
             temp= Instantiate(temp, itemPopup.takeOnSell.transform.position + v, Quaternion.identity, itemPopup.takeOnSell.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);

        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");

            GameDB.itemDic.Remove(itemInfo.uniqueID);
            
        
            itemSell.SetUITab(itemSell.currTab);
            itemSell.SetItemList(GameDB.GetItems((int)itemInfo.bitCategory), (int)GameDB.userInfo.jobType);


            GameDB.money += itemInfo.price / 2;

            UIMng.Instance.SetMoney(GameDB.money);
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/SellClear");
            temp = Instantiate(temp, itemPopup.takeOnSell.transform.position + v, Quaternion.identity, itemPopup.takeOnSell.transform);

            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);



            if (itemPopup != null)
            {
            
                itemPopup.gameObject.SetActive(false);
            }

            // 구매 퀘스트 체크(아이템 판매)

            int questItemTableID = itemInfo.tableID + 20000;

            GameDB.UpdateCount(QuestType.SHOP, questItemTableID, 2);

            //

        }


    }


    public void OnClickCharBuy(PlayerInfo playerInfo)
    {
        
        if (GameDB.money < playerInfo.price || GameDB.money == 0)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, charPopup.takeOnBuy.transform.position + v, Quaternion.identity, charPopup.takeOnBuy.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            int ID = GameDB.userInfo.uniqueCount;

            GameDB.money -= playerInfo.price;
            int[] equipCharacter = new int[] { 0, 0, 0 };


            List<SaveSkill> tempSaveSkillList = new List<SaveSkill>();
            string skillStr = DataManager.ToS(TableType.PLAYERTABLE, playerInfo.tableID, "SKILLLIST");
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

            GameDB.SetCharInfo(playerInfo.tableID, 1, ID++, false, equipCharacter , tempSaveSkillList);
            GameDB.userInfo.uniqueCount = ID;

            charSell.SetUITab(playerInfo.job);
            charSell.SetItemList(GameDB.GetCharList((int)playerInfo.jobBit));
            charSell.SetActive(true);

            UIMng.Instance.SetMoney(GameDB.money);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyClear");

            Vector3 v = new Vector3(0, 5f, 0);
            temp = Instantiate(temp, charPopup.takeOnBuy.transform.position + v, Quaternion.identity, charPopup.takeOnBuy.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);

            // 구매 퀘스트 체크(캐릭터 구매)

            int questItemTableID = playerInfo.tableID + 10000;

            GameDB.UpdateCount(QuestType.SHOP, questItemTableID, 1);

            //

        }




    }

    public void OnClickCharSell(PlayerInfo playerInfo)
    {
        
        if (playerInfo.equip)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/SellFail");
            temp = Instantiate(temp, charPopup.takeOnSell.transform.position + v, Quaternion.identity, charPopup.takeOnSell.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);

        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            GameDB.charDic.Remove(playerInfo.uniqueID);
           
            charSell.SetUITab(charSell.currTab);
            charSell.SetItemList(GameDB.GetCharList((int)charSell.currBitTab));
            

            GameDB.money += playerInfo.price / 2;

            UIMng.Instance.SetMoney(GameDB.money);
            Vector3 v = new Vector3(0, 5f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/SellClear");
            temp = Instantiate(temp, charPopup.takeOnSell.transform.position + v, Quaternion.identity, charPopup.takeOnSell.transform);

            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);



            if (charPopup != null)
            {
              
                charPopup.gameObject.SetActive(false);
            }

            // 구매 퀘스트 체크(캐릭터 판매)

            int questItemTableID = playerInfo.tableID + 10000;

            GameDB.UpdateCount(QuestType.SHOP, questItemTableID, 2);

            //

        }


    }



    public void UITabOnClick(UICharacterTab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        JobBit bitCategory = JobBit.ALL;
        System.Enum.TryParse<JobBit>(uiTab.JobBitCategory.ToString(), out bitCategory);

        charSell.SetUITab(uiTab.JobCategory);
        charSell.SetItemList(GameDB.GetCharList((int)bitCategory));
    }

    public void UITabOnClick(UITab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        ItemBitCategory bitCategory = ItemBitCategory.ALL;
        System.Enum.TryParse<ItemBitCategory>(uiTab.BitCategory.ToString(), out bitCategory);

        itemSell.SetUITab(uiTab.Category);
        itemSell.SetItemList(GameDB.GetItems((int)bitCategory), (int)GameDB.userInfo.jobType);
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


        charSelected = info;
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
        selected = info;
    }


    public void PopupOpen(PlayerInfo info)
    {


        if (info == null)
            return;

        bool onBuyBtnState = false;
        bool onSellBtnState = false;

   
        if (info.uniqueID == -1)
        {
            onBuyBtnState = true;
        }

        
        if (info.uniqueID != -1)
        {
            onSellBtnState = true;
        }

        charPopup.Open(info, false, onBuyBtnState, onSellBtnState);

    }

    public void PopupOpen(ItemInfo info)
    {
        if (info == null)
            return;

        bool onBuyBtnState = false;
        bool onSellBtnState = false;

       
        if (info.uniqueID == -1)
        {
            onBuyBtnState = true;
        }

       
        if (info.uniqueID != -1)
        {
            onSellBtnState = true;
        }


        
        itemPopup.Open(info, false, false, false, onBuyBtnState, onSellBtnState);
    }


}
