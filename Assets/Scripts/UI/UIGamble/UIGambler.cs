using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGambler : BaseUI
{
   
    GridLayoutGroup gridLayout;

    public Button charBtn1;
    public Button charBtn10;
    public Button itemBtn1;
    public Button itemBtn10;

    
    private System.Action charBtn1Delegate;
    private System.Action charBtn10Delegate;
    private System.Action itemBtn1Delegate;
    private System.Action itemBtn10Delegate;


    public void SetCharBtn1Delegate(System.Action function)
    {
        charBtn1Delegate = function;
    }

    public void SetCharBtn10Delegate(System.Action function)
    {
        charBtn10Delegate = function;
    }

    public void SetItemBtn1Delegate(System.Action function)
    {
        itemBtn1Delegate = function;
    }

    public void SetItemBtn10Delegate(System.Action function)
    {
        itemBtn10Delegate = function;
    }

    public void OnClickCharBtn1()
    {
        if (charBtn1Delegate != null)
        {
            charBtn1Delegate();
        }
    }

    public void OnClickCharBtn10()
    {
        if (charBtn10Delegate != null)
        {
            charBtn10Delegate();
        }
    }

    public void OnClickItemBtn1()
    {
        if (itemBtn1Delegate != null)
        {
            itemBtn1Delegate();
        }
    }

    public void OnClickItemBtn10()
    {
        if (itemBtn10Delegate != null)
        {
            itemBtn10Delegate();
        }
    }


    #region //추상 함수 정의부

    public override void Init()
    {
       

        gridLayout = GetComponentInChildren<GridLayoutGroup>(true);
        charBtn1 = UtilHelper.FindButton(gridLayout.transform, "CharacterGamble/CharBtn1", OnClickCharBtn1);
        charBtn10 = UtilHelper.FindButton(gridLayout.transform, "CharacterGamble/CharBtn10", OnClickCharBtn10);
        itemBtn1 = UtilHelper.FindButton(gridLayout.transform, "ItemGamble/ItemBtn1", OnClickItemBtn1);
        itemBtn10 = UtilHelper.FindButton(gridLayout.transform, "ItemGamble/ItemBtn10", OnClickItemBtn10);
        


        isInit = true;

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
