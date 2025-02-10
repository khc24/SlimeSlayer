using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISkillSlot : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    SaveSkill skillInfo;
    Image slotImage;
    

    private System.Action<SaveSkill> pointerEnterDelegate;

    private System.Action<SaveSkill> pointerExitDelegate;


   

    public void SetPointerEnterDelegate(System.Action<SaveSkill> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<SaveSkill> function)
    {
        pointerExitDelegate = function;
    }


    

    public void PointerEnter()
    {
        if (pointerEnterDelegate != null)
        {
            if (skillInfo != null)
                pointerEnterDelegate(skillInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (skillInfo != null)
                pointerExitDelegate(skillInfo);
        }
    }


    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        EquInputEventHandler.skillSlotPivot.x = transform.position.x;
        EquInputEventHandler.skillSlotPivot.y = transform.position.y;
        PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        PointerExit();
    }


    public void SetInfo(SaveSkill info)
    {
        skillInfo = info;

    }

    public void SetRayCast(bool path,SaveSkill saveSkill = null)
    {
        if (slotImage != null && saveSkill != skillInfo) slotImage.raycastTarget = path;
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

