using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class UtilHelper
{
   
    // 확장 메소드를 활용한 함수.
    // 확장 메소드를 사용할때는 this키워드를 사용한다.
    // Component클래스를 확장시킨 이유는 
    // 유니티에서 사용자가 사용하는 클래스가 대부분
    // MonoBehaviour를 상속받기 때문에 기준이 되는 클래스로서
    // Component를 선택

    public static T CreateObject<T>(Transform parent,
                                  bool init = false) where T : Component

    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        if (init)
            obj.SendMessage("Init",
                            SendMessageOptions.DontRequireReceiver);

        
        return obj.GetComponent<T>();
    }
    public static T CreateObject<T>(this Component component,
                                      Transform parent,
                                      bool init = false) where T : Component

    {
        return CreateObject<T>(parent, init);
    }

    public static T Instantiate<T>(this Component component,
                                   string path,
                                   Vector3 pos,
                                   Quaternion rot,
                                   bool init = false,
                                   Transform parent = null) where T : Component
    {
        // 프리팹 파일은 정보를 담고 있는 텍스트 파일
        // Resources.Load 함수는 텍스트 파일을 분석해서
        // 해당텍스트의 정보에 따라 이미지, 3D모델링등의 파일을 
        // 로드하는 역할을 수행
        
        T t = Resources.Load<T>(path);
        if (t == null)
            return null;

        // 오브젝트를 생성
        // Instantiate 함수는 넘겨받은 메모리의 사이즈와 동일한
        // 메모리 공간을 확보하고 메모리에 담겨진 데이터를
        // 복사해주는 함수
        t = Object.Instantiate<T>(t, pos, rot, parent);
        if (init)
        {
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }
            

        return t;
    }

    public static T Find<T>(this Component component,
                             Transform t,
                             string path,
                             bool active = true,
                             bool init = false) where T : Component
    {
        Transform fObj = t.Find(path);
        if( fObj != null )
        {
            fObj.gameObject.SetActive(active);
            if (init)
            {
                fObj.SendMessage("Init",
                            SendMessageOptions.DontRequireReceiver);
            }
            return fObj.GetComponent<T>();
        }
        return null;
    }


    public static T Find<T>(Transform t, string path
                            , bool init = false, bool active = true) where T : Component
    {
        Transform fobj = t.Find(path);
        if (fobj != null)
        {
            if (init)
            {
                fobj.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
            }

            fobj.gameObject.SetActive(active);

            return fobj.GetComponent<T>();
        }


        return null;
    }

    public static T[] FindAll<T>(Transform t, string path
                            , bool init = false, bool active = true) where T : Component
    {
        Transform fobj = t.Find(path);
        if (fobj != null)
        {
            T[] arr = fobj.GetComponentsInChildren<T>(true);

            foreach (var value in arr)
            {
                if (init)
                {
                    value.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
                }

                value.gameObject.SetActive(active);
            }
            return arr;
        }
        return null;
    }

    public static Button FindButton(Transform transform, string path, UnityAction action)
    {
        
        Button btn = Find<Button>(transform, path);
        if (btn != null)
            btn.onClick.AddListener(action);

        return btn;
    }

}

