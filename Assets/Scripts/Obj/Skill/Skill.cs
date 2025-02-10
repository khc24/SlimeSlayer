using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillInfo
{
    public string name;
    public int tableID = -1;
    public int LV = 1;
    public Color color = Color.black;
    public UnitType use = UnitType.None;
    public float originalAttack = 1;
    public int attack;
    public float originalSpeed = 1;
    public float speed;
    public Transform target = null;
    public Vector2 saveVelocity = Vector2.zero;
    
    
}

public class Skill : Unit
{
    [SerializeField]
    protected  SkillInfo skillInfo = new SkillInfo();

    float elapseed = 0f;
    bool isOn = true;

    public readonly Color colorBlack = Color.black;
    public DrawGizmos attackPivo;
    public SpriteRenderer sprite;
    public SkillInfo getSkillInfo
    {
        get { return skillInfo; }
    }

    public override void Init()
    {
        base.Init();
        
        attackPivo = GetComponentInChildren<DrawGizmos>();
        unitType = UnitType.Skill;
        targetType = TargetType.None;

        sprite = GetComponentInChildren<SpriteRenderer>(true);
        if (sprite != null) sprite.sortingOrder = GameDB.SkillOrder;



    }

    
    public void setInfo(int tableId, Color color, int LV = 1, UnitType use = UnitType.None, int attack = 1)
    {
        
        

        gameObject.tag = "Skill";
        skillInfo.use = use;
        skillInfo.tableID = tableId;
        skillInfo.LV = LV;
        skillInfo.color = color;
        skillInfo.attack = attack;
        skillInfo.speed = DataManager.ToF(TableType.SKILLTABLE, skillInfo.tableID, "SPEED");
        //skillInfo.speed = speed;


        if (skillInfo.use == UnitType.Player)
        {
            skillInfo.target = GameDB.targeting;
            gameObject.layer = LayerMask.NameToLayer("PlayerSkill");

        }
        else if(skillInfo.use == UnitType.Monster)
        {
           
            skillInfo.target = GameDB.playerPos;
            gameObject.layer = LayerMask.NameToLayer("MonsterSkill");
        }
        setUpdate();

    }

    public override void setUpdate()
    {
    
        skillInfo.name = DataManager.ToS(TableType.SKILLTABLE, skillInfo.tableID, "NAME");

        skillInfo.originalAttack = (DataManager.ToF(TableType.SKILLTABLE, skillInfo.tableID, "ATTACKPERCENT") * skillInfo.LV) +
                                                DataManager.ToF(TableType.SKILLTABLE, skillInfo.tableID, "ATTACK");
        skillInfo.attack = Mathf.FloorToInt(skillInfo.attack * (skillInfo.originalAttack/100));
       
       
        
    }

    public virtual void Shoot()
    {
        
    }


    


    public void OnColl2D()
    {
        if(getModel.getColl2D != null)
            getModel.getColl2D.enabled = true;
    }

    public void OffColl2D()
    {
        if (getModel.getColl2D != null)
            getModel.getColl2D.enabled = false;
    }

    public void SkillFinish()
    {
        Destroy(gameObject);
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

    private void Update()
    {
        if(!GameDB.isSkillOn)
        {
            if(isOn == true)
            {
                getModel.getRigid2D.velocity = Vector2.zero;

                if (getModel.getAnimator != null)
                    getModel.getAnimator.enabled = false;

                isOn = false;
            }
            
            return;
        }

        if(isOn == false)
        {
            getModel.getRigid2D.velocity = skillInfo.saveVelocity;
            if (getModel.getAnimator != null)
                getModel.getAnimator.enabled = true;

            isOn = true;
        }

        elapseed += Time.deltaTime;

        if(elapseed >= 5f)
        {
            Destroy(gameObject);
        }



    }

}
