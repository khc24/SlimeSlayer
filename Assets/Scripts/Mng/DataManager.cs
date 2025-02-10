using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager 
{
    private static Dictionary<TableType, LowBase> tableList =
        new Dictionary<TableType, LowBase>();

    public static Sprite[] items = Resources.LoadAll<Sprite>("Sprite/Item/Weapons");
    public static Sprite[] miniMaps = Resources.LoadAll<Sprite>("Sprite/UIMinimap_images");
    public static Dictionary<int, Sprite> skillIcons = new Dictionary<int, Sprite>();


    public static LowBase Get(TableType tableType)
    {
        
        Load(tableType);
        LowBase lowBase = null;
        if (tableList.ContainsKey(tableType))
            lowBase = tableList[tableType];
       

        return lowBase;
    }

    public static string ToS(TableType tableType, int tableID, string subKey)
    {
        string str = string.Empty;
        if (tableList.ContainsKey(tableType))
            str = tableList[tableType].ToS(tableID, subKey);
        return str;
    }
    public static int ToI(TableType tableType, int tableID, string subKey)
    {
        int val = -1;
        if (tableList.ContainsKey(tableType))
            val = tableList[tableType].ToI(tableID, subKey);
        return val;
    }
    public static float ToF(TableType tableType, int tableID, string subKey)
    {
        float val = -1;
        if (tableList.ContainsKey(tableType))
            val = tableList[tableType].ToF(tableID, subKey);
        return val;
    }
    public static LowBase Load(TableType tableType, string path = "DB/")
    {
        LowBase lowBase = null;
        if (tableList.ContainsKey(tableType))
            lowBase = tableList[tableType];
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path + tableType);
            
            if (textAsset != null)
            {
                lowBase = new LowBase();
                lowBase.Load(textAsset.text);
                tableList.Add(tableType, lowBase);
            }
        }
        return lowBase;
    }
}

