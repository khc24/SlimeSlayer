using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInvenItem : UIItem
{
    private Transform slot;
    private Transform focus;
    private Transform checkBox;
    private Image icon;
    private TMP_Text levelText;
    private List<Image> star = new List<Image>();
    private System.Action<ItemInfo> clickEvent;
    private int grade;
    private Transform shadow;
    Button button;

    private TMP_Text name;
    private TMP_Text priceText;

    public int index = 0;
    public int listNum = 0;

    public override void Init()
    {
        base.Init();

        slot = UtilHelper.Find<Transform>(transform, "Slot", false, false);
        focus = UtilHelper.Find<Transform>(transform,"Focus",false,false);
        checkBox = UtilHelper.Find<Transform>(transform, "CheckBox", false, false);
        icon = UtilHelper.Find<Image>(transform, "Icon", false, false);
        levelText = UtilHelper.Find<TMP_Text>(transform, "LevelText", false, false);
        
        
        name = UtilHelper.Find<TMP_Text>(transform, "Name", false, false);
        priceText = UtilHelper.Find<TMP_Text>(transform, "PriceText", false, false);
        
        
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, false));
        shadow = UtilHelper.Find<Transform>(transform, "Shadow", false, false);
        button = GetComponent<Button>();
        if(button != null)
            button.onClick.AddListener(OnClick);

    }

    void OnClick()
    {
        if (clickEvent != null)
            clickEvent(itemInfo);
    }

    public void SetCount(int idx , int listcount)
    {
        index = idx;
        listNum = listcount;
    }

    public void SetInfo(ItemInfo info)
    {
        itemInfo = info;

        
        if (info != null)
        {
            
          
            if (icon != null)
            {
                icon.gameObject.SetActive(true);
                
                icon.sprite= info.sprite;
            }

            if(checkBox != null)
            {
                if(info.checkbox == true)
                {
                    checkBox.gameObject.SetActive(true);
                }
                else
                {
                    checkBox.gameObject.SetActive(false);
                }
            }

       
            if (levelText != null)
            {
                levelText.text = "LV." + info.level.ToString();
                levelText.gameObject.SetActive(true);
            }

           
            
            for (int i = star.Count - 1; i >= 0; --i)
            {
                if ((info.grade - 1) < i)
                    star[i].gameObject.SetActive(false);
                else
                {
                    star[i].gameObject.SetActive(true);
                }
                    

            }

            if (focus != null)
            {
                if (info.equip)
                    focus.gameObject.SetActive(true);
                else
                    focus.gameObject.SetActive(false);
            }

            if(slot != null)
            {
                slot.gameObject.SetActive(true);
            }


            if (name != null)
            {
                name.text = info.name.ToString();
                name.gameObject.SetActive(true);
            }

            if (priceText != null)
            {
                priceText.text = "°¡°Ý : " + string.Format("{0:#,0}", info.price)+ "¿ø";
                priceText.gameObject.SetActive(true);
            }


        }
    }

    public void SetInfo(ItemInfo info, int characterJobType)
    {
        SetInfo(info);

        if ((info.wearType & characterJobType) != characterJobType)
        {
            shadow.gameObject.SetActive(true);
        }
        else
            shadow.gameObject.SetActive(false);

    }


    public void SetButtonListener(System.Action<ItemInfo> action)
    {
        if (button != null)
            clickEvent = action;
            
    }

    public void Clear()
    {
        SetInfo(null);

        if (icon != null) icon.gameObject.SetActive(false);
        if (levelText != null) levelText.gameObject.SetActive(false);
        if (shadow != null) shadow.gameObject.SetActive(false);
        if (focus != null) focus.gameObject.SetActive(false);
        if(slot != null) slot.gameObject.SetActive(false);
        if (name != null) name.gameObject.SetActive(false);
        if (priceText != null) priceText.gameObject.SetActive(false);
        if (checkBox != null) checkBox.gameObject.SetActive(false);

        

        foreach (var img in star)
            img.gameObject.SetActive(false);
    }

    


}
