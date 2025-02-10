using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGameStateBox : MonoBehaviour
{
    Transform fail;
    Transform clear;

    public void Init()
    {
        clear = transform.Find("CLRER");
        fail = transform.Find("FAIL");
    }

    public void finishMode(int num)
    {
        if(num == 1)
        {
            clear.gameObject.SetActive(true);
        }
        else if(num == 2)
        {
            fail.gameObject.SetActive(true);
        }
        else if(num==3)
        {
            print("¿€µø");
            clear.gameObject.SetActive(false);
            fail.gameObject.SetActive(false);
        }
    }

}
