using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpgradeExit : MonoBehaviour,
                                            IPointerDownHandler,
                                            IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioMng.Instance.PlayUI("UI_Exit");
        UIMng.Instance.Get<UIUpgradeShop>(UIType.UIUpgradeShop).SetActive(false);
        UIMng.Instance.Get<UIUpgradeShop>(UIType.UIUpgradeShop).Close();
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(true);

        UnitMng.Instance.Resume();
    }

    public void OnPointerUp(PointerEventData eventData)
    {



    }
}

