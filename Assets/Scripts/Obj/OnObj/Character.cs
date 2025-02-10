using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;






[System.Serializable]
public class CharInfo
{
    public int uniqueID;
    public int tableID;
    public int LV = 1;
    public string name;
    public int maxAttack;
    public int attack;
    public int maxDefence;
    public int defence;
    public int maxHp;
    public int hp;
    public int currHp;
    public float speed;
    public float currSpeed;
    
    public int weaponID = 4;

}



public  class Character : Unit
{

    
   

    public List<Weapon> weapons;
    public Weapon weapon;

    private bool damageState = false;
    private float damageElap = 0;
    private float damageTime = 1;
    private bool isDead = false;


    [SerializeField]
    protected CharInfo charInfo = new CharInfo();


    public override void setWeapon(int tableID)
    {
        

    }

   

    public override void setUpdate()
    {

        charInfo.hp = (int)(charInfo.maxHp * (charInfo.LV / 20f));
        charInfo.currHp = charInfo.hp;
        charInfo.attack = (int)(charInfo.maxAttack * (charInfo.LV / 20f));
        charInfo.currSpeed = 2f * charInfo.speed;



    }



   

    public override void Init()
    {

       base.Init();

        weapons.AddRange(GetComponentsInChildren<Weapon>(true));
        
        if (weapons.Count >= 1)
        {
            foreach (var value in weapons)
            {
                value.Init();
                if (value.spriteColor != null)
                    weapon = value;
            }
        }

    }

    

    public override void SetDamage(int attack)
    {
        damageState = true;
        damageElap = 0;
        int damage = charInfo.defence - attack;
        if (damage < 0)
            charInfo.currHp += damage;
        if (charInfo.currHp <= 0)
        {
            isDead = true;
            getModel.getSpriteColor.Execute(Color.black,
                                new Color(0, 0, 0, 0),
                                ColorMode.One,
                                1.0f,
                                true);
            Destroy(gameObject, 1.0f);


            if (getModel.getRigid2D != null)
                getModel.getRigid2D.isKinematic = true;

            if (getModel.getColl2D != null)
                getModel.getColl2D.enabled = false;
            
            enabled = false;
        }
        else
        {
            getModel.getSpriteColor.Execute(Color.black,
                    Color.red,
                    ColorMode.One,
                    0.2f);

        }
    }


 

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
