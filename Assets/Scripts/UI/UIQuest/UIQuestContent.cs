using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIQuestContent : BaseUI
{
    Quest questInfo;
    UIQuestInfoBtn questInfoBtnt;
   
    TMP_Text questName;
    Slider slider;
    Image barImage;
    Image iconImage;
    Image openImage;
    Image backgroundImage;
    int progresCount = 0;
    int clearCount = 0;

    Color trueColor = new Color(0.3f, 0.8f, 0.1f);
    Color trueBackColor = new Color(0.5f, 0.9f, 0.4f);
    Color falseColor = new Color(0.8f, 0.1f, 0.3f);
    Color falseBackColor = new Color(0.9f, 0.4f, 0.6f);
    Color clearColor = new Color(0.3f, 0.3f, 0.3f);
    Color clearBackColor = new Color(0.6f, 0.6f, 0.6f);

    public void SetPointerDownDelegate(System.Action<Quest> function)
    {
        if (questInfoBtnt != null) questInfoBtnt.SetPointerDownDelegate(function);
    }

    public void SetPointerEnterDelegate(System.Action<Quest> function)
    {
        if(questInfoBtnt != null) questInfoBtnt.SetPointerEnterDelegate(function);
    }

    public void SetPointerExitDelegate(System.Action<Quest> function)
    {
        if (questInfoBtnt != null) questInfoBtnt.SetPointerExitDelegate(function);
    }

    public void SetInfo(Quest info)
    {
        if (info == null)
        {
            questInfo = info;
            if (questInfoBtnt != null) questInfoBtnt.SetInfo(info);

            gameObject.SetActive(false);
            return;
        }

        questInfo = info;
        if (questInfoBtnt != null) questInfoBtnt.SetInfo(info);

        questName.text = info.GetString("QUESTNAME");

        SetUpdate();
    }

    public void SetUpdate()
    {


        slider.value = questInfo.getCountValue();


        if (questInfo.GetQuestState == QuestStateType.TRUE)
                {
                 
                    barImage.color = trueColor;
                    iconImage.color = trueColor;
                    openImage.color = trueColor;
                    backgroundImage.color = trueBackColor;
                 }
                else if (questInfo.GetQuestState == QuestStateType.FALSE)
                {
                    barImage.color = falseColor;
                    iconImage.color = falseColor;
                    openImage.color = falseColor;
                    backgroundImage.color = falseBackColor;
                }
                else if (questInfo.GetQuestState == QuestStateType.CLEAR)
                {
                    barImage.color = clearColor;
                    iconImage.color = clearColor;
                    openImage.color = clearColor;
                    backgroundImage.color = clearBackColor;
                }
            

        

        
    }

    #region //추상 함수 정의부

    public override void Init()
    {
        questInfoBtnt = GetComponentInChildren<UIQuestInfoBtn>(true);
        if (questInfoBtnt != null) questInfoBtnt.Init();
        slider = GetComponentInChildren<Slider>(true);
        if (slider != null)
        {
            slider.value = 0;
            barImage = slider.fillRect.GetComponent<Image>();
        }

        
        backgroundImage = UtilHelper.Find<Image>(transform, "Background");
        iconImage = UtilHelper.Find<Image>(transform, "UIQuestInfoOpen");
        openImage = UtilHelper.Find<Image>(transform, "UIQuestInfoOpen/Icon");


        Transform t = transform.Find("QuestName");

        if (t != null)
        {
            questName = t.GetComponent<TMP_Text>();
            
        }
        

        
       


    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion


}
