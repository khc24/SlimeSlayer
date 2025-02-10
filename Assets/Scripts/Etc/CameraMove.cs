using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 8f;

    public Transform setTarget
    {
        set { target = value; }
    }

    public float setSpeed
    {
        set { speed = value; }
    }

    
    void Start()
    {
        int monster = LayerMask.NameToLayer("Monster");
        int player = LayerMask.NameToLayer("Player");
        int playerSkill = LayerMask.NameToLayer("PlayerSkill");
        int monsterSkill = LayerMask.NameToLayer("MonsterSkill");
        int outWall = LayerMask.NameToLayer("OutWall");
        int inWall = LayerMask.NameToLayer("InWall");


        Physics2D.IgnoreLayerCollision(player, player);
        Physics2D.IgnoreLayerCollision(player, monster);
        Physics2D.IgnoreLayerCollision(monster, monster);

        Physics2D.IgnoreLayerCollision(playerSkill, player);
        Physics2D.IgnoreLayerCollision(playerSkill, playerSkill);
        Physics2D.IgnoreLayerCollision(playerSkill, monsterSkill);


        Physics2D.IgnoreLayerCollision(monsterSkill, monster);
        Physics2D.IgnoreLayerCollision(monsterSkill, monsterSkill);

        Physics2D.IgnoreLayerCollision(outWall, inWall);



    }

   

    public void LateRun()
    {
        if (target == null)
            return;

        Vector3 tempPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * speed);
        

    }

   

}
