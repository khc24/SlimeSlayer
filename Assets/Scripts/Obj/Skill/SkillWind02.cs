using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWind02 : Skill
{

    public override void Shoot()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster")
        {
            collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);

        }

    }


}

