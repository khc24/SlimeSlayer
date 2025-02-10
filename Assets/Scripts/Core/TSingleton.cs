using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where T : TSingleton<T>
// T��� ������ Ÿ���� TSingleton<T>�� ��ӹ��� Ŭ������ �����ϴ�.
// �� �ǹ̸� �����ϴ�. <��������>


public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    

    private static T instance;

    public static T Instance
    {
        
        get
        {
            
            if ( instance == null )
            {
               
                instance = UtilHelper.CreateObject<T>(null, true);
                

                DontDestroyOnLoad(instance);
            }
                return instance;
        }
    }

    
    public virtual void Release()
    {
        Destroy(gameObject);
        instance = null;
    }
}
