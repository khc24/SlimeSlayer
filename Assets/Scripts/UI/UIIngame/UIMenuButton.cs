using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMenuButton : BaseUI,
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
        
        UIMng.Instance.Get<UIMenu>(UIType.UIMenu).SetActive(true);
        
        UnitMng.Instance.Pause();
        


    }

    public void OnPointerUp(PointerEventData eventData)
    {


    }



}
