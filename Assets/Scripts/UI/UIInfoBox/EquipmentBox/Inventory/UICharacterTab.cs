using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterTab : MonoBehaviour
{
    private Transform highlight;
    private Transform focus;
    private Button button;

    [SerializeField]
    private Job job;

    [SerializeField]
    private JobBit jobBit;


    public Job JobCategory
    {
        get
        {
            return job;
        }
    }

    public JobBit JobBitCategory
    {
        get
        {
            return jobBit;
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

    public void SetButtonListener(System.Action<UICharacterTab> action)
    {
        if(button != null)
        button.onClick.AddListener(() => { action(this); });
    }
}
