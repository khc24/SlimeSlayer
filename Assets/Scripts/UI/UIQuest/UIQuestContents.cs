using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestContents : BaseUI
{
    public UIQuestContent questContent;
    public float itemHeight = 220;
    

    private ScrollRect scroll;
    private List<UIQuestContent> questContentList = new List<UIQuestContent>();

    private List<Quest> questList = new List<Quest>();

    RectTransform scrollRect;
    float scrollHeight;
    int itemCount = 0;

    public Vector2 v = Vector2.zero;
    private float opset = 0;

  


    


    public void SetQuestList()
    {
        
        questList.Clear();

        foreach(var value in GameDB.currQuestDic)
        {
            foreach(var subValue in value.Value)
            {
                questList.Add(subValue.Value);
            }
        }

        questList.Sort(Sort);

        float contentY = scroll.content.anchoredPosition.y;

        for (int i = 0; i < questContentList.Count; i++)
        {
            bool isChange = RelocationItem(questContentList[i], contentY, scrollHeight);
            


                int idx = (int)(-questContentList[i].transform.localPosition.y / itemHeight);


                if (idx < 0 || idx > questList.Count - 1)
                {
                    questContentList[i].gameObject.SetActive(false);
                }
                else
                {
                    questContentList[i].gameObject.SetActive(true);
                    SetData(questContentList[i], idx);
                }

            
        }



        SetContentHight();
      
    }

    public void SetData(UIQuestContent item, int idx)
    {
        item.SetInfo(questList[idx]);
    }

    public bool RelocationItem(UIQuestContent path, float contentY, float scrollHeight)
    {
        
        if (path.transform.localPosition.y + contentY > itemHeight + itemHeight)
        {
            path.transform.localPosition -= new Vector3(0, questContentList.Count * itemHeight);
            RelocationItem(path, contentY, scrollHeight);
      
            return true;
        }
        else if (path.transform.localPosition.y + contentY < -scrollHeight - opset)
        {
            path.transform.localPosition += new Vector3(0, questContentList.Count * itemHeight);
            RelocationItem(path, contentY, scrollHeight);
            return true;
      
        }
        return false;
    }
    private void SetContentHight()
    {
        // 콘텐츠 컴포넌트의 놓이를 정한다.
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, questList.Count * itemHeight);
        scroll.normalizedPosition = new Vector2(0, 1);
    }

    private void CreateItem()
    {
        questContent = Resources.Load<UIQuestContent>("Prefab/UI/UIQuestContent");
        

        itemCount = 0;
        
        if ((scrollRect.rect.height / itemHeight) == (int)(scrollRect.rect.height / itemHeight))
        {
            itemCount = (int)(scrollRect.rect.height / itemHeight) + 3;
            opset = itemHeight + itemHeight / 2;
        }
        else
        {
            itemCount = (int)(scrollRect.rect.height / itemHeight) + 3;
            opset = itemHeight;
        }

        for (int i = 0; i < itemCount; i++)
        {
            UIQuestContent content = Instantiate<UIQuestContent>(questContent, scroll.content.transform);
            questContentList.Add(content);

            content.transform.localPosition = new Vector3(0, -i * itemHeight);
            content.Init();
            if (i < questList.Count)
                content.SetInfo(questList[i]);
            else
                content.SetActive(false);

            scroll.normalizedPosition = new Vector2(0, 1);
        }
    }

    public void SetPointerDownAllDelegate(System.Action<Quest> function)
    {
        foreach (var value in questContentList)
        {
            value.SetPointerDownDelegate(function);
        }
    }

    public void SetPointerEnterAllDelegate(System.Action<Quest> function)
    {
        foreach(var value in questContentList)
        {
            value.SetPointerEnterDelegate(function);
        }
    }

    public void SetPointerExitAllDelegate(System.Action<Quest> function)
    {
        foreach (var value in questContentList)
        {
            value.SetPointerExitDelegate(function);
        }
    }

    public int Sort(Quest left, Quest right)
    {

        if (left.GetQuestState > right.GetQuestState)
        {
            return 1;
        }

        else if (left.GetQuestState < right.GetQuestState)
        {
            return -1;
        }

        else
        {

            if (left.GetLEVEL > right.GetLEVEL)
            {
                return 1;
            }

            else if (left.GetLEVEL < right.GetLEVEL)
            {
                return -1;
            }


            return 0;
        }

    }


    #region //추상 함수 정의부

    public override void Init()
    {
        scroll = GetComponentInChildren<ScrollRect>(true);
        scrollRect = scroll.GetComponent<RectTransform>();
        scrollHeight = scrollRect.rect.height;

        SetQuestList();
        CreateItem();

        isInit = true;
    }

    public override void Run()
    {
        
        float contentY = scroll.content.anchoredPosition.y;
        for (int i = 0; i < questContentList.Count; i++)
        {
            bool isChange = RelocationItem(questContentList[i], contentY, scrollHeight);
            if (isChange)
            {
               

                int idx = (int)(- questContentList[i].transform.localPosition.y / itemHeight);


                if (idx < 0 || idx > questList.Count - 1)
                {
                    questContentList[i].gameObject.SetActive(false);
                }
                else
                {
                    questContentList[i].gameObject.SetActive(true);
                    SetData(questContentList[i], idx);
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
  

    

}
