using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{

    #region Scene에서 상속받은 함수들
    // 신 파일이 로드가 완료되는 시점에 호출되는 함수입니다.
    public override void Enter()
    {

        GameDB.currSceneType = SceneType.TitleScene;
        
        UIMng.Instance.modeChange();
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(false);
        UIMng.Instance.Get<UIMenu>(UIType.UIMenu).SetActive(false);
        UIMng.Instance.Get<UITitle>(UIType.UITitle).SetActive(true);
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("TitleBackground", 0.3f);

        Invoke("delayAction", 1.2f);
    }

    public void delayAction()
    {
        UIMng.Instance.Get<UITitle>(UIType.UITitle).ani.SetTrigger("Action");
        AudioMng.Instance.Play2DEffect("door_lock_slide_04");
      
    }

   
    public override void Exit()
    {
        UIMng.Instance.Get<UITitle>(UIType.UITitle).PosChange();
        UIMng.Instance.Get<UITitle>(UIType.UITitle).SetActive(false);
    }
  

    public override void Progress(float progress)
    {
        

    }

    #endregion Scene에서 상속받은 함수들
 
    


   
}
