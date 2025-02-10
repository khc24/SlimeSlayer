using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGoblinKnife : Skill
{
   
    
    public override void Shoot()
    {
       
        getModel.getSpriteColor.SetColor(getSkillInfo.color);


        
        getModel.getSpriteColor.Execute(getSkillInfo.color,
                                    new Color(getSkillInfo.color.r * 0.8f, getSkillInfo.color.g * 0.8f, getSkillInfo.color.b * 0.8f, 0.8f),
                                    ColorMode.Pingpong,
                                    0.1f);

       
        getModel.getRigid2D.velocity = transform.right * getSkillInfo.speed;
        getSkillInfo.saveVelocity = getModel.getRigid2D.velocity;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ( collision.tag == "Player" || collision.tag == "OutWall")
        {
            collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);

            Hit hit = Resources.Load<Hit>("Prefab/Hit");

            Vector3 v = transform.position;

            if (attackPivo != null)
            {
                v = attackPivo.transform.position;
            }

            hit = Instantiate(hit, v, Quaternion.identity);
            hit.Init();


            Destroy(gameObject);
        }
    }

}
