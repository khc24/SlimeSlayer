using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{

    #region Scene���� ��ӹ��� �Լ���
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ��Դϴ�.
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

    #endregion Scene���� ��ӹ��� �Լ���
 
    


   
}
