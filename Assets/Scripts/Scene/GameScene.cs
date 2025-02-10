using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터 매니저
// 캐릭터 매니저 ( 비동기 로드하는 방법 )
public enum DungeonMode
{
    EASY = 1,
    NORMAL = 2,
    HARD = 4,
    HELL = 8
}


public class GameScene : Scene
{
    private static DungeonMode dungeonMode = DungeonMode.EASY;
    public static DungeonMode GetDungeonMode
    {
        get { return dungeonMode; }
    }

    public static DungeonMode SetDungeonMode
    {
        set { dungeonMode = value; }
    }

    private static int monsterSkillLevel = 1;

    public static int MonsterSkillLevel
    {
        set { monsterSkillLevel = value; }
        get { return monsterSkillLevel; }
    }

    public static bool isState = false;

    public static bool isClear = false;
    

    public static int stageLv = 0;
    public static bool stageStart = false;
    // 비동기로 생성할 캐릭터 집합
    List<SpawnPoint> spawnList = new List<SpawnPoint>();

    

    // 1. 생성할 캐릭터 리스트를 취합
    // 2. 비동기로 캐릭터 리스트를 생성
    // 3. 생성이된 캐릭터가 있다면 로딩 ui에 적용
    public IEnumerator IELoadAll()
    {
        SpawnPoint[] spawnArr = GameObject.FindObjectsOfType<SpawnPoint>();

        // spawnList는 현재 생성이 완료되지 않은 캐릭터 리스트를 가리킨다.
        spawnList.AddRange(spawnArr);

        // 생성할 캐릭터의 개수를 받는다.
        int totalCount = spawnList.Count;

        // 비동기 함수를 호출
        for (int i = 0; i < spawnList.Count; ++i)
            spawnList[i].Create();

        List<SpawnPoint> temp = new List<SpawnPoint>();

        float ratio = 0.3f;

        // 생성할 캐릭터가 있을때까지 계속 순회
        while (spawnList.Count > 0)
        {
            temp.Clear();
            foreach (SpawnPoint s in spawnArr)
            {
                // 아직 로드된 상태가 아니라면 temp값에 저장
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
        
        UnitMng.Instance.Pause();

        isClear = true;
        foreach(var unitValue in UnitMng.Instance.getUnitDic)
        {
            if (unitValue.Value.tag == "Portal")
                isClear = false;
        }

        yield return new WaitForSeconds(1.0f);
        


        
        UIMng.Instance.SetActive(UIType.LoadingUI, false);

        UnitMng.isUnitcreateComplete = true;
        


        UIMng.Instance.SetActive(UIType.UIIngame, true);
        UIMng.Instance.MinimapSetUpdate();
        ControlMng.Instance.SetEnable(true);

       


        
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("GameBackground", 0.3f);
        isState = true;
        
        Invoke("delayPause", 0.2f);

        foreach (var unitValue in UnitMng.Instance.getUnitDic)
        {
            if (unitValue.Value.tag == "Player")
            {
                Player p = unitValue.Value.GetComponent<Player>();
                
            }
        }
    }

    public void LoadAll()
    {

        StartCoroutine(IELoadAll());
    }

    #region Scene에서 상속받은 함수들
    
    public override void Enter()
    {
        UnitMng.pause = true;
        if(GameScene.stageLv == 2)
        {
            UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetBackImage = DataManager.miniMaps[1];
        }
        GameDB.SkillOrder = 101;
        GameDB.currSceneType = SceneType.GameScene;
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "CreateGrid2D");
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallCreate");
        if (stageStart == true)
        {
            GameDB.getGold = 0;
            UIMng.Instance.SetGetGold(GameDB.getGold);
            stageStart = false;
        }
        LoadAll();
        
        
        UIMng.Instance.modeChange();
        
    }

    
    public override void Exit()
    {
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallDestory");
        UIMng.Instance.MinimapSet(false);

        UnitMng.isUnitcreateComplete = false;

        isClear = false;
        isState = false;
       

    }
    
    
    public override void Progress(float progress)
    {
        // 아래처럼 코드를 작성하면 100% 신 파일이 로드되더라도 ui 슬라이더가 30%만 채워지는 상태가 된다.
        UIMng.Instance.CallEvent(UIType.LoadingUI, "SetValue", progress * 0.3f);
    }
    #endregion Scene에서 상속받은 함수들

    public void delayPause()
    {
        
        UnitMng.Instance.Resume();
    }
    private void OnGUI()
    {
        
    }

}
