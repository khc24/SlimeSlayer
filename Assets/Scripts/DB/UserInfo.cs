using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class UserInfo
{
    // 아이템을 구매하고 저장로드하기 위해서는 유니크한 식별 값이 있어야하며, 이 상태
    public int uniqueCount = 500;


    // 플레이어가 가지고 있는 돈
    public int money = 0;


    // 마지막에 선택했던 캐릭터 정보
    public int charUniqueID;

    // 직업
    public int jobType = (int)JobBit.WARRIOR;

    // 갖고 있는 캐릭터 목록
    public List<SaveCharacter> listOfChar = new List<SaveCharacter>();

    //// 갖고 있는 아이템 목록
    public List<SaveInfo> listOfItems = new List<SaveInfo>();

    public List<SaveQuest> listOfQuests = new List<SaveQuest> ();

    



    // 현재 선택된 캐릭터 id를 가져온다

    public SaveCharacter CurrCharacter
    {
        get { return Get(charUniqueID); }
    }

    public int GetCharUniqueID

    {
        get { return charUniqueID;}
    }

    public int SetCharUniqueID

    {
        set { charUniqueID = value; }
    }

    // 캐릭터 아이티를 통해 캐릭터 정보를 받아온다
    public SaveCharacter Get(int characterUniqueID)
    {
        foreach (var character in GameDB.charDic)
        {
            if (character.Value.uniqueID == characterUniqueID)
                return character.Value;
        }
        return null;
    }

  

    // 캐릭터가 착용하고 있아 아이템의 유니크 아이디를 받는다.
    public int GetIDOfItem(int charID, int category)
    {
        SaveCharacter character = Get(charID);

        if (character != null)
            return character.equipItemArray[category];

        return -1;
    }

    public void OnReset()
    {
        
    }

}
