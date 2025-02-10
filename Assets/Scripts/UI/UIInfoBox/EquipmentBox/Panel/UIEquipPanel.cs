using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipPanel : BaseUI
{

    
    public Dictionary<ItemCategory,UIInvenItem> invenDic = new Dictionary<ItemCategory, UIInvenItem>();


    public void EquipItem(ItemInfo info)
    {
        if(invenDic.ContainsKey(info.category))
        {
            invenDic[info.category].SetInfo(info);
        }
    }
    public void TakeOffEquipment(ItemCategory category)
    {
        if (invenDic.ContainsKey(category))
            invenDic[category].Clear();
    }

    public void TakeOffEquipmentAll()
    {
        foreach(var value in invenDic.Values)
        {
            value.Clear();
        }
    }

    public void EquipItemAll(int[] array)
    {
        foreach(var value in array)
        {
            ItemInfo i = GameDB.GetItem(value);
            if(i != null)
            {
                EquipItem(i);
            }
        }
    }

    #region //추상 함수 정의부
    public override void Init()
    {
        List<UIInvenItem> list = new List<UIInvenItem>();
        list.AddRange(GetComponentsInChildren<UIInvenItem>(true));

        foreach(var value in list)
        {
            if(!invenDic.ContainsKey(value.itemInfo.category))
            {
                value.Init();
                invenDic.Add(value.itemInfo.category, value);
            }
        }

        isInit = true;
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
