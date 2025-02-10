using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Mng<T> : TSingleton<T> where T : Mng<T>
{

    protected MngType mngType = MngType.None;
    // ������Ʈ �����ؾ� �� ����� ������
    public abstract void Run();

    // �Ƚ��� ������Ʈ���� ������ ���
    public abstract void FixRun();

    public abstract void LateRun();

    public abstract void Init();

    public abstract void OnActive();
    public abstract void OnDeactive();
    public abstract void OnGameEnable();
    public abstract void OnGameDisable();
    public abstract void SetActive(bool state);
    public abstract void SetEnable(bool state);

  
    public virtual void enableChang(MngType mngType, bool path = false)
    {
        if(path)
        {
            if ((GameDB.MngEnabled & (int)mngType) != (int)mngType)
                 GameDB.MngEnabled += (int)mngType;
        }
        else
        {
            if ((GameDB.MngEnabled & (int)mngType) == (int)mngType)
                GameDB.MngEnabled -= (int)mngType;
        }
    }
}

