using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class UIMinimap : BaseUI
{
    private UIMapIcon prefab;
    private UIRPGBackground rpgBackground;
    private Transform player;
    public List<UIMapIcon> mapIcons = new List<UIMapIcon>();


    public Sprite SetBackImage
    {
        set { if (rpgBackground != null) rpgBackground.SetBackImage = value; }
    }

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {
        prefab = Resources.Load<UIMapIcon>("Prefab/UI/UIMapIcon");
        rpgBackground = GetComponentInChildren<UIRPGBackground>();
        if(rpgBackground != null)   rpgBackground.Init();

        SetActive(false);
    }


    public override void Run()
    {
        if (gameObject.activeSelf == false)
            return;

        if(rpgBackground != null)
        rpgBackground.Run();

        
            iconSetting();
        


        foreach(var value in mapIcons)
        {
            value.Run();
        }
    }

    public override void Open()
    {
    }
    public override void Close()
    {
    }
    #endregion BASEUI로부터 상속받은 함수목록

    public void iconSetting()
    {
        bool death = false;
        List<UIMapIcon> tempIcons = new List<UIMapIcon>();

        foreach (var value in mapIcons)
        {
            if (value == null)
            {
                death = true;
            }
            else
            {
                tempIcons.Add(value);
            }

        }


        List<Unit> tempList = new List<Unit>();
        tempList.AddRange(UnitMng.Instance.GetMinimapIcons());




        if (death == true || tempList.Count != mapIcons.Count)
        {


            foreach (var value in tempIcons)
            {
                value.iconDestroy();
            }

            mapIcons.Clear();
            foreach (var value in tempList)
            {
                AddIcon(value.getModel.getTarget.getCenter, value.unitType);
            }

        }

    }


    public void AddIcon(Transform target , UnitType unitType)
    {


        UIMapIcon mapIcon = Instantiate(prefab, rpgBackground.transform);
        mapIcon.Init();
        mapIcon.SetTarget(target);

        if(unitType == UnitType.Monster)
        {
            mapIcon.SetColor(Color.red);
        }
        else if(unitType == UnitType.Item)
        {
            mapIcon.SetColor(Color.yellow);
        }

        mapIcons.Add(mapIcon);
    }

    public void SetUpdate()
    {
        if (GameDB.playerPos == null)
            return;
        
            player = GameDB.playerPos;

        if (rpgBackground != null)
        {
            rpgBackground.Init();
            rpgBackground.SetTarget(player);
        }

        // 3D 월드의 사이즈를 구한다.
        GameObject min = GameObject.Find("Min");
        GameObject max = GameObject.Find("Max");
        float worldWidth = 0;
        float worldDepth = 0;
        if (min != null && max != null && rpgBackground != null)
        {
            worldWidth = Mathf.Abs(min.transform.position.x - max.transform.position.x);
            worldDepth = Mathf.Abs(min.transform.position.y - max.transform.position.y);
            Vector2 sizeDelta = rpgBackground.SizeDelta;
            // 미니맵이 사용할 좌표 크기값을 설정
            MinimapHelper.Setting(worldWidth, worldDepth, sizeDelta.x, sizeDelta.y);
        }

        List<Unit> tempList = new List<Unit>();
        tempList.AddRange(UnitMng.Instance.GetMinimapIcons());

        foreach (var value in mapIcons)
        {
            if (value != null)
            {
                value.iconDestroy();
            }
        }

        mapIcons.Clear();
        foreach (var value in tempList)
        {
            AddIcon(value.getModel.getTarget.getCenter, value.unitType);
        }

        SetActive(true);

    }
    
}
