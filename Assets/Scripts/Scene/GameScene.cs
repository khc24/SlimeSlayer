using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �Ŵ���
// ĳ���� �Ŵ��� ( �񵿱� �ε��ϴ� ��� )
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
    // �񵿱�� ������ ĳ���� ����
    List<SpawnPoint> spawnList = new List<SpawnPoint>();

    

    // 1. ������ ĳ���� ����Ʈ�� ����
    // 2. �񵿱�� ĳ���� ����Ʈ�� ����
    // 3. �����̵� ĳ���Ͱ� �ִٸ� �ε� ui�� ����
    public IEnumerator IELoadAll()
    {
        SpawnPoint[] spawnArr = GameObject.FindObjectsOfType<SpawnPoint>();

        // spawnList�� ���� ������ �Ϸ���� ���� ĳ���� ����Ʈ�� ����Ų��.
        spawnList.AddRange(spawnArr);

        // ������ ĳ������ ������ �޴´�.
        int totalCount = spawnList.Count;

        // �񵿱� �Լ��� ȣ��
        for (int i = 0; i < spawnList.Count; ++i)
            spawnList[i].Create();

        List<SpawnPoint> temp = new List<SpawnPoint>();

        float ratio = 0.3f;

        // ������ ĳ���Ͱ� ���������� ��� ��ȸ
        while (spawnList.Count > 0)
        {
            temp.Clear();
            foreach (SpawnPoint s in spawnArr)
            {
                // ���� �ε�� ���°� �ƴ϶�� temp���� ����
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

    #region Scene���� ��ӹ��� �Լ���
    
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
        // �Ʒ�ó�� �ڵ带 �ۼ��ϸ� 100% �� ������ �ε�Ǵ��� ui �����̴��� 30%�� ä������ ���°� �ȴ�.
        UIMng.Instance.CallEvent(UIType.LoadingUI, "SetValue", progress * 0.3f);
    }
    #endregion Scene���� ��ӹ��� �Լ���

    public void delayPause()
    {
        
        UnitMng.Instance.Resume();
    }
    private void OnGUI()
    {
        
    }

}
