using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameState : MonoBehaviour
{
    
   

    public void GameClear()
    {
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).Open(true);
    }

    public void GameFail()
    {
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).Open(false);
    }

    
}
