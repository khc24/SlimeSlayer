using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRPGBackground : MonoBehaviour
{
    public Transform player;
    private Image backImange;

    public Sprite SetBackImage
    {
        set { if(backImange != null)backImange.sprite = value; }
    }

    private RectTransform rectTransform;


    public Vector2 SizeDelta
    {
        get { return rectTransform.sizeDelta; }
    }

    
    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        backImange = GetComponent<Image>();
    }

   

    public void SetTarget(Transform target)
    {
        player = target;
    }


    public void Run()
    {
        if (rectTransform == null || player == null)
            return;
        MinimapHelper.MarkOnTheRPGGame(player, rectTransform);
    }

   
}
