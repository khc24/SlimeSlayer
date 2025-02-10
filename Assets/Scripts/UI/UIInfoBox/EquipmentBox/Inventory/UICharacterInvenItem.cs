using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterInvenItem : BaseUI
{

    PlayerInfo playerInfo;
    SaveCharacter saveInfo;
    private Transform slot;
    private Transform focus;
    private Transform checkBox;
    private Image icon;
    private TMP_Text levelText;
    private List<Image> star = new List<Image>();
    private System.Action<PlayerInfo> clickEvent;
    private int grade;
    private Transform shadow;
    Button button;

    private new TMP_Text name;
    private TMP_Text priceText;



    void OnClick()
    {
        if (clickEvent != null)
            clickEvent(playerInfo);
    }

    public void SetInfo(PlayerInfo info)
    {
        playerInfo = info;

        // 정보를 받았을때 인벤토리 버튼이 어떻게 보여줘야 할 지 정보를 설정
        if (info != null)
        {
            
            //아이템 아이콘 설정
            if (icon != null)
            {
                icon.gameObject.SetActive(true);
                
                icon.sprite= info.sprite;
            }

            // 아이템 레벨 설정
            if (levelText != null)
            {
                levelText.text = "LV." + info.level.ToString();
                levelText.gameObject.SetActive(true);
            }

           
            // 별 등급 표시
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

            if (checkBox != null)
            {
                if (info.checkbox == true)
                {
                    checkBox.gameObject.SetActive(true);
                }
                else
                {
                    checkBox.gameObject.SetActive(false);
                }
            }

        

            if (slot != null)
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
                priceText.text = "가격 : " + string.Format("{0:#,0}", info.price) + "원";
                priceText.gameObject.SetActive(true);
            }

        }
    }




    public void SetButtonListener(System.Action<PlayerInfo> action)
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

    #region //추상 함수 정의부

    public override void Init()
    {
        slot = UtilHelper.Find<Transform>(transform, "Slot", false, false);
        focus = UtilHelper.Find<Transform>(transform, "Focus", false, false);
        checkBox = UtilHelper.Find<Transform>(transform, "CheckBox", false, false);
        icon = UtilHelper.Find<Image>(transform, "Icon", false, false);
        levelText = UtilHelper.Find<TMP_Text>(transform, "LevelText", false, false);

        name = UtilHelper.Find<TMP_Text>(transform, "Name", false, false);
        priceText = UtilHelper.Find<TMP_Text>(transform, "PriceText", false, false);

        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, false));
        shadow = UtilHelper.Find<Transform>(transform, "Shadow", false, false);
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);

     
 

    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion
}
