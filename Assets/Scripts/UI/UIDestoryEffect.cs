using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDestoryEffect : MonoBehaviour
{
   
    TMP_Text getGoldText;
    public bool isUpdate = true;

    void Start()
    {
        
        Destroy(gameObject,0.5f);
    }

    public void SegText(int gold)
    {
        
        getGoldText = GetComponentInChildren<TMP_Text>(true);
        if(getGoldText != null) getGoldText.text = string.Format("{0:#,0}", gold);
    }

 
    void Update()
    {
        if(isUpdate)
        transform.position += Vector3.up * Time.deltaTime*100f;
    }
}
