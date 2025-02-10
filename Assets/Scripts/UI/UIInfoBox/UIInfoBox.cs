using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoBox : BaseUI
{
    UIEquipmentBox equipmentBox;

    public void SetUITab()
    {
        
        equipmentBox.SetUITab();
    }

    public void EquipItem(PlayerInfo info)
    {
        equipmentBox.EquipItem(info);
    }

    public override void OnDeactive()
    {
        base.OnDeactive();
        equipmentBox.Close();
    }

    #region //�߻� Ŭ���� ���Ǻ�
    public override void Init()
    {
        
        equipmentBox = GetComponentInChildren<UIEquipmentBox>(true);
        if(equipmentBox != null) equipmentBox.Init();

    }
    public override void Run()
    {
        if (equipmentBox != null && equipmentBox.isInit != false)
            equipmentBox.Run();
    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

}

