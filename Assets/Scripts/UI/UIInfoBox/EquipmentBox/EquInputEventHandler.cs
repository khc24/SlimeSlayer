using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquInputEventHandler : MonoBehaviour
{
    UIInventory inventory;
    UICharacterInventory charInventory;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    UIEquipPanel equipPanel;
    UICharacterState characterState;

    // 클릭한 아이템 정보
    public static ItemInfo selected;
    public static PlayerInfo charSelected;


    UISkillBox skillBox;

    public static Vector2 skillSlotPivot = Vector2.zero;

    public bool isSkillPopMove = false;

    Button skillOnBtn;
    Button skillOffBtn;

    UISkillPop skillPopup;

    

    public void Init()
    {
        inventory = GetComponentInChildren<UIInventory>(true);
        if (inventory != null)
        {
            inventory.UITabClickEvent(UITabOnClick);
            inventory.UIInvenItemClickEvent(UIInvenItemOnClick);
            inventory.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);
        }

        charInventory = GetComponentInChildren<UICharacterInventory>(true);
        if (charInventory != null)
        {
            charInventory.UITabClickEvent(UITabOnClick);
            charInventory.UIInvenItemClickEvent(UIInvenItemOnClick);
            charInventory.SetItemList(GameDB.GetCharList((int)JobBit.ALL));
        }

        itemPopup = GetComponentInChildren<UIItemPopup>(true);

        if (itemPopup != null)
        {
            itemPopup.SetOnEquipDelegate(OnClickEquipItem);
            itemPopup.SetOffEquipDelegate(OnClickOffEquipItem);
            itemPopup.SetChangeEquipDelegate(OnClickChangeEquipItem);
        }

        charPopup = GetComponentInChildren<UICharacterPopup>(true);

        if (charPopup != null)
        {
            charPopup.SetOnEquipDelegate(OnClickEquipItem);
        }

        equipPanel = GetComponentInChildren<UIEquipPanel>(true);
        characterState = GetComponentInChildren<UICharacterState>(true);


        skillBox = GetComponentInChildren<UISkillBox>(true);
        if(skillBox != null)
        {
            skillBox.SetSkillUpAllDelegate(SkillUpOnClick);
            skillBox.SetPointerEnterAllDelegate(PointerEnter);
            skillBox.SetPointerExitAllDelegate(PointerExit);
        }

        skillPopup = GetComponentInChildren<UISkillPop>(true);

        skillOnBtn = UtilHelper.FindButton(transform, "SkillOnBtn", SkillOnBtnOnClick);
        skillOffBtn = UtilHelper.FindButton(transform, "UISkillBox/SkillOffBtn", SkillOffBtnOnClick);


    }


    public void SkillUpOnClick(SaveSkill info)
    {
        if (GameDB.player == null || info == null)
            return;

        if(GameDB.player.GetPlayerInfo.skillPoint > 0)
        {
            AudioMng.Instance.PlayUI("UI_Button");
            GameDB.player.GetPlayerInfo.skillPoint--;
            info.level++;
            skillBox.SetUpdate();

            
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Exit");
        }

        
    }

    public void PointerEnter(SaveSkill info)
    {
        

        skillPopup.transform.position = Input.mousePosition;
        float halfHeigh = Screen.height * 0.5f;
        if (charPopup.transform.position.y > halfHeigh)
        {
            RectTransform rect = skillPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 1);
        }
        else
        {
            RectTransform rect = skillPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 0);
        }
        isSkillPopMove = true;
        skillBox.SetRayCastAll(false, info);
        skillBox.SetTabGuardtAll(true);
        skillBox.PopupSet(true);
        skillPopup.Open(info);
    }

    public void PointerExit(SaveSkill info)
    {

        skillBox.SetRayCastAll(true);
        skillBox.SetTabGuardtAll(false);
        isSkillPopMove = false;
        skillBox.PopupSet(false);
        skillPopup.Close();
    }



    public void SkillOnBtnOnClick()
    {
        if (GameDB.player == null)
            return;

        skillBox.SetSkillList();
        skillBox.gameObject.SetActive(true);
    }

    public void SkillOffBtnOnClick()
    {
        skillBox.gameObject.SetActive(false);
        skillBox.PopupSet(false);
    }


    public void OnClickEquipItem(PlayerInfo playerInfo)
    {
        if (playerInfo == null)
            return;

        AudioMng.Instance.PlayUI("UI_Equip");
        PlayerInfo currInfo = GameDB.GetChar(GameDB.userInfo.GetCharUniqueID);


        if (currInfo != null)
        {
            currInfo.IsUpdate = false;
            charInventory.SetCombatEquipment(currInfo, false);
        }


        characterState.EquipItem(playerInfo);
        charInventory.SetCombatEquipment(playerInfo, true);

        equipPanel.TakeOffEquipmentAll();
        equipPanel.EquipItemAll(playerInfo.equipItemArray);




        inventory.SetItemList(GameDB.GetItems((int)inventory.currBitTab), playerInfo.jobType);
  

        
        if (charPopup != null)
        {
           
            charPopup.gameObject.SetActive(false);
        }

        charPopup.Close();
    }


    public void OnClickEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        
        if (GameDB.userInfo.CurrCharacter.WearingTheEquipment((int)itemInfo.category))
        {
            inventory.SetCombatEquipment(GameDB.GetItem(GameDB.userInfo.CurrCharacter.equipItemArray[(int)itemInfo.category]), 0, false);

        }

        equipPanel.EquipItem(itemInfo);
        inventory.SetCombatEquipment(itemInfo, GameDB.userInfo.CurrCharacter.uniqueID, true);

        
        GameDB.userInfo.CurrCharacter.SetWearing((int)itemInfo.category, itemInfo.uniqueID);

        
        if (itemPopup != null)
        {
            
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }


    public void OnClickOffEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

     
        SaveCharacter Character = GameDB.userInfo.Get(itemInfo.equipCharacter);

        if (Character != null)
        {
            
            Character.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(Character.uniqueID);
        }

       
        inventory.SetCombatEquipment(itemInfo, 0, false);
        equipPanel.TakeOffEquipment(itemInfo.category);

       
        if (itemPopup != null)
        {
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }


    public void OnClickChangeEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

       
        SaveCharacter otherCharacter = GameDB.userInfo.Get(itemInfo.equipCharacter);

       
        if (otherCharacter != null)
        {
            
            otherCharacter.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(otherCharacter.uniqueID);
        }

 
        int currCharUniqueID = GameDB.userInfo.GetCharUniqueID;
        int itemUniqueID = GameDB.userInfo.GetIDOfItem(currCharUniqueID, category);
        ItemInfo prevItem = GameDB.GetItem(itemUniqueID);

        GameDB.userInfo.CurrCharacter.SetWearing(category, itemInfo.uniqueID);
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        inventory.SetCombatEquipment(prevItem, 0, false);
        inventory.SetCombatEquipment(itemInfo, currCharUniqueID, true);
        equipPanel.EquipItem(itemInfo);

        if (itemPopup != null)
        {
            
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }



    public void UITabOnClick(UICharacterTab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        JobBit bitCategory = JobBit.ALL;
        System.Enum.TryParse<JobBit>(uiTab.JobBitCategory.ToString(), out bitCategory);

        charInventory.SetUITab(uiTab.JobCategory);
        charInventory.SetItemList(GameDB.GetCharList((int)bitCategory));
    }

    public void UITabOnClick(UITab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        ItemBitCategory bitCategory = ItemBitCategory.ALL;
        System.Enum.TryParse<ItemBitCategory>(uiTab.BitCategory.ToString(), out bitCategory);

        inventory.SetUITab(uiTab.Category);
        inventory.SetItemList(GameDB.GetItems((int)bitCategory), (int)GameDB.userInfo.jobType);
    }

    public void UIInvenItemOnClick(PlayerInfo info)
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


    public void UIInvenItemOnClick(ItemInfo info)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        if (info == null)
        {
            selected = info;
            return;
        }

        if(info.category == ItemCategory.WEAPON || info.category == ItemCategory.SHIELD || info.category == ItemCategory.PET)
        {
            
            if (info != null && info.sprite != null)
            {
                PopupOpen(info);
            }
        }
        
        else if (info.category == ItemCategory.PORTION)
        {

        }
  


        selected = info;
    }


    public void PopupOpen(PlayerInfo info)
    {


        bool onEquipBtnState = false;

        SaveCharacter character = GameDB.userInfo.CurrCharacter;
        
        if(GameDB.userInfo.GetCharUniqueID == info.uniqueID)
        {

        }
        else
        {
            onEquipBtnState = true;
        }

        if(GameDB.currSceneType == SceneType.GameScene)
        {
            charPopup.Open(info);
        }
        else
        {
            charPopup.Open(info, onEquipBtnState);
        }

        
    }

    public void PopupOpen(ItemInfo info)
    {

        
        bool onEquipBtnState = false;
        bool offEquipBtnState = false;
        bool changeEquipBtnState = false;

        
        SaveCharacter character = GameDB.userInfo.CurrCharacter;
        int catecory = (int)info.category;
       


  
        if ((character.jobType & info.wearType) == character.jobType)
        {

            if (info.equip)
            {
                
                if (character.WearingTheEquipment(catecory))
                {
                
                    if(info.equipCharacter == character.uniqueID)
                    {
                        offEquipBtnState = true;
                    }
                    else
                    {
                        offEquipBtnState = true;
                        changeEquipBtnState = true;
                    }
                }
                else
                {
                    offEquipBtnState = true;
                    changeEquipBtnState = true;
                }
            }
            else
            {

                if (character.WearingTheEquipment(catecory))
                {
                    onEquipBtnState = true;
                }
                else
                {
                    onEquipBtnState = true;

                }
                
            }
        }
        
        else
        { 
            if(info.equip)
            {
                offEquipBtnState = true;
            }
            else
            {

            }
        }

        if(GameDB.currSceneType == SceneType.GameScene)
        {
            itemPopup.Open(info);
        }
        else
        {
            itemPopup.Open(info, onEquipBtnState, changeEquipBtnState, offEquipBtnState, false, false);
        }
        
        
    }


    public void Run()
    {

        if (skillPopup != null && isSkillPopMove == true)
            if (Input.mousePosition.x >= skillSlotPivot.x && Input.mousePosition.x <= (skillSlotPivot.x + 200) &&
                Input.mousePosition.y >= skillSlotPivot.y && Input.mousePosition.y <= (skillSlotPivot.y + 200))
                skillPopup.transform.position = Input.mousePosition;

    }


}
