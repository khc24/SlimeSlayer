using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemBuy : BaseUI
{
 

    private int contentHeight = 205;
    private int contentWidth = 880;
    private int contentCount = 0;
    private int contentRow = 0;
    private int contentCol = 0;
    private float opset = 0;

    private int viewCount = 0;
    private ScrollRect scroll;
    private RectTransform scrollRect;
    float scrollHeight;




    private UIInvenItem invenItemPrefab;

    public List<UIInvenItem> invenItemList = new List<UIInvenItem>();
 
    private List<ItemInfo> itemList = new List<ItemInfo>();



    public ItemCategory currTab = ItemCategory.ALL;
    public ItemBitCategory currBitTab = ItemBitCategory.ALL;

    private UITabBtns tabBtns;
    private Transform tabFocus;






    public void UITabClickEvent(System.Action<UITab> onClick)
    {

        tabBtns.SetButtonListener(onClick);
    }

    public void UIInvenItemClickEvent(System.Action<ItemInfo> onClick)
    {

        foreach (var value in invenItemList)
        {
            value.SetButtonListener(onClick);
        }
    }


    public void SetUITab(ItemCategory category)
    {

        currTab = category;
        System.Enum.TryParse<ItemBitCategory>(currTab.ToString(), out currBitTab);
        tabBtns.SetHighlightActive(category);
        tabFocus.position = tabBtns.GetFocusPos(category);
        tabFocus.gameObject.SetActive(true);


        Clear();

        scroll.normalizedPosition = new Vector2(0, 1);

    }





    public void SetCombatEquipment(ItemInfo itemInfo, int characterID, bool equip)
    {
        if (itemInfo == null)
            return;
        itemInfo.equip = equip;
        itemInfo.equipCharacter = characterID;

        float contentY = scroll.content.anchoredPosition.y;

        for (int i = 0; i < invenItemList.Count; i++)
        {


            bool isChange = RelocationItem(invenItemList[i], contentY, scrollHeight);



            int idx = (int)(-invenItemList[i].transform.localPosition.y / contentHeight);
               



            if (idx < 0 || idx > viewCount - 1)
            {
                invenItemList[i].gameObject.SetActive(false);

            }

            else
            {

                invenItemList[i].gameObject.SetActive(true);
                invenItemList[i].Clear();

                if (idx < itemList.Count)
                    SetData(invenItemList[i], idx);
            }




        }
    }


    public void SetItemList(List<ItemInfo> itemlist)
    {
        scroll.normalizedPosition = new Vector2(0, 1);

        itemList.Clear();
 
        for (int i = 0; i < itemlist.Count; ++i)
        {


            itemList.Add(itemlist[i]);

            if (i < invenItemList.Count)
            {
                SetData(invenItemList[i], i);


            }


        }

        if (itemList.Count > contentCount)
        {
            
                viewCount = itemList.Count;
           
        }
        else
        {
            viewCount = contentCount;
        }

        SetContentHight();
    }

    public void Clear()
    {
        for (int i = 0; i < invenItemList.Count; ++i)
        {

            invenItemList[i].Clear();
        }

    }


    #region //추상 함수 정의부

    public override void Init()
    {
        invenItemPrefab = Resources.Load<UIInvenItem>("Prefab/UI/UIShopItem");
        scroll = GetComponentInChildren<ScrollRect>(true);
        scrollRect = scroll.GetComponent<RectTransform>();
        scrollHeight = scrollRect.rect.height;

        tabBtns = GetComponentInChildren<UITabBtns>(true);
        if (tabBtns != null)
            tabBtns.Init();

        tabFocus = transform.Find("TabFocus");

        CreateContent(1);



        SetUITab(ItemCategory.ALL);
        if (GameDB.userInfo != null)
            SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL));



        isInit = true;



    }

    public override void Run()
    {
        float contentY = scroll.content.anchoredPosition.y;




        for (int i = 0; i < invenItemList.Count; i++)
        {


            bool isChange = RelocationItem(invenItemList[i], contentY, scrollHeight);
            if (isChange)
            {


                int idx = (int)(-invenItemList[i].transform.localPosition.y / contentHeight);



                if (idx < 0 || idx > viewCount - 1)
                {
                    invenItemList[i].gameObject.SetActive(false);

                }

                else
                {

                    invenItemList[i].gameObject.SetActive(true);
                    invenItemList[i].Clear();


                    if (idx < itemList.Count)
                        SetData(invenItemList[i], idx);
                }

            }
        }

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

    private void CreateContent(int colValue)
    {



        scroll.normalizedPosition = new Vector2(0, 1);

        contentCount = 0;
        contentRow = 0;
        contentCol = colValue;

        if ((scrollRect.rect.height / contentHeight) == (int)(scrollRect.rect.height / contentHeight))
        {

            contentRow = (int)(scrollRect.rect.height / contentHeight) + 3;
            opset = contentHeight + contentHeight / 2;
        }
        else
        {

            contentRow = (int)(scrollRect.rect.height / contentHeight) + 3;
            opset = contentHeight;
        }

        contentCount = contentRow * contentCol;
        viewCount = contentCount;
        invenItemList.Clear();

        for (int i = 0; i < contentCount; i++)
        {
            int h = (int)(i / contentCol);

            int w = (int)i % contentCol;



            UIInvenItem content = Instantiate<UIInvenItem>(invenItemPrefab, scroll.content.transform);
            invenItemList.Add(content);

            content.transform.localPosition = new Vector3(0, -h * contentHeight);
            content.Init();
            
            if (i < itemList.Count)
            {

                content.SetInfo(itemList[i]);
            }


        }
    }


    public bool RelocationItem(UIInvenItem path, float contentY, float scrollHeight)
    {

        if (path.transform.localPosition.y + contentY > contentHeight + contentHeight)
        {
            path.transform.localPosition -= new Vector3(0, contentRow * contentHeight);
            RelocationItem(path, contentY, scrollHeight);

            return true;
        }
        else if (path.transform.localPosition.y + contentY < -scrollHeight - opset)
        {
            path.transform.localPosition += new Vector3(0, contentRow * contentHeight);
            RelocationItem(path, contentY, scrollHeight);
            return true;

        }
        return false;
    }
    private void SetContentHight()
    {

        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, itemList.Count * contentHeight);
        scroll.normalizedPosition = new Vector2(0, 1);
    }

    public void SetData(UIInvenItem item, int idx)
    {
        item.SetInfo(itemList[idx]);
    }
}
