using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where T : TSingleton<T>
// T라는 데이터 타입은 TSingleton<T>를 상속받은 클래스만 가능하다.
// 는 의미를 갖습니다. <강제조건>


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
