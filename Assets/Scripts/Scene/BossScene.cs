using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : Scene
{

    #region Scene에서 상속받은 함수들
    
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

    #endregion Scene에서 상속받은 함수들


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
