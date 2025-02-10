using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    #region _추상함수목록_
  

    public override void Attack()
    {
        if(GameDB.player == null)
        {
            currState = UnitState.Idle;
        }

        if (Vector3.Distance(getModel.getTarget.getCenter.position, GameDB.playerPos.position) >= 3.8f || screenIn == false)
        {
            currState = UnitState.Chase;
        }

        if (getModel.IsTag("Attack") || getModel.IsTag("Damage"))
        {
            return;
        }


        path.path.Clear();
        getModel.Attack();
        charDir(getModel.getTarget.getTargetPosDir);
    }

    public override void Chase()
    {
        base.Chase();
        if (GameDB.player == null)
            return;

        if (Vector3.Distance(getModel.getTarget.getCenter.position, GameDB.playerPos.position) < 3.8f &&
            screenIn == true && arrCheck == true )
        {
            arrCheck = false;
            

            Vector3 ShPos = getModel.getTarget.getCenter.position;
            Vector3 ShDir = getModel.getTarget.getTargetPosDir;
            ShDir.Normalize();

            ShPos += (ShDir * 0.5f);

            

            RaycastHit2D hit = Physics2D.CircleCast(ShPos, 0.2f, getModel.getTarget.getCenter.right.normalized, 5f,
                                                     1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("OutWall"));

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    currState = UnitState.Attack;
                }
            }


        }
        arrCheck = false;
    }
    

    #endregion _추상함수목록_

    public void Shoot()
    {

        Vector3 ShPos = getModel.getTarget.getCenter.position;
        Vector3 ShDir = getModel.getTarget.getTargetPosDir;
        ShDir.Normalize();


        ShPos += (ShDir * 0.6f);

        Skill attack1 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 3, "MODEL"));
        




        attack1 = Instantiate(attack1, ShPos, getModel.getTarget.getCenter.rotation);

        if(getModel.getTarget.getTargetPosDir.x < 0)
        {
            attack1.transform.localScale = new Vector3(1,-1,1);
        }

        attack1.Init();
        attack1.setInfo(3, Color.black, 1, this.unitType, monsterInfo.lastState[0]);
        attack1.Shoot();
       
    }
}
