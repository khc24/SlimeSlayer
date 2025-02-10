using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareInfo
{
    public int spawnID;
    public int hp;
    public int currHp;
    public int[] lastState = new int[] { 0, 0, 0 };

}

[RequireComponent(typeof(Model))]
public abstract class Unit : MonoBehaviour
{

    private Model model;
    public UnitType unitType = UnitType.None;
    public TargetType targetType = TargetType.None;
    

    public Vector3 Left = new Vector3(-1, 1, 1);
    public Vector3 Right = new Vector3(1, 1, 1);

    public Transform HpPivot;
    public Transform dialogPivot;

    public SceneType sceneType = SceneType.None;

    public bool screenIn = false;

    public bool isDie = false;

    public Model getModel
    {
        get
        {
            return model;
        }
    }


    #region // baseObj

    public virtual void Init()
    {
        isDie = false;
        model = GetComponent<Model>();
       
        if(model != null)   model.Init();

         HpPivot = transform.Find("HpPivot");

    }
    public virtual void Run()
    {

    }

    public virtual void Open()
    {

    }

    public virtual void Close()
    {

    }


    #endregion


    #region _추상함수목록_
    public abstract void Idle();
    public abstract void Move();

    public abstract void Move(Vector2 dir);

    public abstract void Attack();

    public abstract void Attack(Vector2 dir);

    public abstract void Patrol();

    public abstract void Chase();

    public abstract void Skill1();

    public abstract void Skill2();
    #endregion _추상함수목록_


    public virtual void OnTouchMassage()
    {

    }
    public virtual void OnDealOpen()
    {

    }

    public virtual void OnQuestOpen()
    {

    }

    public virtual void Pause()
    {

        

        if (model.getAnimator != null)
            model.getAnimator.enabled = false;
       
        if(model.getTarget.getAnimator != null) model.getTarget.getAnimator.enabled = false;

        if (model.getRigid2D != null)
        {
           
            model.getRigid2D.simulated = false;
        }
            


    }
    public virtual void Resume()
    {
        if (model.getAnimator != null)
            model.getAnimator.enabled = true;

        if (model.getTarget.getAnimator != null) model.getTarget.getAnimator.enabled = true;

        if (model.getRigid2D != null)
        {
           
            model.getRigid2D.simulated = true;
        }
            
    }

    public virtual void Destroy(float delayTime = 0)
    {
        Object.Destroy(gameObject, delayTime);
    }


    public virtual void setWeapon(int tableID)
    {
      

    }

    public virtual float GetHP()
    {
        return -1f;
    }

    public virtual void setInfo(int spawnID, int tableID, int LV)
    {

    }

    public virtual void setUpdate()
    {
    }


    public virtual void charDir()
    {
        if (getModel.getRigid2D.velocity.x > 0)
            transform.localScale = Right;
        else if (getModel.getRigid2D.velocity.x < 0)
            transform.localScale = Left;
    }

    public virtual void charDir(Vector3 dir)
    {
        if (dir.x > 0)
        {
            transform.localScale = Right;
            getModel.getTarget.getCenter.localScale = Right;
        }


        else if (dir.x < 0)
        {
            transform.localScale = Left;
            Vector3 v = Left;
            v.y = -1;
            getModel.getTarget.getCenter.localScale = v;
        }

    }

    public virtual void SetDamage(int attack)
    {
        
    }


    public virtual void setWeapon()
    {

    }


}