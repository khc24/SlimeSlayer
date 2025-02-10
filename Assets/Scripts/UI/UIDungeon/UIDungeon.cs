using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeon : BaseUI
{
    Button easyBtn;
    Button normalBtn;
    Button hardBtn;
    Button hellBtn;

    Button exitBackBtn;
    Button exitBtn;



    public void EasyBtn()
    {
        AudioMng.Instance.PlayUI("UI_Open");

        GameScene.stageStart = true;
        GameScene.SetDungeonMode = DungeonMode.EASY;
        GameScene.stageLv = 0;
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetBackImage = DataManager.miniMaps[0];
        SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene, 0);
         

        
        UIMng.Instance.FadeOut(1);
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();
    }

   

    public void NormalBtn()
    {

        AudioMng.Instance.PlayUI("UI_Open");

        GameScene.stageStart = true;
        GameScene.SetDungeonMode = DungeonMode.NORMAL;
        GameScene.stageLv = 1;
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetBackImage = DataManager.miniMaps[0];
        SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene, 1);


      
        UIMng.Instance.FadeOut(1);

        
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();


    }

    public void HardBtn()
    {
        AudioMng.Instance.PlayUI("UI_Open");

        GameScene.stageStart = true;
        GameScene.SetDungeonMode = DungeonMode.HARD;
        GameScene.stageLv = 3;
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetBackImage = DataManager.miniMaps[2];
        SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene, 3);


        
        UIMng.Instance.FadeOut(1);

        
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();

    }


    public void HellBtn()
    {

        AudioMng.Instance.PlayUI("UI_Open");

        GameScene.stageStart = true;

        GameScene.SetDungeonMode = DungeonMode.HELL;
        GameScene.stageLv = 4;
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetBackImage = DataManager.miniMaps[2];
        SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene, 4);


        
        UIMng.Instance.FadeOut(1);

        
        UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        UnitMng.Instance.Clear();
    }

    public void ExitBtn()
    {

        AudioMng.Instance.PlayUI("UI_Exit");
        
        gameObject.SetActive(false);
        UnitMng.Instance.Resume();
    }

    #region //추상 클래스 정의부
    public override void Init()
    {
        VerticalLayoutGroup v = GetComponentInChildren<VerticalLayoutGroup>(true);

        


        easyBtn = UtilHelper.FindButton(v.transform, "EasyBtn", EasyBtn);
        normalBtn = UtilHelper.FindButton(v.transform, "NormalBtn", NormalBtn);
        hardBtn = UtilHelper.FindButton(v.transform, "HardBtn", HardBtn);
        hellBtn = UtilHelper.FindButton(v.transform, "HellBtn", HellBtn);
        exitBtn = UtilHelper.FindButton(transform, "DungeonBox/UIExitBtn", ExitBtn);
        exitBackBtn = UtilHelper.FindButton(transform, "UIExitBackBtn", ExitBtn);




    }
    public override void Run()
    {
       


    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion

}
