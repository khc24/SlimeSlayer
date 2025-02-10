using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo : ShareInfo
{
    // ���� �������� ���� �� �ֱ� ������ �� �������� �����ϱ� ���� ����ũ ���̵�
    public int uniqueID;

    // �������� ���̺����� ���̵�
    public int tableID;

    // ������ ���� ( ���̺� ���� �»� ����, �� �����ʹ� ������ ������ ����Ų��. )
    public int grade;

    // ������ ���� ( ������ ������ ���� ��ġ�� ����Ų��. )
    public int level;

    // ������ ���� ����( �������� ���¸� ����Ű�� ���� )
    public bool equip;

    // �������� �����ϰ� �ִ� ĳ���� ���̵�
    public int equipCharacter;

    public bool checkbox = false;

    
}

[System.Serializable]
public class SaveSkill
{
    public int level = 0;
    
    public int tableID = 0;

   
    
}

[System.Serializable]
public class SaveCharacter : SaveInfo
{
    public int jobType;
    public int[] equipItemArray = new int[3] {0, 0, 0};
    public List<SaveSkill> skillList = new List<SaveSkill>();
    public int skillPoint = 0;
    public bool isSkill1 = true;
    public bool isSkill2 = true;
    public bool isSkill3 = true;

    // �ش� ī�װ��� �������� �����Ű�� �Լ�
    public void SetWearing(int category, int itemUniqueID)
    {
        equipItemArray[category] = itemUniqueID;
    }

    // ī�װ��� �´� �������� ���������� üũ�ϴ� �Լ�
    public bool WearingTheEquipment(int category)
    {
        return equipItemArray[category] != 0;
    }
}