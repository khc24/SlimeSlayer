using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCharBtn : MonoBehaviour
{
    Button btn;
    UICharacterInventory inventory;
    public bool path = false;
   
    void Start()
    {
        btn = GetComponent<Button>();
        inventory = GameObject.FindObjectOfType<UICharacterInventory>();
        btn.onClick.AddListener(() => InvenClose(path));
        
    }

    public void InvenClose(bool path)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        inventory.SetActive(path);
    }
    
}
