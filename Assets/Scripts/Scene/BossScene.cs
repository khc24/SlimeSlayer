using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : Scene
{

    #region Scene���� ��ӹ��� �Լ���
    
    public override void Enter()
    {
        GameDB.currSceneType = SceneType.BossScene;
        UIMng.Instance.modeChange();
    }

    
    public override void Exit()
    {

    }
    

    public override void Progress(float progress)
    {
       
        
    }

    #endregion Scene���� ��ӹ��� �Լ���


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
