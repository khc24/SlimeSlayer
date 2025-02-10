using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlectItemSlot : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    ItemInfo itemInfo;
    Image slotImage;


    private System.Action<ItemInfo> pointerEnterDelegate;

    private System.Action<ItemInfo> pointerExitDelegate;




    public void SetPointerEnterDelegate(System.Action<ItemInfo> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<ItemInfo> function)
    {
        pointerExitDelegate = function;
    }




    public void PointerEnter()
    {
        if (pointerEnterDelegate != null)
        {
            if (itemInfo != null)
                pointerEnterDelegate(itemInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (itemInfo != null)
                pointerExitDelegate(itemInfo);
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


    public void SetInfo(ItemInfo info)
    {
        itemInfo = info;

    }

    public void SetRayCast(bool path, ItemInfo saveInfo = null)
    {
        
        
        if (slotImage != null && itemInfo != saveInfo) slotImage.raycastTarget = path;
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

