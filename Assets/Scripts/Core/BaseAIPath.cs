using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAIPath : MonoBehaviour
{
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
    public virtual void OnEnable()
    {
    }
    public virtual void OnDisable()
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
            OnEnable();
        }
        else
        {
            OnDisable();
        }
        enabled = state;
    }

    
}

