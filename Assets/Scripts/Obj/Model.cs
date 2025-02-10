using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Model : MonoBehaviour
{
    private Animator animator;
    
    private SpriteColor spriteColor;
    private Rigidbody2D rigid2D;
    private Collider2D coll2D;
  
    private Target target;



    public Animator getAnimator
    {
        get
        {
            return animator;
        }
    }

    

    public SpriteColor getSpriteColor
    {
        get
        {
            return spriteColor;
        }
    }

    public Rigidbody2D getRigid2D
    {
        get
        {
            return rigid2D;
        }
    }

    public Target getTarget
    {
        get
        {
            return target;
        }
    }

    public Collider2D getColl2D
    {
        get
        {
            return coll2D;
        }
    }



    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
       
        rigid2D = GetComponent<Rigidbody2D>();
        spriteColor = GetComponent<SpriteColor>();
        if (spriteColor != null) spriteColor.Init();
        target = GetComponent<Target>();
        if (target != null) target.Init();
        coll2D = GetComponent<Collider2D>();

        
    }

    public void SetTrigger(string trigger)
    {

        animator.SetTrigger(trigger);
    }

    public void Move(Vector2 velocity , bool colliderChek = false)
    {
        if (velocity == Vector2.zero)
            animator.SetBool("Move", false);
        
        else
        {
            if(IsTag("Move") == false)
                animator.SetTrigger("AttackCancel");
            animator.SetBool("Move", true);
        }

        if(colliderChek == false)
        {
            
            transform.Translate(velocity.normalized * velocity.magnitude * Time.deltaTime);
            
        }
        else
        {
            rigid2D.velocity = Vector2.zero;
        }

        
        

    }

    public void Attack()
    {
        
            animator.SetTrigger("Attack");   
    }

    public void Attack(Vector2 velocity)
    {
       
    }

    public bool IsTag(string tag, int layer = 0)
    {
       
        AnimatorStateInfo stateInfo =
            animator.GetCurrentAnimatorStateInfo(layer);

        return stateInfo.IsTag(tag);
    }
   
    public bool IsInTransition(int layer = 0)
    {
        return animator.IsInTransition(layer);
    }



}
