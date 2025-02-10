using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBase
{
    private Dictionary<int, Dictionary<string, string>> table =
        new Dictionary<int, Dictionary<string, string>>();

    public Dictionary<int, Dictionary<string, string>> Table
    {
        get { return table; }   
    }

    public void Load(string text)
    {
        // 개행 문자를 기준으로 문자열을 분리
        string[] rows = text.Split('\n');

        int length = rows.Length - 1;
        if (string.IsNullOrEmpty(rows[length]))
            length--;

        // 제한 라인수를 설정하였기 때문에 length까지 순회한다
        for (int i = 0; i <= length; ++i)
        {
            rows[i] = rows[i].Replace("\r", "");
        }
        // , 문자를 기준으로 분리
        string[] subjects = rows[0].Split(",");

        for (int r = 1; r <= length; ++r)
        {
            string[] columns = rows[r].Split(',');
            int tableID = -1;
            // 테이블 아이디를 받는다
            int.TryParse(columns[0], out tableID);

            // 메인키가 등록되어 있지 않다면 저장
            if (table.ContainsKey(tableID) == false)
                table.Add(tableID, new Dictionary<string, string>());

            for (int col = 1; col < columns.Length; ++col)
            {
                if (table[tableID].ContainsKey(subjects[col]) == false)
                {
                    table[tableID].Add(subjects[col], columns[col]);
                }
            }
        }
    }
    public string ToS(int tableID, string subKey)
    {
        string str = string.Empty;
        if (table.ContainsKey(tableID))
        {
            if (table[tableID].ContainsKey(subKey))
                str = table[tableID][subKey];
        }
        return str;
    }
    public int ToI(int tableID, string subKey)
    {
        int val = -1;
        if (table.ContainsKey(tableID))
        {
            if (table[tableID].ContainsKey(subKey))
                int.TryParse(table[tableID][subKey], out val);
        }
        return val;
    }
    public float ToF(int tableID, string subKey)
    {
        float val = -1;
        if (table.ContainsKey(tableID))
        {
            if (table[tableID].ContainsKey(subKey))
                float.TryParse(table[tableID][subKey], out val);
        }
        return val;
    }

}

