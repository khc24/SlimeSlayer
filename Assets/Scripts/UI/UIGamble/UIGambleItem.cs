using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGambleItem : BaseUI
{
    ItemInfo itemInfo;




    UISlectItemSlot selectItemSlot;

    private Image icon;
    
    private List<Image> star = new List<Image>();
    private int grade;


    

    
    public void SetPointerEnterDelegate(System.Action<ItemInfo> function)
    {
        if (selectItemSlot != null) selectItemSlot.SetPointerEnterDelegate(function);
    }

    public void SetPointerExitDelegate(System.Action<ItemInfo> function)
    {
        if (selectItemSlot != null) selectItemSlot.SetPointerExitDelegate(function);
    }

    

  


    public void SetInfo(ItemInfo info)
    {
        itemInfo = info;

        // 정보를 받았을때 인벤토리 버튼이 어떻게 보여줘야 할 지 정보를 설정
        if (info != null)
        {

            if (selectItemSlot != null) selectItemSlot.SetInfo(info);

            //아이템 아이콘 설정
            if (icon != null)
            {
                icon.gameObject.SetActive(true);

                icon.sprite = info.sprite;
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

                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public void SetRayCast(bool path, ItemInfo itemPivot = null)
    {
        if (selectItemSlot != null) selectItemSlot.SetRayCast(path, itemPivot);
    }

    #region //추상 함수 정의부

    public override void Init()
    {

        icon = UtilHelper.Find<Image>(transform, "Icon", false, true);
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, true));


        selectItemSlot = GetComponentInChildren<UISlectItemSlot>(true);
        selectItemSlot.Init();



        Close();
        
    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
