using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGambleChar : BaseUI
{
    PlayerInfo playerInfo;

    UISlectCharSlot selectCharSlot;


   
    private Image icon;
   
    private List<Image> star = new List<Image>();
    private int grade;

    


    
    

   
    public void SetPointerEnterDelegate(System.Action<PlayerInfo> function)
    {
        if(selectCharSlot != null) selectCharSlot.SetPointerEnterDelegate(function);
    }

    public void SetPointerExitDelegate(System.Action<PlayerInfo> function)
    {
        if(selectCharSlot != null) selectCharSlot.SetPointerExitDelegate(function);
    }





    public void SetInfo(PlayerInfo info)
    {
        playerInfo = info;

        // 정보를 받았을때 인벤토리 버튼이 어떻게 보여줘야 할 지 정보를 설정
        if (info != null)
        {

            if (selectCharSlot != null) selectCharSlot.SetInfo(info);

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

    public void SetRayCast(bool path, PlayerInfo charPivot = null)
    {
        if (selectCharSlot != null ) selectCharSlot.SetRayCast(path,charPivot);
    }


    #region //추상 함수 정의부

    public override void Init()
    {
        
        icon = UtilHelper.Find<Image>(transform, "Icon", false, true);
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, true));
       
        

        selectCharSlot = GetComponentInChildren<UISlectCharSlot>(true);
        selectCharSlot.Init();

        gameObject.SetActive(false);
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
