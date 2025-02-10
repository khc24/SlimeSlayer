using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupExit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject popup;
    void Start()
    {
        popup = GameObject.Find("UIItemPopup");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioMng.Instance.PlayUI("UI_Exit");
        popup.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }



}
