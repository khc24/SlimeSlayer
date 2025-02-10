using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class UserInfo
{
    // �������� �����ϰ� ����ε��ϱ� ���ؼ��� ����ũ�� �ĺ� ���� �־���ϸ�, �� ����
    public int uniqueCount = 500;


    // �÷��̾ ������ �ִ� ��
    public int money = 0;


    // �������� �����ߴ� ĳ���� ����
    public int charUniqueID;

    // ����
    public int jobType = (int)JobBit.WARRIOR;

    // ���� �ִ� ĳ���� ���
    public List<SaveCharacter> listOfChar = new List<SaveCharacter>();

    //// ���� �ִ� ������ ���
    public List<SaveInfo> listOfItems = new List<SaveInfo>();

    public List<SaveQuest> listOfQuests = new List<SaveQuest> ();

    



    // ���� ���õ� ĳ���� id�� �����´�

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

    // ĳ���� ����Ƽ�� ���� ĳ���� ������ �޾ƿ´�
    public SaveCharacter Get(int characterUniqueID)
    {
        foreach (var character in GameDB.charDic)
        {
            if (character.Value.uniqueID == characterUniqueID)
                return character.Value;
        }
        return null;
    }

  

    // ĳ���Ͱ� �����ϰ� �־� �������� ����ũ ���̵� �޴´�.
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
