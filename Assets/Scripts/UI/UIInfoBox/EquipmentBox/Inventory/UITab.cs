using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITab : MonoBehaviour
{
    private Transform highlight;
    private Transform focus;
    private Button button;

    [SerializeField]
    private ItemCategory category;

    [SerializeField]
    private ItemBitCategory bitCategory;


    public ItemCategory Category
    {
        get
        {
            return category;
        }
    }

    public ItemBitCategory BitCategory
    {
        get
        {
            return bitCategory;
        }
    }

    public Transform Focus
    {
        get
        {
            return focus;
        }
    }

    public void Init()
    {
        highlight = transform.Find("Highlight");
        focus = transform.Find("Focus");
        button = GetComponent<Button>();
    }

    public void SetHighlightActive(bool pass)
    {
        if (highlight != null)
            highlight.gameObject.SetActive(pass);
    }

    public void SetButtonListener(System.Action<UITab> action)
    {
        if(button != null)
        button.onClick.AddListener(() => { action(this); });
    }
}
