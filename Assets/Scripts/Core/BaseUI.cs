using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseUI : MonoBehaviour
{

    public bool isInit = false;

    public abstract void Init();
    public abstract void Run();

    public abstract void Open();

    public abstract void Close();

    public virtual void OnActive()
    {
    }
    public virtual void OnDeactive()
    {

    }
    public virtual void OnUIEnable()
    {
    }
    public virtual void OnUIDisable()
    {

    }
    public virtual void Destroy(float delayTime = 0)
    {
        Object.Destroy(gameObject, delayTime);
    }
    public virtual void SetActive(bool state)
    {
        

        if (state)
        {
            OnActive();
        }
        else
        {
            OnDeactive();
        }
        gameObject.SetActive(state);
    }
    public virtual void SetEnable(bool state)
    {
        if (state)
        {
            OnUIEnable();
        }
        else
        {
            OnUIDisable();
        }
        enabled = state;
    }

   
}
