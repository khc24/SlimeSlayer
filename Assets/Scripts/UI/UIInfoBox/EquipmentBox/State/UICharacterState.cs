using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterState : BaseUI
{
    public UICharacterSlot charItem;
    

    public void EquipItem(PlayerInfo info)
    {
        if (info != null && charItem != null)
        {
        
            charItem.SetInfo(info);
        }
            
    }
   

    #region //�߻� �Լ� ���Ǻ�
    public override void Init()
    {
        charItem = GetComponentInChildren<UICharacterSlot>();
        if (charItem != null) charItem.Init();

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
