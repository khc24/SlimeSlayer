using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIQuestInfoBtn : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Quest questInfo;
    Image slotSelect;

    private System.Action<Quest> pointerDownDelegate;

    private System.Action<Quest> pointerEnterDelegate;

    private System.Action<Quest> pointerExitDelegate;


    public void SetPointerDownDelegate(System.Action<Quest> function)
    {
        pointerDownDelegate = function;
    }

    public void SetPointerEnterDelegate(System.Action<Quest> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<Quest> function)
    {
        pointerExitDelegate = function;
    }


    public void PointerDown()
    {
        if (pointerDownDelegate != null)
        {
            if (questInfo != null)
                pointerDownDelegate(questInfo);
        }
    }

    public void PointerEnter()
    {
        if (pointerEnterDelegate != null)
        {
            if (questInfo != null)
                pointerEnterDelegate(questInfo);
        }

        slotSelect.gameObject.SetActive(true);
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (questInfo != null)
                pointerExitDelegate(questInfo);
        }
        slotSelect.gameObject.SetActive(false);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit();
    }


    public void SetInfo(Quest info)
    {
        questInfo = info;

        

    }



    #region //추상 함수 정의부

    public override void Init()
    {
        slotSelect = UtilHelper.Find<Image>(transform, "SlotSelect",false,false);

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

