using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class UtilHelper
{
   
    // Ȯ�� �޼ҵ带 Ȱ���� �Լ�.
    // Ȯ�� �޼ҵ带 ����Ҷ��� thisŰ���带 ����Ѵ�.
    // ComponentŬ������ Ȯ���Ų ������ 
    // ����Ƽ���� ����ڰ� ����ϴ� Ŭ������ ��κ�
    // MonoBehaviour�� ��ӹޱ� ������ ������ �Ǵ� Ŭ�����μ�
    // Component�� ����

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
        // ������ ������ ������ ��� �ִ� �ؽ�Ʈ ����
        // Resources.Load �Լ��� �ؽ�Ʈ ������ �м��ؼ�
        // �ش��ؽ�Ʈ�� ������ ���� �̹���, 3D�𵨸����� ������ 
        // �ε��ϴ� ������ ����
        
        T t = Resources.Load<T>(path);
        if (t == null)
            return null;

        // ������Ʈ�� ����
        // Instantiate �Լ��� �Ѱܹ��� �޸��� ������� ������
        // �޸� ������ Ȯ���ϰ� �޸𸮿� ����� �����͸�
        // �������ִ� �Լ�
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

