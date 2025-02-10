using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDark02 : Skill
{


    

    

    public override void Shoot()
    {
        
        getModel.getRigid2D.velocity = Vector2.zero;
        getSkillInfo.saveVelocity = Vector2.zero; 

       
    }

    private void SkillAttack()
    {

        Collider2D[] enemys = Physics2D.OverlapBoxAll(attackPivo.transform.position,
                            attackPivo.size,
                            0,
                            1 << LayerMask.NameToLayer("Monster"));

        foreach (var value in enemys)
        {
            value.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);

        }
    }


}

