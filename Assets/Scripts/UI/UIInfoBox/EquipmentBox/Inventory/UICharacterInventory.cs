using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInventory : BaseUI
{


    private int contentHeight = 225;
    private int contentWidth = 230;
    private int contentCount = 0;
    private int contentRow = 0;
    private int contentCol = 0;
    private float opset = 0;

    private int viewCount = 0;
    private ScrollRect scroll;
    private RectTransform scrollRect;
    float scrollHeight;

    


    private UICharacterInvenItem invenCharPrefab;

    public List<UICharacterInvenItem> invenCharList = new List<UICharacterInvenItem>();
    
    private List<PlayerInfo> charList = new List<PlayerInfo>();



    public Job currTab = Job.ALL;
    public JobBit currBitTab = JobBit.ALL;

    private UICharacterTabBtns tabBtns;
    private Transform tabFocus;






    public void UITabClickEvent(System.Action<UICharacterTab> onClick)
    {

        tabBtns.SetButtonListener(onClick);
    }

    public void UIInvenItemClickEvent(System.Action<PlayerInfo> onClick)
    {

        foreach (var value in invenCharList)
        {
            value.SetButtonListener(onClick);
        }
    }


    public void SetUITab(Job job)
    {

        

        currTab = job;
        System.Enum.TryParse<JobBit>(currTab.ToString(), out currBitTab);
        tabBtns.SetHighlightActive(job);
        tabFocus.position = tabBtns.GetFocusPos(job);
        tabFocus.gameObject.SetActive(true);


        Clear();


        scroll.normalizedPosition = new Vector2(0, 1);

    }





    public void SetCombatEquipment(PlayerInfo charInfo,  bool equip)
    {
        if (charInfo == null)
            return;
        charInfo.equip = equip;


        if (charInfo.equip == true)
        {
            GameDB.userInfo.SetCharUniqueID = charInfo.uniqueID;
            GameDB.userInfo.jobType = charInfo.jobType;
        }

        float contentY = scroll.content.anchoredPosition.y;

        for (int i = 0; i < invenCharList.Count; i++)
        {


            bool isChange = RelocationItem(invenCharList[i], contentY, scrollHeight);



            int idx = ((int)(-invenCharList[i].transform.localPosition.y / contentHeight) * 4) +
                (int)(invenCharList[i].transform.localPosition.x / contentWidth);



            if (idx < 0 || idx > viewCount - 1)
            {
                invenCharList[i].gameObject.SetActive(false);

            }

            else
            {

                invenCharList[i].gameObject.SetActive(true);
                invenCharList[i].Clear();
                
                if (idx < charList.Count)
                    SetData(invenCharList[i], idx);
            }




        }
    }


    public void SetItemList(List<PlayerInfo> charlist)
    {
        scroll.normalizedPosition = new Vector2(0, 1);
       
        charList.Clear();
        
        for (int i = 0; i < charlist.Count; ++i)
        {

           

            charList.Add(charlist[i]);


        
            if (i < invenCharList.Count)
            {
                SetData(invenCharList[i], i);

            

            }


        }

        if (charList.Count > contentCount)
        {
            if(((float)charList.Count / 4) == (int)(charList.Count / 4))
            {
                viewCount = charlist.Count;
            }
            else
            {
                viewCount = ((int)(charList.Count / 4) * 4) +4;
            }
            
        }
        else
        {
            viewCount = contentCount;
        }

        SetContentHight();
    }

    public void Clear()
    {
        for (int i = 0; i < invenCharList.Count; ++i)
        {

            invenCharList[i].Clear();
        }

    }


    #region //추상 함수 정의부

    public override void Init()
    {
        invenCharPrefab = Resources.Load<UICharacterInvenItem>("Prefab/UI/UICharacterInvenItem");
        scroll = GetComponentInChildren<ScrollRect>(true);
        scrollRect = scroll.GetComponent<RectTransform>();
        scrollHeight = scrollRect.rect.height;

        tabBtns = GetComponentInChildren<UICharacterTabBtns>(true);
        if (tabBtns != null)
            tabBtns.Init();

        tabFocus = transform.Find("TabFocus");

        CreateContent(4);





        SetUITab(Job.ALL);
        if (GameDB.userInfo != null)
            SetItemList(GameDB.GetCharList((int)JobBit.ALL));



        isInit = true;



    }

    public override void Run()
    {
        float contentY = scroll.content.anchoredPosition.y;




        for (int i = 0; i < invenCharList.Count; i++)
        {


            bool isChange = RelocationItem(invenCharList[i], contentY, scrollHeight);
            if (isChange)
            {


                int idx = ((int)(-invenCharList[i].transform.localPosition.y / contentHeight) * 4) +
                    (int)(invenCharList[i].transform.localPosition.x / contentWidth);



                if (idx < 0 || idx > viewCount - 1)
                {
                    invenCharList[i].gameObject.SetActive(false);

                }

                else
                {

                    invenCharList[i].gameObject.SetActive(true);
                    invenCharList[i].Clear();


                    if (idx < charList.Count)
                        SetData(invenCharList[i], idx);
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
        invenCharList.Clear();

        for (int i = 0; i < contentCount; i++)
        {
            int h = (int)(i / contentCol);

            int w = (int)i % contentCol;



            UICharacterInvenItem content = Instantiate<UICharacterInvenItem>(invenCharPrefab, scroll.content.transform);
            invenCharList.Add(content);

            content.transform.localPosition = new Vector3(w * contentWidth, -h * contentHeight);
            content.Init();
            
            if (i < charList.Count)
            {

                content.SetInfo(charList[i]);
            }

        }
    }


    public bool RelocationItem(UICharacterInvenItem path, float contentY, float scrollHeight)
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
        int contentSizeHeight = 0;

        

        if (((float)charList.Count / 4) == (int)(charList.Count / 4))
        {

            contentSizeHeight = (int)(charList.Count / 4);
        }
        else
        {

            contentSizeHeight = (int)(charList.Count / 4);
            ++contentSizeHeight;
        }

       

        // 콘텐츠 컴포넌트의 놓이를 정한다.
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, contentSizeHeight * contentHeight);
        scroll.normalizedPosition = new Vector2(0, 1);
    }

    public void SetData(UICharacterInvenItem item, int idx)
    {
        
        item.SetInfo(charList[idx]);
    }
}
