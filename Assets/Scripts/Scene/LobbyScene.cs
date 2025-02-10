using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : Scene
{
    
    List<SpawnPoint> spawnList = new List<SpawnPoint>();

    public IEnumerator IELoadAll()
    {
        SpawnPoint[] spawnArr = GameObject.FindObjectsOfType<SpawnPoint>();

        
        spawnList.AddRange(spawnArr);

        
        int totalCount = spawnList.Count;

        
        for (int i = 0; i < spawnList.Count; ++i)
            spawnList[i].Create();

        List<SpawnPoint> temp = new List<SpawnPoint>();

        float ratio = 0.3f;

        
        while (spawnList.Count > 0)
        {
            temp.Clear();
            foreach (SpawnPoint s in spawnArr)
            {
                
                if (!s.IsCreated)
                    temp.Add(s);
                else
                {
                    ratio += (0.7f) / totalCount;
                    UIMng.Instance.CallEvent(UIType.LoadingUI, "SetValue", ratio);
                }
            }
            spawnList.Clear();
            spawnList.AddRange(temp);
            yield return null;

        }

        UnitMng.Instance.GetPlayer();
        yield return new WaitForSeconds(1.0f);

        
        
        UIMng.Instance.SetActive(UIType.LoadingUI, false);

        UnitMng.isUnitcreateComplete = true;
        
        UIMng.Instance.SetActive(UIType.UIIngame, true);
        ControlMng.Instance.SetEnable(true);

        foreach(var value in GameDB.charDic)
        {
            value.Value.currHp = value.Value.lastState[2];
        }

        
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("LobbyBackground", 0.3f);
        UIMng.Instance.SetEventSystme(true);
        UnitMng.Instance.ResumeAll();


        
    }

    public void LoadAll()
    {

        StartCoroutine(IELoadAll());
    }


    #region Scene에서 상속받은 함수들
    
    public override void Enter()
    {

       
        GameScene.isClear = false;
    
        GameScene.stageStart = false;
     

        GameDB.currSceneType = SceneType.LobbyScene;
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "CreateGrid2D");
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallCreate");
        LoadAll();
        UIMng.Instance.modeChange();
        
    }

    
    public override void Exit()
    {
        UIMng.Instance.Get<UIDungeon>(UIType.UIDungeon).SetActive(false);
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallDestory");

        
    }
  

    public override void Progress(float progress)
    {

    }
    #endregion Scene에서 상속받은 함수들

    private void OnGUI()
    {
     
    }

}
