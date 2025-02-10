using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIDialog : BaseUI
{
    Quest currQuest;

    NPCNameType npcNameType;

    TMP_Text dialogText;
    RectTransform dialogBoxRect;

    public Transform dialogPivot;
    public Transform shield;

    public RectTransform questBox;
    public TMP_Text questBoxNameText;
    public TMP_Text questBoxContentText;
    public ScrollRect questBoxCountScroll;
    public ScrollRect questBoxScroll;

    public RectTransform rewardBox;
    public TMP_Text rewardBoxNameText;
    
    public ScrollRect rewardBoxScroll;


    VerticalLayoutGroup vertical;


    Button dealBtn;
    Button questBtn;

    Button okBtn;
    TMP_Text okBtnText;
    Button noBtn;
    TMP_Text noBtnText;
    

    RectTransform BackImage;

    UIReward rewardPrefab;
    List<UIReward> rewardList = new List<UIReward>();

    UICount countPrefab;
    List<UICount> countList = new List<UICount>();

    bool isOff = false;
    bool isDie = false;


    public void OnTouchMode(bool isDeal)
    {
        AudioMng.Instance.PlayUI("UI_Open");
        isOff = false;

        UIDialogMng dialogMng = UIMng.Instance.Get<UIDialogMng>(UIType.UIDialogMng);
        if (dialogMng != null) dialogMng.SetDialogIn(true);



        if(currQuest != null)
            if(currQuest.GetQuestState == QuestStateType.CLEAR)
            {
                currQuest = GameDB.FindQuest(npcNameType.ToString());

                if (currQuest != null)
                {
                    questBoxNameText.text = currQuest.GetString("QUESTNAME");

                    questBoxContentText.text = currQuest.GetString("CONTENT");

                    rewardBoxNameText.text = currQuest.GetString("QUESTNAME");
                    

                    rewardCreat();
                    CountCreat();

                }

            }
                

        BackImage.transform.SetParent(shield.transform);
        BackImage.anchoredPosition = Vector2.zero;

        // 버튼
        questBtn.gameObject.SetActive(true);
        

        // 쉴드 안 눌리게 하는 백그라운드
        BackImage.gameObject.SetActive(true);

        if(isDeal)
        {
            // 거래 버튼
            dealBtn.gameObject.SetActive(true);
        }
        
    }

    public void AutoMassage(bool path = true)
    {
        if(isOff)
        {
            AudioMng.Instance.PlayUI("UI_Dialog");
            ExitOn(true);
            return;
        }
            
        if (currQuest == null)
        {
            SetMassage(DataManager.ToS(TableType.QUESTTITLETABLE, (int)npcNameType, "NULL")); 
            isOff = true;
            return;
            
        }

        List<string> dialogMassages = currQuest.GetAutoMassage(path);
        
        if(dialogMassages.Count == 0)
        {
            AudioMng.Instance.PlayUI("UI_Exit");
            ExitOn(true);
        }
        else if (dialogMassages.Count == 1)
        {
            AudioMng.Instance.PlayUI("UI_Dialog");
            SetMassage(dialogMassages[0]);
        }
        else if (dialogMassages.Count == 3)
        {
            AudioMng.Instance.PlayUI("UI_Open");
            
            BackImage.transform.SetParent(shield.transform);
            BackImage.anchoredPosition = Vector2.zero;
            questBox.transform.SetParent(shield.transform);
            questBox.anchoredPosition = new Vector2(0, 350);
            // 버튼
            okBtn.gameObject.SetActive(true);
            okBtnText.text = dialogMassages[0];
            noBtn.gameObject.SetActive(true);
            noBtnText.text = dialogMassages[1];

            SetRewardParent(questBoxScroll.content.transform);
            questBox.gameObject.SetActive(true);

            
            BackImage.gameObject.SetActive(true);
        }
        else if (dialogMassages.Count == 2)
        {
            AudioMng.Instance.PlayUI("UI_Dialog");
            rewardBox.transform.SetParent(shield.transform);
            rewardBox.anchoredPosition = new Vector2(0, 350);
            SetRewardParent(rewardBoxScroll.content.transform);
            rewardBox.gameObject.SetActive(true);

        }
        

    }

    public void SetMassage(string text)
    {
        Vector2 start = new Vector2(100, 100);
        

        if (text.Length > 13)
        {
            
            dialogText.alignment = TextAlignmentOptions.Left;
            float tempValue = text.Length / 13;
            int tempInt = (int)tempValue;
            tempInt++;
            start.x = 50 * 13 + 150;
            start.y = 100 * tempInt;
            
        }
        else
        {
            
            dialogText.alignment = TextAlignmentOptions.Center;
            start.x = 50 * text.Length + 150;
            
        }


        dialogBoxRect.sizeDelta = start;
        dialogText.text = text;
        
    }

    void rewardCreat()
    {
       
        if (rewardPrefab == null || currQuest == null)
            return;

        
        foreach (var value in rewardList)
        {
            value.Close();
        }
        rewardList.Clear();

        UIReward tempReward;

        
        if (currQuest.GetRewardGold > 0)
        {
            
            tempReward = Instantiate(rewardPrefab, questBoxScroll.content.transform);

            string tempGoldStr = string.Format("{0:#,0}", currQuest.GetRewardGold) + "G";
            
            tempReward.Init();
            tempReward.SetText(tempGoldStr);

            rewardList.Add(tempReward);
            
        }
       
        if (currQuest.GetRewardItem != null && currQuest.GetRewardItem.Count > 0)
        {
            for(int i =0; i < currQuest.GetRewardItem.Count; ++i)
            {
                
                tempReward = Instantiate(rewardPrefab, questBoxScroll.content.transform);
                string tempItemStr = "LV. " + currQuest.GetRewardItem[i][1] +
                                    " " + DataManager.ToS(TableType.ITEMTABLE, currQuest.GetRewardItem[i][0], "NAME");
                tempReward.Init();
                tempReward.SetText(tempItemStr);

                rewardList.Add(tempReward);
                

            }
        }
        
        if (currQuest.GetRewardCharacter != null && currQuest.GetRewardCharacter.Count > 0)
        {
            
            for (int i = 0; i < currQuest.GetRewardCharacter.Count; ++i)
            {

                tempReward = Instantiate(rewardPrefab, questBoxScroll.content.transform);
                string tempItemStr = "LV. " + currQuest.GetRewardCharacter[i][1] +
                                    " " + DataManager.ToS(TableType.PLAYERTABLE, currQuest.GetRewardCharacter[i][0], "NAME");
                tempReward.Init();
                tempReward.SetText(tempItemStr);

                rewardList.Add(tempReward);


            }
        }
    }

    public void SetRewardParent(Transform parent)
    {
        
        for (int i = 0; i < rewardList.Count; i++)
        {
            rewardList[i].transform.SetParent(parent);
        }
    }

    public void SetDialog(string name, Transform targetPivot, Transform shieldPivot)
    {
        System.Enum.TryParse<NPCNameType>(name,out npcNameType);
        dialogPivot = targetPivot;
        shield = shieldPivot;

        if (rewardPrefab == null)
            rewardPrefab = Resources.Load<UIReward>("Prefab/UI/UIReward");

        if (countPrefab == null)
            countPrefab = Resources.Load<UICount>("Prefab/UI/UICount");

        currQuest = GameDB.FindQuest(name);
        

        if (currQuest != null)
        {
            questBoxNameText.text = currQuest.GetString("QUESTNAME");
            
            questBoxContentText.text = currQuest.GetString("CONTENT");

            rewardBoxNameText.text = currQuest.GetString("QUESTNAME");
            

            rewardCreat();
            CountCreat();

        }
        

        SetMassage(DataManager.ToS(TableType.QUESTTITLETABLE, (int)npcNameType, "TITLE"));

    }



    public void targetChase(Vector3 tarPos)
    {
        if (Camera.main != null)
        {
            Vector3 viewPos = Camera.main.WorldToScreenPoint(tarPos);
            transform.position = viewPos;
        }

    }


    public void DealOnClick()
    {
        AudioMng.Instance.PlayUI("Open");

        if (dialogPivot != null)
        {
            Unit tempUnit = dialogPivot.GetComponentInParent<Unit>(true);
            if (tempUnit != null) tempUnit.OnDealOpen();
        }

        ExitOn(true);
        
    }

    public void QuestOnClick()
    {

        AudioMng.Instance.PlayUI("UI_Dialog");

        

        ExitOn(false);
        AutoMassage();



    }
    public void OkOnClick()
    {

        ExitOn(false);
        AutoMassage(true);
        AutoMassage(true);

    }


    public void ExitOn(bool isTouchOff = false)
    {
        BackImage.transform.SetParent(transform);
        questBox.transform.SetParent(transform);
        rewardBox.transform.SetParent(transform);

        dealBtn.gameObject.SetActive(false);
        questBtn.gameObject.SetActive(false);

        okBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);

        questBox.gameObject.SetActive(false);
        rewardBox.gameObject.SetActive(false);
        if(isTouchOff)
        {
            UIDialogMng dialogMng = UIMng.Instance.Get<UIDialogMng>(UIType.UIDialogMng);
            if (dialogMng != null) dialogMng.SetDialogIn(false);

            SetMassage(DataManager.ToS(TableType.QUESTTITLETABLE, (int)npcNameType, "TITLE"));
            UIMng.Instance.OffTouchMode();
        }
        
    }


    public void NoOnClick()
    {
        ExitOn(false);
        AutoMassage(false);
        AutoMassage(true);

    }

   

    #region // 추상 메소드
    public override void Init()
    {
        Transform t = transform.Find("UIDialogBox");
        if (t != null) dialogBoxRect = t.GetComponent<RectTransform>();
        dialogText = GetComponentInChildren<TMP_Text>(true);

        t = transform.Find("buttonBackground");
        if (t != null)
        {
            BackImage = t.GetComponent<RectTransform>();
            BackImage.gameObject.SetActive(false);
        }

        t = transform.Find("buttonBackground/VerticalLayout");
        if (t != null)
        {
            vertical = t.GetComponent<VerticalLayoutGroup>();

            dealBtn = UtilHelper.FindButton(vertical.transform, "DealBtn", DealOnClick);
            dealBtn.gameObject.SetActive(false);

            questBtn = UtilHelper.FindButton(vertical.transform, "QuestBtn", QuestOnClick);
            dealBtn.gameObject.SetActive(false);

            okBtn = UtilHelper.FindButton(vertical.transform, "OkBtn", OkOnClick);
            okBtnText = UtilHelper.Find<TMP_Text>(okBtn.transform, "Text");
            okBtn.gameObject.SetActive(false);
            

            noBtn = UtilHelper.FindButton(vertical.transform, "NoBtn", NoOnClick);
            noBtnText = UtilHelper.Find<TMP_Text>(noBtn.transform, "Text");
            noBtn.gameObject.SetActive(false);


        }

        t = transform.Find("QuestBox");
        if (t != null)
        {
            questBox = t.GetComponent<RectTransform>();
            questBoxNameText = UtilHelper.Find<TMP_Text>(t, "QUESTNAME");
            questBoxContentText = UtilHelper.Find<TMP_Text>(t, "contentBox/CONTENT");

            questBoxCountScroll = UtilHelper.Find<ScrollRect>(t, "countBox/Scroll View");
            questBoxScroll = UtilHelper.Find<ScrollRect>(t, "rewardBox/Scroll View");
            questBox.gameObject.SetActive(false);
        }

        t = transform.Find("RewardBox");
        if (t != null)
        {
            rewardBox = t.GetComponent<RectTransform>();
            rewardBoxNameText = UtilHelper.Find<TMP_Text>(t, "QUESTNAME");
           
            rewardBoxScroll = t.GetComponentInChildren<ScrollRect>(true);
            rewardBox.gameObject.SetActive(false);
        }

    }

    public override void Run()
    {
        if (isDie)
        {

            return;
        }


        if (dialogPivot != null)
        {
            targetChase(dialogPivot.position);
        }
        else
        {

            Close();
        }
    }

    public override void Open()
    {

    }

    public override void Close()
    {

        isDie = true;
        Destroy(this.gameObject);
    }
    #endregion



    void CountCreat()
    {
        if (countPrefab == null || currQuest == null)
            return;


        foreach (var value in countList)
        {
            value.Close();
        }
        countList.Clear();

        UICount tempCount;

        Dictionary<int, int[]> tempCountDic = new Dictionary<int, int[]>();

        if (currQuest.GetQuestCount(QuestType.SHOP) != null)
        {
            foreach (var value in currQuest.GetQuestCount(QuestType.SHOP))
            {


                if (value.Key == 30300)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(거래)캐릭터/아이템" +
                                        " " + value.Value[value.Value.Length - 1] + "개";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }

                else if (value.Key == 30100)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(구매)캐릭터/아이템" +
                                        " " + value.Value[value.Value.Length - 1] + "개";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }

                else if (value.Key == 30200)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(판매)캐릭터/아이템" +
                                        " " + value.Value[value.Value.Length - 1] + "개";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000 && value.Key < 30000)
                {
                    if (value.Key == 20300)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(거래)아이템" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 20100)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(구매)아이템" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 20200)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(판매)아이템" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key >= 21000 && value.Key < 22000)
                    {
                        if (value.Key >= 21100 && value.Key < 21200)
                        {
                            int tempTableID = value.Key - 21100;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(구매)아이템 " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 21200 && value.Key < 21300)
                        {
                            int tempTableID = value.Key - 21200;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(판매)아이템 " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }

                    }
                    else if (value.Key >= 22000 && value.Key < 23000)
                    {
                        if (value.Key >= 22100 && value.Key < 22200)
                        {
                            int tempCategory = value.Key - 22100;

                            ItemBitCategory bitCategory = (ItemBitCategory)tempCategory;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(구매)카테고리 " + bitCategory.ToString() +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 22200 && value.Key < 22300)
                        {
                            int tempCategory = value.Key - 22200;

                            ItemBitCategory bitCategory = (ItemBitCategory)tempCategory;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(판매)카테고리 " + bitCategory.ToString() +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                    }
                }

                else if (value.Key >= 10000 && value.Key < 20000)
                {
                    if (value.Key == 10300)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(거래)캐릭터" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 10100)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(구매)캐릭터" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 10200)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(판매)캐릭터" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key >= 11000 && value.Key < 12000)
                    {
                        if (value.Key >= 11100 && value.Key < 11200)
                        {
                            int tempTableID = value.Key - 11100;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(구매)캐릭터 " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 11200 && value.Key < 11300)
                        {
                            int tempTableID = value.Key - 11200;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(판매)캐릭터 " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }

                    }
                    else if (value.Key >= 12000 && value.Key < 13000)
                    {
                        if (value.Key >= 12100 && value.Key < 12200)
                        {
                            int tempJob = value.Key - 12100;

                            JobBit bitJob = (JobBit)tempJob;

                            string kjob = "";

                            if (bitJob.ToString() == "WARRIOR")
                            {
                                kjob = "전사";
                            }
                            else if (bitJob.ToString() == "WIZARD")
                            {
                                kjob = "마법사";
                            }

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(구매)직업 " + kjob +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 12200 && value.Key < 12300)
                        {
                            int tempJob = value.Key - 12200;

                            JobBit bitJob = (JobBit)tempJob;

                            string kjob = "";

                            

                            if (bitJob.ToString() == "WARRIOR")
                            {
                                kjob = "전사";
                            }
                            else if (bitJob.ToString() == "WIZARD")
                            {
                                kjob = "마법사";
                            }

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(판매)직업 " + kjob +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                    }


                }








            }

        }


        if (currQuest.GetQuestCount(QuestType.UPGRADE) != null)
        {
            foreach (var value in currQuest.GetQuestCount(QuestType.UPGRADE))
            {
                if (value.Key == 30000000)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(강화)캐릭터/아이템" +
                                        " " + value.Value[value.Value.Length - 1] + "번";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000000 && value.Key < 30000000)
                {
                    if (value.Key == 20000000)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(강화)아이템" +
                                            " " + value.Value[value.Value.Length - 1] + "번";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 20000000 && value.Key < 21000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 20000000 && value.Key < 20001000)
                        {
                            int tempTableID = value.Key - 20000000;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)아이템 " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 20001000 && tempInt == 0)
                        {
                            int tempLV = value.Key - 20000000;
                            tempLV = (int)(tempLV / 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)아이템 LV." + tempLV +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 20001000)
                        {
                            int tempLV = value.Key - 20000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 20000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)아이템 LV." + tempLV + " " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }

                    }
                    else if (value.Key > 21000000 && value.Key < 22000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 21000000 && value.Key < 21001000)
                        {
                            int tempTableID = value.Key - 21000000;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)카테고리 " + (ItemBitCategory)tempTableID +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 21001000)
                        {
                            int tempLV = value.Key - 21000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 21000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)카테고리 LV." + tempLV + " " + (ItemBitCategory)tempTableID +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }


                    }
                    else if (value.Key > 22000000 && value.Key < 23000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 22000000 && value.Key < 22001000)
                        {
                            int tempTableID = value.Key - 22000000;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)아이템 " + tempTableID + "성" +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 22001000)
                        {
                            int tempLV = value.Key - 22000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 22000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)아이템 LV." + tempLV + " " + tempTableID + "성" +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }


                    }


                }

                //

                else if (value.Key >= 10000000 && value.Key < 20000000)
                {
                    if (value.Key == 10000000)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(강화)캐릭터" +
                                            " " + value.Value[value.Value.Length - 1] + "번";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 10000000 && value.Key < 11000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 10000000 && value.Key < 10001000)
                        {
                            int tempTableID = value.Key - 10000000;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)캐릭터" + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 10001000 && tempInt == 0)
                        {
                            int tempLV = value.Key - 10000000;
                            tempLV = (int)(tempLV / 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)캐릭터 LV." + tempLV +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 10001000)
                        {
                            int tempLV = value.Key - 10000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 10000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)캐릭터 LV." + tempLV + " " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }

                    }
                    else if (value.Key > 11000000 && value.Key < 12000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 11000000 && value.Key < 11001000)
                        {
                            int tempTableID = value.Key - 11000000;

                            string jobStr = "";
                            if (tempTableID == 1)
                            {
                                jobStr = "전사";
                            }
                            else if (tempTableID == 2)
                            {
                                jobStr = "마법사";
                            }




                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)직업 " + jobStr +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 11001000)
                        {



                            int tempLV = value.Key - 11000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 11000000 - (tempLV * 1000);

                            string jobStr = "";
                            if (tempTableID == 1)
                            {
                                jobStr = "전사";
                            }
                            else if (tempTableID == 2)
                            {
                                jobStr = "마법사";
                            }

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)직업 LV." + tempLV + " " + jobStr +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }


                    }
                    else if (value.Key > 12000000 && value.Key < 13000000)
                    {
                        int tempInt = (int)(value.Key / 1000);
                        tempInt = value.Key - (tempInt * 1000);

                        if (value.Key > 12000000 && value.Key < 12001000)
                        {
                            int tempTableID = value.Key - 12000000;

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)캐릭터 " + tempTableID + "성" +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 12001000)
                        {
                            int tempLV = value.Key - 12000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 12000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                            string tempItemStr = "(강화)캐릭터 LV." + tempLV + " " + tempTableID + "성" +
                                                " " + value.Value[value.Value.Length - 1] + "개";
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }


                    }


                }


               




            }
        }


     
        if (currQuest.GetQuestCount(QuestType.GAMBLE) != null)
        {
            foreach (var value in currQuest.GetQuestCount(QuestType.GAMBLE))
            {
                if (value.Key == 30000)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(뽑기)캐릭터/아이템 " +
                                        " " + value.Value[value.Value.Length - 1] + "번";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000 && value.Key < 30000)
                {
                    if (value.Key == 20000)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)아이템 " +
                                            " " + value.Value[value.Value.Length - 1] + "번";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 20000 && value.Key < 21000)
                    {


                        int tempTableID = value.Key - 20000;

                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)아이템 " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }
                    else if (value.Key >= 21000 && value.Key < 22000)
                    {
                        int tempTableID = value.Key - 21000;


                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)카테고리 " + (ItemBitCategory)tempTableID +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);


                    }
                    else if (value.Key >= 22000 && value.Key < 23000)
                    {

                        int tempTableID = value.Key - 22000;

                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)아이템 " + tempTableID + "성" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }


                }

                //

                else if (value.Key >= 10000 && value.Key < 20000)
                {
                    if (value.Key == 10000)
                    {
                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)캐릭터" +
                                            " " + value.Value[value.Value.Length - 1] + "번";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 10000 && value.Key < 11000)
                    {


                        int tempTableID = value.Key - 10000;

                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)캐릭터 " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }
                    else if (value.Key >= 11000 && value.Key < 12000)
                    {
                        int tempTableID = value.Key - 11000;

                        string jobStr = "";
                        if (tempTableID == 1)
                        {
                            jobStr = "전사";
                        }
                        else if (tempTableID == 2)
                        {
                            jobStr = "마법사";
                        }

                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)직업 " + jobStr +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);


                    }
                    else if (value.Key >= 12000 && value.Key < 13000)
                    {

                        int tempTableID = value.Key - 12000;

                        tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                        string tempItemStr = "(뽑기)캐릭터 " + tempTableID + "성" +
                                            " " + value.Value[value.Value.Length - 1] + "개";
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }


                }


            



            }
        }
        



        if (currQuest.GetQuestCount(QuestType.HUNT) != null)
        {
            foreach (var value in currQuest.GetQuestCount(QuestType.HUNT))
            {
                if (value.Key == 0)
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(사냥) 몬스터" +
                                        " " + value.Value[value.Value.Length - 1] + "개";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else
                {
                    tempCount = Instantiate(countPrefab, questBoxCountScroll.content.transform);
                    string tempItemStr = "(사냥)" + DataManager.ToS(TableType.MONSTERTABLE, value.Key, "NAME") +
                                        " " + value.Value[value.Value.Length - 1] + "개";
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }

            }

        }

    }


}