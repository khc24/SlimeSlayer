using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpMng : BaseUI
{
    public List<UIHp> hpList = new List<UIHp>();

    public UIHp UIHpCreate(Transform pivot, ShareInfo info)
    {
        
            UIHp hp = Resources.Load<UIHp>("Prefab/UI/UIHp");
        
            if (hp == null)
            return null;
        
        hp = Instantiate(hp, pivot.position, Quaternion.identity,transform);
        hp.Init();
        hp.HpPivot = pivot;
        hp.info = info;
        
        hpList.Add(hp);
        return hp;
    }

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {

    }
    public override void Run()
    {
        
        List<UIHp> temp = new List<UIHp>();

        foreach (var value in hpList)
        {
            if(value !=null)
                temp.Add(value);
        }

        hpList.Clear();
        hpList.AddRange(temp);

        foreach (var value in hpList)
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
}
