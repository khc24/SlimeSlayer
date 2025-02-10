using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInfoButton : BaseUI,
                                            IPointerDownHandler,
                                            IPointerUpHandler  
{
    SpriteColor spriteColor;
    

    #region BASEUI�κ��� ��ӹ��� �Լ����
    public override void Init()
    {
        spriteColor = GetComponent<SpriteColor>();
        if (spriteColor != null) spriteColor.Init();

        

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
    #endregion BASEUI�κ��� ��ӹ��� �Լ����

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameDB.targetRead == true || UIJoystick.downOn == true)
            return;
        AudioMng.Instance.PlayUI("UI_Button");
        
        UnitMng.Instance.Pause();

        UIInfoBox box = UIMng.Instance.Get<UIInfoBox>(UIType.UIInfoBox);
        box.SetUITab();
        box.EquipItem(GameDB.GetChar(GameDB.userInfo.GetCharUniqueID));

        

        box.SetActive(true);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        

    }

    public void DelayTime()
    {
        UIInfoBox box = UIMng.Instance.Get<UIInfoBox>(UIType.UIInfoBox);
        box.SetUITab();

        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(false);
    }


}
