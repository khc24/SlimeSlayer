using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPopupExit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject popup;
    void Start()
    {
        popup = GameObject.Find("UICharacterPopup");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        popup.gameObject.SetActive(false);
        AudioMng.Instance.PlayUI("UI_Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }


}
