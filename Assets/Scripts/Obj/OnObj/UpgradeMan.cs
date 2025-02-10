using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMan : NPC
{

    public override void OnDealOpen()
    {
        AudioMng.Instance.PlayUI("UI_Open");

        
        UnitMng.Instance.Pause();


        Invoke("delayQuest", 0.1f);
    }

    public override void OnQuestOpen()
    {

    }

    public void OnQuest()
    {


        UIMng.Instance.OnTouchMode(npcInfo.name, true);


    }

    private void delayQuest()
    {
        UIUpgradeShop shop = UIMng.Instance.Get<UIUpgradeShop>(UIType.UIUpgradeShop);
        shop.SetUITab();
        UIMng.Instance.Get<UIUpgradeShop>(UIType.UIUpgradeShop).SetActive(true);
    }
}

