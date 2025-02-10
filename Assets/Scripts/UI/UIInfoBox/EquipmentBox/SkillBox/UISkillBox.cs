using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillBox : BaseUI
{
    public UISkillContent skillContent;
    public float itemHeight = 220;

    public TMP_Text skillPointText;

    private ScrollRect scroll;
    private List<UISkillContent> skillContentList = new List<UISkillContent>();

    private List<SaveSkill> saveSkillList = new List<SaveSkill>();

    RectTransform scrollRect;
    float scrollHeight;
    int itemCount = 0;

    public Image popupShield;


    
    private float opset = 0;


    public void SetRayCastAll(bool path, SaveSkill saveSkill = null)
    {

        
            foreach (var value in skillContentList)
            {
                value.SetRayCast(path , saveSkill);
            }
        
    }

    public void SetTabGuardtAll(bool path)
    {


        foreach (var value in skillContentList)
        {
            value.SetTabGuard(path);
        }

    }

    


    public void SetUpdate()
    {
        skillPointText.text = GameDB.player.GetPlayerInfo.skillPoint.ToString();

        float contentY = scroll.content.anchoredPosition.y;
        for (int i = 0; i < skillContentList.Count; i++)
        {
            bool isChange = RelocationItem(skillContentList[i], contentY, scrollHeight);
            
                int idx = (int)(-skillContentList[i].transform.localPosition.y / itemHeight);


                if (idx < 0 || idx > saveSkillList.Count - 1)
                {
                    skillContentList[i].gameObject.SetActive(false);
                }
                else
                {
                    skillContentList[i].gameObject.SetActive(true);
                    SetData(skillContentList[i], idx);
                }

            
        }
    }


    public void SetSkillList()
    {

        saveSkillList.Clear();

        if(GameDB.player == null)
        {
            foreach(var value in skillContentList)
            {
                value.SetInfo(null);
                value.SetActive(false);
            }

            return;
        }

        skillPointText.text = GameDB.player.GetPlayerInfo.skillPoint.ToString();

        foreach (var value in GameDB.player.GetPlayerInfo.skillList)
        {
            saveSkillList.Add(value);
        }

        saveSkillList.Sort(Sort);

        foreach (var value in skillContentList)
        {
            value.SetInfo(null);
            value.gameObject.SetActive(false);
        }

        for (int i = 0; i < itemCount - 3; i++)
        {
            if (i < saveSkillList.Count)
            {
                SetData(skillContentList[i], i);
                skillContentList[i].gameObject.SetActive(true);
            }

        }

        SetContentHight();
        SetUpdate();
    }

    public void SetData(UISkillContent item, int idx)
    {
        item.SetInfo(saveSkillList[idx]);
    }

    public bool RelocationItem(UISkillContent path, float contentY, float scrollHeight)
    {

        if (path.transform.localPosition.y + contentY > itemHeight + itemHeight)
        {
            path.transform.localPosition -= new Vector3(0, skillContentList.Count * itemHeight);
            RelocationItem(path, contentY, scrollHeight);

            return true;
        }
        else if (path.transform.localPosition.y + contentY < -scrollHeight - opset)
        {
            path.transform.localPosition += new Vector3(0, skillContentList.Count * itemHeight);
            RelocationItem(path, contentY, scrollHeight);
            return true;

        }
        return false;
    }
    private void SetContentHight()
    {
        
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, saveSkillList.Count * itemHeight);
        scroll.normalizedPosition = new Vector2(0, 1);
    }

    private void CreateItem()
    {
        

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
            UISkillContent content = Instantiate<UISkillContent>(skillContent, scroll.content.transform);
            skillContentList.Add(content);

            content.transform.localPosition = new Vector3(0, -i * itemHeight);
            content.Init();
            if (i < saveSkillList.Count)
                content.SetInfo(saveSkillList[i]);
            else
                content.SetActive(false);

            scroll.normalizedPosition = new Vector2(0, 1);
        }
    }

    public void SetSkillUpAllDelegate(System.Action<SaveSkill> function)
    {
        foreach (var value in skillContentList)
        {
            value.SetSkillUpDelegate(function);
        }
    }

    public void SetPointerEnterAllDelegate(System.Action<SaveSkill> function)
    {
        foreach (var value in skillContentList)
        {
            value.SetPointerEnterDelegate(function);
        }
    }

    public void SetPointerExitAllDelegate(System.Action<SaveSkill> function)
    {
        foreach (var value in skillContentList)
        {
            value.SetPointerExitDelegate(function);
        }
    }

    public int Sort(SaveSkill left, SaveSkill right)
    {

        if (left.tableID > right.tableID)
        {
            return 1;
        }

        else if (left.tableID < right.tableID)
        {
            return -1;
        }

        else
        {

            return 0;
        }

    }

    public void PopupSet(bool path)
    {
        popupShield.gameObject.SetActive(path);
        scroll.vertical = !path;
    }

    #region //추상 함수 정의부

    public override void Init()
    {
        scroll = GetComponentInChildren<ScrollRect>(true);
        scrollRect = scroll.GetComponent<RectTransform>();
        scrollHeight = scrollRect.rect.height;

        skillContent = Resources.Load<UISkillContent>("Prefab/UI/UISkillContent");

        skillPointText = UtilHelper.Find<TMP_Text>(transform, "UISkillPoint/SkillPoint");

        popupShield = UtilHelper.Find<Image>(transform, "Background/PopupShield");
        popupShield.gameObject.SetActive(false);

        CreateItem();
        SetSkillList();
        

        isInit = true;
    }

    public override void Run()
    {

        float contentY = scroll.content.anchoredPosition.y;
        for (int i = 0; i < skillContentList.Count; i++)
        {
            bool isChange = RelocationItem(skillContentList[i], contentY, scrollHeight);
            if (isChange)
            {
                int idx = (int)(-skillContentList[i].transform.localPosition.y / itemHeight);


                if (idx < 0 || idx > saveSkillList.Count - 1)
                {
                    skillContentList[i].gameObject.SetActive(false);
                }
                else
                {
                    skillContentList[i].gameObject.SetActive(true);
                    SetData(skillContentList[i], idx);
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
