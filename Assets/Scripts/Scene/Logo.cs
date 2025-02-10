using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    private void Awake()
    {
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //SetResolution();
    }
    void Start()
    {

       
        GameMng.Instance.get();
        
    }

    public void SetResolution()
    {
        int setWidth = 1080;
        int setHeight = 1920;

        Screen.SetResolution(setWidth,setHeight, true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

