using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestInputEventHandler : MonoBehaviour
{

    UIQuestContents questContents;

    UIQuestPopup questPopup;

    private Button exitBack;
    private Button exitButton;

    private Button exitQuestPopupBack;
    private Button exitQuestPopupButton;


    Transform UIEffectShow;

    bool isPopMove = false;



    public void Init()
    {

        questContents = GetComponentInChildren<UIQuestContents>();
        if(questContents != null)
        {

            questContents.SetPointerDownAllDelegate(PointerDown);
            questContents.SetPointerEnterAllDelegate(PointerEnter);
            questContents.SetPointerExitAllDelegate(PointerExit);
        }
        questPopup = GetComponentInChildren<UIQuestPopup>(true);

        exitBack = UtilHelper.FindButton(transform, "UIQuestExit", UIQuestExit);
        exitButton = UtilHelper.FindButton(transform, "UIQuestBox/UIQuestExit", UIQuestExit);

        exitQuestPopupBack = UtilHelper.FindButton(transform, "UIQuestBox/UIQuestPopup/UIQuestPopupExit", UIQuestPopupExit);
        exitQuestPopupButton = UtilHelper.FindButton(transform, "UIQuestBox/UIQuestPopup/Background/UIQuestPopupExit", UIQuestPopupExit);

        UIEffectShow = transform.Find("UIEffectShow");
      
    }

    void UIQuestExit()
    {
       
        AudioMng.Instance.PlayUI("UI_Exit");
        gameObject.SetActive(false);
       UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(true);

        UnitMng.Instance.Resume();

    }

    void UIQuestPopupExit()
    {

        AudioMng.Instance.PlayUI("UI_Exit");
        questPopup.Close();
       

    }


    public void PointerDown(Quest info)
    {
        AudioMng.Instance.PlayUI("UI_Open");
        questPopup.Open(info);
    }

    public void PointerEnter(Quest info)
    {
        
        
    }

    public void PointerExit(Quest info)
    {
        
    }


    public void Run()
    {
        
    }

}

