using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIExit : MonoBehaviour, IPointerDownHandler,
                                     IPointerUpHandler
{
   

    public void OnPointerDown(PointerEventData eventData)
    {

    
        UnitMng.Instance.Resume();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        

    }


}
