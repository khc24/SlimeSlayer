using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharGamblePopup : BaseUI
{

    private List<UIGambleChar> gambleCharList = new List<UIGambleChar>();

    private Button button;
    private System.Action onClickDelegate;

    public void OnClick()
    {
        Close();
        if (onClickDelegate != null)
        {
            onClickDelegate();
        }
            
    }

    
    public void SetOnCLickDelegate(System.Action action)
    {
        onClickDelegate = action;
    }


    public void SetGambleCharInfo(List<PlayerInfo> list)
    {
        int count = 0;
        foreach (var value in list)
        {
            gambleCharList[count++].SetInfo(value);
        }

        gameObject.SetActive(true);
    }

    public void SetPointerEnter(System.Action<PlayerInfo> action)
    {
        foreach (var value in gambleCharList)
        {
            value.SetPointerEnterDelegate(action);
        }
    }

    public void SetPointerExit(System.Action<PlayerInfo> action)
    {
        foreach (var value in gambleCharList)
        {
            value.SetPointerExitDelegate(action);
        }
    }


    public void SetRayCastAll(bool path, PlayerInfo charPivot = null)
    {


        foreach (var value in gambleCharList)
        {
            value.SetRayCast(path, charPivot);
        }

    }

    #region //추상 함수 정의부



    public override void Init()
    {
        gambleCharList.AddRange(GetComponentsInChildren<UIGambleChar>(true));
        button = GetComponent<Button>();
        if (button != null)

        {
            button.onClick.AddListener(OnClick);
            
        }
            

        foreach(var value in gambleCharList)
        {
            value.Init();
            
        }
    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
        foreach (var value in gambleCharList)
        {
            value.Close();
        }

        gameObject.SetActive(false);
    }

    #endregion
}

