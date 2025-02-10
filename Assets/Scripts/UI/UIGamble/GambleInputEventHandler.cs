using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GambleInputEventHandler : MonoBehaviour
{
    UIGamble uiGamble;
    UIGambler gambler;

    UIItemGamblePopup itemGamblePopup;
    UICharGamblePopup charGamblePopup;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;


    private Button leftBtn;
    private Button rightBtn;
   
    private Button exitBack;
    private Button exitButton;

    private ScrollRect scrollRect;

    private Animator ani;
    Transform UIEffectShow;

    public bool isCharPopMove = false;
    public bool isItemPopMove = false;


    private bool isCoroutine = false;

  

    public static Vector2 gamblePivot = Vector2.zero;

    public void Init()
    {
        ani = GetComponentInChildren<Animator>(true);

        Transform t = transform.Find("UIGambleExit");

        exitBack = UtilHelper.FindButton(transform, "UIGableExit", UIGambleExit);  
        exitButton = UtilHelper.FindButton(transform, "UIGambleBox/UIGambleExit", UIGambleExit);

        scrollRect = GetComponentInChildren<ScrollRect>(true);
        if(scrollRect != null)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
            leftBtn = UtilHelper.FindButton(scrollRect.transform, "LeftBtn", OnClickLeftBtn);
            rightBtn = UtilHelper.FindButton(scrollRect.transform, "RightBtn", OnClickRightBtn);

        }

        gambler = GetComponentInChildren<UIGambler>(true);

        if(gambler != null)
        {
            gambler.SetCharBtn1Delegate(OnClickCharBtn1);
            gambler.SetCharBtn10Delegate(OnClickCharBtn10);
            gambler.SetItemBtn1Delegate(OnClickItemBtn1);
            gambler.SetItemBtn10Delegate(OnClickItemBtn10);
        }

        itemGamblePopup = GetComponentInChildren<UIItemGamblePopup>(true);
        if(itemGamblePopup != null)
        {
            itemGamblePopup.SetOnCLickDelegate(ItemOnClick);
            itemGamblePopup.SetPointerEnter(PointerEnter);
            itemGamblePopup.SetPointerExit(PointerExit);
        }
        charGamblePopup = GetComponentInChildren<UICharGamblePopup>(true);

        if (charGamblePopup != null)
        {
            charGamblePopup.SetOnCLickDelegate(CharOnClick);
            charGamblePopup.SetPointerEnter(PointerEnter);
            charGamblePopup.SetPointerExit(PointerExit);
        }

        itemPopup = GetComponentInChildren<UIItemPopup>(true);
        
        charPopup = GetComponentInChildren<UICharacterPopup>(true);


        UIEffectShow = transform.Find("UIEffectShow");
    }


    public void PointerEnter(PlayerInfo info)
    {
        
        charPopup.transform.position = Input.mousePosition;
        float halfWidth = Screen.width * 0.5f;
        if (charPopup.transform.position.x > halfWidth)
        {
            RectTransform rect = charPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(1, 1);
        }
        else
        {
            RectTransform rect = charPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 1);
        }

        charGamblePopup.SetRayCastAll(false, info);
        isCharPopMove = true;
        charPopup.Open(info);
    }

    public void PointerExit(PlayerInfo info)
    {
        charGamblePopup.SetRayCastAll(true);
        isCharPopMove = false;
        charPopup.GambleClose();
    }

    void CharOnClick()
    {
        isCharPopMove = false;
        AudioMng.Instance.PlayUI("UI_Exit");
        charGamblePopup.SetRayCastAll(true);
        charPopup.GambleClose();

    }

    public void CallCharBtn1()
    {
        AudioMng.Instance.PlayUI("UI_Shop");
        GameDB.money -= 10000;
        UIMng.Instance.SetMoney(GameDB.money);
        List<PlayerInfo> tempList = new List<PlayerInfo>();
        tempList = GameDB.GambleRandomCharCreate(1);

        foreach(var value in tempList)
        {
            // »Ì±â Äù½ºÆ® Ã¼Å©(Ä³¸¯ÅÍ »Ì±â)

            int questItemTableID = value.tableID + 10000;

            GameDB.UpdateCount(QuestType.GAMBLE, questItemTableID, value.level);

            //
        }

        charGamblePopup.SetGambleCharInfo(tempList);
        ani.SetTrigger("Idle");
    }

    public void CallCharBtn10()
    {
        AudioMng.Instance.PlayUI("UI_Shop");
        GameDB.money -= 110000;
        UIMng.Instance.SetMoney(GameDB.money);
        List<PlayerInfo> tempList = new List<PlayerInfo>();
        tempList = GameDB.GambleRandomCharCreate(12);

        foreach (var value in tempList)
        {
            // »Ì±â Äù½ºÆ® Ã¼Å©(Ä³¸¯ÅÍ »Ì±â)

            int questItemTableID = value.tableID + 10000;

            GameDB.UpdateCount(QuestType.GAMBLE, questItemTableID, value.level);

            //
        }

        charGamblePopup.SetGambleCharInfo(tempList);
        ani.SetTrigger("Idle");

    }

    public void CallItemBtn1()
    {
        AudioMng.Instance.PlayUI("UI_Shop");
        GameDB.money -= 10000;
        UIMng.Instance.SetMoney(GameDB.money);
        List<ItemInfo> tempList = new List<ItemInfo>();
        tempList = GameDB.GambleRandomCreate(1);

        foreach (var value in tempList)
        {
            // »Ì±â Äù½ºÆ® Ã¼Å©(¾ÆÀÌÅÛ »Ì±â)

            int questItemTableID = value.tableID + 20000;

            GameDB.UpdateCount(QuestType.GAMBLE, questItemTableID, value.level);

            //
        }

        itemGamblePopup.SetGambleItemInfo(tempList);
        ani.SetTrigger("Idle");
    }

    public void CallItemBtn10()
    {
        AudioMng.Instance.PlayUI("UI_Shop");
        GameDB.money -= 110000;
        UIMng.Instance.SetMoney(GameDB.money);
        List<ItemInfo> tempList = new List<ItemInfo>();
        tempList = GameDB.GambleRandomCreate(12);

        foreach (var value in tempList)
        {
            // »Ì±â Äù½ºÆ® Ã¼Å©(¾ÆÀÌÅÛ »Ì±â)

            int questItemTableID = value.tableID + 20000;

            GameDB.UpdateCount(QuestType.GAMBLE, questItemTableID, value.level);

            //
        }

        itemGamblePopup.SetGambleItemInfo(tempList);
        ani.SetTrigger("Idle");

    }


    void OnClickCharBtn1()
    {
        if (10000f > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, gambler.charBtn1.transform.position + v, Quaternion.identity, gambler.charBtn1.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {

            AudioMng.Instance.PlayUI("UI_Shop");
            ani.SetTrigger("Char1");
         
        }
    }

    void OnClickCharBtn10()
    {
        if (110000f > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, gambler.charBtn10.transform.position + v, Quaternion.identity, gambler.charBtn10.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            ani.SetTrigger("Char10");
          
        }
    }


    public void PointerEnter(ItemInfo info)
    {
        itemPopup.transform.position = Input.mousePosition;
        float halfWidth = Screen.width * 0.5f;
        
        if (itemPopup.transform.position.x > halfWidth)
        {
            RectTransform rect = itemPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(1, 1);
        }
        else
        {
            RectTransform rect = itemPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 1);
        }

        itemGamblePopup.SetRayCastAll(false, info);
        isItemPopMove = true;
        itemPopup.Open(info);
    }

    public void PointerExit(ItemInfo info)
    {
        itemGamblePopup.SetRayCastAll(true);
        isItemPopMove = false;
        itemPopup.GambleClose();
    }

    void ItemOnClick()
    {
        isItemPopMove = false;
        AudioMng.Instance.PlayUI("UI_Exit");
        itemGamblePopup.SetRayCastAll(true);
        itemPopup.GambleClose();
    }

    void OnClickItemBtn1()
    {
        if (10000f > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, gambler.itemBtn1.transform.position + v, Quaternion.identity, gambler.itemBtn1.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            ani.SetTrigger("Item1");
           
        }

        
    }

    void OnClickItemBtn10()
    {
        if (110000f > GameDB.money)
        {
            AudioMng.Instance.PlayUI("UI_ShopFail");
            Vector3 v = new Vector3(0f, 50f, 0);
            UIDestoryEffect temp = Resources.Load<UIDestoryEffect>("Prefab/UI/BuyFail");
            temp = Instantiate(temp, gambler.itemBtn10.transform.position + v, Quaternion.identity, gambler.itemBtn10.transform);
            if (UIEffectShow != null)
                temp.transform.SetParent(UIEffectShow.transform);
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Shop");
            ani.SetTrigger("Item10");
       
        }
    }

    void OnClickLeftBtn()
    {
        if (isCoroutine)
            return;
        StartCoroutine(MoveBox(false));
    }

    void OnClickRightBtn()
    {
        if (isCoroutine)
            return;
        StartCoroutine(MoveBox(true));
       
    }

    public IEnumerator MoveBox(bool right)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        isCoroutine = true;
        float elapsd = 0;
        Vector2 tempVector2;
        if (right)
        {
            tempVector2 = scrollRect.normalizedPosition + new Vector2(1f,0);
            while (elapsd < 1f)
            {
                if (scrollRect.normalizedPosition.x >= 1)
                {
                    tempVector2 = Vector2.right;
                    break;
                }
                    
                elapsd += Time.deltaTime * 4f;
                scrollRect.normalizedPosition += new Vector2(4f * Time.deltaTime, 0);
                yield return null;
            }
            scrollRect.normalizedPosition = tempVector2;
            
        }
        else
        {
           
            tempVector2 = scrollRect.normalizedPosition - new Vector2(1f, 0);
            while (elapsd < 1f)
            {
                if (scrollRect.normalizedPosition.x <= 0)
                {
                    tempVector2 = Vector2.zero;
                    break;
                }
                    
                elapsd += Time.deltaTime * 4f;
                scrollRect.normalizedPosition -= new Vector2(4f * Time.deltaTime, 0);
                yield return null;
            }
                scrollRect.normalizedPosition = tempVector2;
        }

        isCoroutine = false;
    }

    void UIGambleExit()
    {
        isItemPopMove = false;
        AudioMng.Instance.PlayUI("UI_Exit");
        gameObject.SetActive(false);
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(true);

        UnitMng.Instance.Resume();
        
    }

    public void Run()
    {
        if(isCharPopMove)
            if (Input.mousePosition.x >= gamblePivot.x && Input.mousePosition.x <= (gamblePivot.x + 200) &&
               Input.mousePosition.y >= gamblePivot.y && Input.mousePosition.y <= (gamblePivot.y + 200))
                charPopup.transform.position = Input.mousePosition;
        if(isItemPopMove)
            if (Input.mousePosition.x >= gamblePivot.x && Input.mousePosition.x <= (gamblePivot.x + 200) &&
               Input.mousePosition.y >= gamblePivot.y && Input.mousePosition.y <= (gamblePivot.y + 200))
                itemPopup.transform.position = Input.mousePosition;

    }

}
