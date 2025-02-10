using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWind01 : Skill
{

    public override void Shoot()
    {
        
        getModel.getRigid2D.velocity = transform.right * getSkillInfo.speed;
        getSkillInfo.saveVelocity = getModel.getRigid2D.velocity;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster" || collision.tag == "OutWall")
        {
            collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);

            getModel.getColl2D.enabled = false;
            getModel.getRigid2D.velocity = Vector2.zero;
            getModel.getAnimator.SetTrigger("Finish");

        }


    }


}

