using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    #region _추상함수목록_


    public override void Attack()
    {
        if (GameDB.player == null)
        {
            currState = UnitState.Idle;
        }

        

        if (Vector3.Distance(getModel.getTarget.getCenter.position, GameDB.playerPos.position) > 1.8f || screenIn == false)
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


        if (Vector3.Distance(getModel.getTarget.getCenter.position, GameDB.playerPos.position) <= 1.8f &&
            screenIn == true && arrCheck == true)
        {
            
            arrCheck = false;
            Vector3 ShPos = getModel.getTarget.getCenter.position;
            Vector3 ShDir = getModel.getTarget.getTargetPosDir;
            ShDir.Normalize();

            ShPos += (ShDir * 0.5f);

            RaycastHit2D hit = Physics2D.CircleCast(ShPos, 0.2f, getModel.getTarget.getCenter.right.normalized, 5f,
                                                     1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("OutWall")
                                                     | 1 << LayerMask.NameToLayer("InWall"));

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

        if (GameDB.playerPos == null)
            return;

        Collider2D coll = Physics2D.OverlapCircle(GameDB.playerPos.position, 0.3f, 1 << LayerMask.NameToLayer("Player"));
        
        if(coll != null)
        {
            coll.SendMessage("SetDamage", monsterInfo.lastState[0], SendMessageOptions.DontRequireReceiver);
            Hit hit = Resources.Load<Hit>("Prefab/Hit");

            hit = Instantiate(hit, coll.transform.position, Quaternion.identity);
            hit.Init();
        }
          
        
    }
}

