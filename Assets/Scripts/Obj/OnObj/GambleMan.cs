using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleMan : NPC
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
        UIMng.Instance.Get<UIGamble>(UIType.UIGamble).SetActive(true);
  
    }
}
