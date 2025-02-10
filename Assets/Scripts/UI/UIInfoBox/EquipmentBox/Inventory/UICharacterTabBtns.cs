using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterTabBtns : MonoBehaviour
{
    private List<UICharacterTab> uiTabs = new List<UICharacterTab>();
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
        uiTabs.AddRange(GetComponentsInChildren<UICharacterTab>(true));

        foreach (var value in uiTabs)
        {
            value.Init();
        }

    }

    public void SetHighlightActive(Job category)
    {
        foreach (var value in uiTabs)
        {
            if (value.JobCategory == category)
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


    public Vector3 GetFocusPos(Job category)
    {
        foreach(var value in uiTabs)
        {
            if(value.JobCategory == category)
            {
                focusPos = value.Focus.position;
            }
        }

        return focusPos;
    }

    public void SetButtonListener(System.Action<UICharacterTab> action)
    {
        foreach(var value in uiTabs)
        {
            value.SetButtonListener(action);
        }

    }

  
}
