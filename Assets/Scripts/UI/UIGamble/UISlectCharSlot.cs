using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlectCharSlot : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    PlayerInfo playerInfo;
    Image slotImage;


    private System.Action<PlayerInfo> pointerEnterDelegate;

    private System.Action<PlayerInfo> pointerExitDelegate;




    public void SetPointerEnterDelegate(System.Action<PlayerInfo> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<PlayerInfo> function)
    {
        pointerExitDelegate = function;
    }




    public void PointerEnter()
    {
        if (pointerEnterDelegate != null)
        {
            if (playerInfo != null)
                pointerEnterDelegate(playerInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (playerInfo != null)
                pointerExitDelegate(playerInfo);
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {

        GambleInputEventHandler.gamblePivot.x = transform.position.x;
        GambleInputEventHandler.gamblePivot.y = transform.position.y;
        PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GambleInputEventHandler.gamblePivot = Vector2.zero;

        PointerExit();
    }


    public void SetInfo(PlayerInfo info)
    {
        playerInfo = info;

    }

    public void SetRayCast(bool path, PlayerInfo saveInfo = null)
    {
        if (slotImage != null && playerInfo != saveInfo) slotImage.raycastTarget = path;
    }

    #region //추상 함수 정의부

    public override void Init()
    {

        slotImage = GetComponent<Image>();
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

