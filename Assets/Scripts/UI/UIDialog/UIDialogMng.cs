using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogMng : BaseUI
{
    private Canvas canvas;
    Dictionary<string, UIDialog> dialogDic = new Dictionary<string, UIDialog>();
    Dictionary<string, UIDialog> tempDic = new Dictionary<string, UIDialog>();
    Transform uiTop;
    Transform uiDown;
    Transform shield;
    Button dialogTouch;
    UIDialog currDialog;
    Animator dialogIn;


    public void OnTouch()
    {
        
        if (currDialog != null)
            currDialog.AutoMassage();
       
    }

    public void OnTouchMode(string name , bool isDeal)
    {
        currDialog = null;
        
        if (dialogDic.ContainsKey(name))
        {
            
            dialogDic[name].OnTouchMode(isDeal);
            SetTopPos(name);
            currDialog = dialogDic[name];
            SetSortOrder(true);
        }
    
           
    }
    public void OffTouchMode()
    {
        if (currDialog != null)
            currDialog.transform.SetParent(uiDown);

        currDialog = null;
        SetSortOrder(false);
        UIMng.Instance.SetQuestList();
    }


    public void SetTopPos(string name)
    {
        foreach(var value in dialogDic)
        {
            if(value.Key == name)
            {
                value.Value.transform.SetParent(uiTop);
            }
            else
            {
                value.Value.transform.SetParent(uiDown);
            }
            
        }
    }

    public UIDialog UIDialogCreate(Transform pivot, string name)
    {
        if (System.Enum.IsDefined(typeof(NPCNameType), name))
        {
            UIDialog dialog = Resources.Load<UIDialog>("Prefab/UI/UIDialog");

            if (dialog == null)
                return null;

            dialog = Instantiate(dialog, pivot.position, Quaternion.identity, uiDown);
            dialog.Init();
            dialog.SetDialog(name,pivot,shield);
            dialogDic.Add(name, dialog);
            return dialog;


        }

        return null;
    }


    #region // 추상 메소드
    public override void Init()
    {
        Transform t = transform.Find("UITop");
        if (t != null) uiTop = t;
        
        t = transform.Find("UIDown");
        if (t != null) uiDown = t;

        t = transform.Find("Shield");
        if (t != null)
        {
            shield = t;
            dialogTouch = t.GetComponent<Button>();
            dialogTouch.onClick.AddListener(OnTouch);
        }

        dialogIn = GetComponentInChildren<Animator>(true);

        canvas = GetComponent<Canvas>();

        SetSortOrder(false);
    }

    public void SetDialogIn(bool path)
    {
        if (dialogIn != null) dialogIn.gameObject.SetActive(path);
    }

    public void SetSortOrder(bool path)
    {
        if (canvas == null)
            return;

        if(path)
        {
            canvas.sortingOrder = 11;
        }
        else
        {
            canvas.sortingOrder = 9;
        }
    }

    public override void Run()
    {
        tempDic.Clear();
        foreach (var value in dialogDic)
        {
            if (value.Value == null)
            {

            }
            else
            {
                tempDic.Add(value.Key, value.Value);
                value.Value.Run();
            }
            
        }

        dialogDic.Clear();

        foreach (var value in tempDic)
        {
            
                dialogDic.Add(value.Key, value.Value);
                value.Value.Run();

        }




    }

    public override void Open()
    {

    }

    public override void Close()
    {
        foreach(var value in dialogDic)
        {
            Destroy(value.Value.gameObject);
        }

        SetSortOrder(false);
    }
    #endregion
}
