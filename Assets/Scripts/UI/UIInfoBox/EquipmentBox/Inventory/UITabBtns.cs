using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabBtns : MonoBehaviour
{
    private List<UITab> uiTabs = new List<UITab>();
    private Vector3 focusPos = Vector3.zero;
    private HorizontalLayoutGroup horizontal;

    public Vector3 FocusPos
    {
        get
        {
            return focusPos;
        }
    }

    public void Init()
    {
        uiTabs.AddRange(GetComponentsInChildren<UITab>(true));

        foreach (var value in uiTabs)
        {
            value.Init();
        }

    }

    public void SetHighlightActive(ItemCategory category)
    {
        foreach (var value in uiTabs)
        {
            if (value.Category == category)
            {
                focusPos = value.Focus.position;
                value.SetHighlightActive(true);
            }
            else
            {
                value.SetHighlightActive(false);
            }
        }

    }


    public Vector3 GetFocusPos(ItemCategory category)
    {
        foreach(var value in uiTabs)
        {
            if(value.Category == category)
            {
                focusPos = value.Focus.position;
               
            }
        }

        return focusPos;
    }

    public void SetButtonListener(System.Action<UITab> action)
    {
        foreach(var value in uiTabs)
        {
            value.SetButtonListener(action);
        }

    }

 
}
