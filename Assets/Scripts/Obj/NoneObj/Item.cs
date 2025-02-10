using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameItemType
{
    None,

}

[System.Serializable]
public class ItemInfo : SaveInfo
{

    // 테이블에서 받아올 정보
    public ItemCategory category;
    public int iconCount;
    public int wearType;
    public int R;
    public int G;
    public int B;
    public int A;

    public int basicAttack;
    public int attack;
    public int basicDefence;
    public int defence;
    public int basicHp;
   



    public string explain;
    public string name;
    public string wearFull;

    public int price;


    // tableID, grade, uniquID 들은  saveInfo 에

    public ItemBitCategory bitCategory;
    public Sprite sprite;
    public Color color = Color.black;


}

public class Item : Unit
{
    
    #region _추상함수목록_
    public override void Idle()
    {

    }
    public override void Move()
    {

    }

    public override void Move(Vector2 dir)

    {

    }

    public override void Attack()
    {

    }

    public override void Attack(Vector2 dir)
    {

    }

    public override void Patrol()
    {

    }

    public override void Chase()
    {

    }

    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }

    #endregion 기능 함수 목록
}
