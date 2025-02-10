using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemGamblePopup : BaseUI
{

    private List<UIGambleItem> gambleItemList = new List<UIGambleItem>();

    private Button button;
    private System.Action onClickDelegate;


    public void OnClick()
    {
        Close();

        if (onClickDelegate != null)
            onClickDelegate();
    }

    public void SetOnCLickDelegate(System.Action action)
    {
        onClickDelegate = action;
    }

    public void SetGambleItemInfo(List<ItemInfo> list)
    {
        int count = 0;
        foreach (var value in list)
        {
            gambleItemList[count++].SetInfo(value);
        }

        gameObject.SetActive(true);
    }

    public void SetPointerEnter(System.Action<ItemInfo> action)
    {
        foreach (var value in gambleItemList)
        {
            value.SetPointerEnterDelegate(action);
        }
    }

    public void SetPointerExit(System.Action<ItemInfo> action)
    {
        foreach (var value in gambleItemList)
        {
            value.SetPointerExitDelegate(action);
        }
    }


    public void SetRayCastAll(bool path, ItemInfo itemPivot = null)
    {


        foreach (var value in gambleItemList)
        {
            value.SetRayCast(path, itemPivot);
        }

    }

    #region //추상 함수 정의부



    public override void Init()
    {
        gambleItemList.AddRange(GetComponentsInChildren<UIGambleItem>(true));
        button = GetComponent<Button>();
        if (button != null)
        {
            
            button.onClick.AddListener(OnClick);
        }
            

        foreach (var value in gambleItemList)
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
        foreach (var value in gambleItemList)
        {
            value.Close();
        }

        gameObject.SetActive(false);
    }

    #endregion
}
