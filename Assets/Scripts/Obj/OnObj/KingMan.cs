using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMan : NPC
{

    

    public override void OnDealOpen()
    {
        
    }

    public override void OnQuestOpen()
    {
        
    }


    public void OnQuest()
    {
        UIMng.Instance.OnTouchMode(npcInfo.name,false);
    }

    private void delayQuest()
    {
        
    }
}

