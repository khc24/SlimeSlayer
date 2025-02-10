using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIQuestPopup : BaseUI
{

    private Quest currQuest;
    TMP_Text UIquestState;
    TMP_Text questName;
    TMP_Text npcName;
    TMP_Text questExplain;
    TMP_Text questCount;
    TMP_Text questReward;

    UICount countPrefab;
    List<UICount> countList = new List<UICount>();
    ScrollRect countBoxScroll;
    

    UIReward rewardPrefab;
    List<UIReward> rewardList = new List<UIReward>();
    ScrollRect rewardBoxScroll;

    public void Open(Quest info)
    {
        if(info == null)
            return;

        currQuest = info;

        string str = "";

        
        if (info.GetQuestState == QuestStateType.FALSE)
        {
            str = "������";
        }
        else if (info.GetQuestState == QuestStateType.TRUE)
        {
            str = "������";
        }
        else if (info.GetQuestState == QuestStateType.CLEAR)
        {
            str = "�Ϸ�";
        }
        
        UIquestState.text = str;
        questName.text = info.GetString("QUESTNAME");
        npcName.text = info.GetString("NPCKNAME");
        questExplain.text = info.GetString("CONTENT");
        CountCreat();
        RewardCreat();


        gameObject.SetActive(true);

    }



    void CountCreat()
    {
        if (countPrefab == null || currQuest == null)
            return;


        foreach (var value in countList)
        {
            value.Close();
        }
        rewardList.Clear();

        UICount tempCount;

        Dictionary<int, int[]> tempCountDic = new Dictionary<int, int[]>();

        if (currQuest.GetQuestCount(QuestType.SHOP) != null)
        {
            foreach (var value in currQuest.GetQuestCount(QuestType.SHOP))
            {
                

                if (value.Key == 30300)
                {
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(�ŷ�)ĳ����/������" +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                
                else if (value.Key == 30100)
                {
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(����)ĳ����/������" +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }

                else if (value.Key == 30200)
                {
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(�Ǹ�)ĳ����/������" +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000 && value.Key < 30000)
                {
                    if(value.Key == 20300)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�ŷ�)������" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 20100)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(����)������" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 20200)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�Ǹ�)������" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if(value.Key >= 21000 && value.Key < 22000)
                    {
                        if(value.Key >= 21100 && value.Key < 21200)
                        {
                            int tempTableID = value.Key - 21100;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(����)������ " + DataManager.ToS(TableType.ITEMTABLE,tempTableID,"NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if(value.Key >= 21200 && value.Key < 21300)
                        {
                            int tempTableID = value.Key - 21200;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�Ǹ�)������ " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(����)ī�װ� " + bitCategory.ToString() +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 22200 && value.Key < 22300)
                        {
                            int tempCategory = value.Key - 22200;

                            ItemBitCategory bitCategory = (ItemBitCategory)tempCategory;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�Ǹ�)ī�װ� " + bitCategory.ToString() +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�ŷ�)ĳ����" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 10100)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(����)ĳ����" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key == 10200)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�Ǹ�)ĳ����" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key >= 11000 && value.Key < 12000)
                    {
                        if (value.Key >= 11100 && value.Key < 11200)
                        {
                            int tempTableID = value.Key - 11100;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(����)ĳ���� " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);
                        }
                        else if (value.Key >= 11200 && value.Key < 11300)
                        {
                            int tempTableID = value.Key - 11200;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�Ǹ�)ĳ���� " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                           
                            if(bitJob.ToString() == "WARRIOR")
                            {
                                kjob = "����";
                            }
                            else if (bitJob.ToString() == "WIZARD")
                            {
                                kjob = "������";
                            }

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(����)���� " + kjob +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                                kjob = "����";
                            }
                            else if (bitJob.ToString() == "WIZARD")
                            {
                                kjob = "������";
                            }

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�Ǹ�)���� " + kjob +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(��ȭ)ĳ����/������" +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000000 && value.Key < 30000000)
                {
                    if(value.Key == 20000000)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(��ȭ)������" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)������ " + DataManager.ToS(TableType.ITEMTABLE,tempTableID,"NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 20001000 && tempInt == 0)
                        {
                            int tempLV = value.Key - 20000000;
                            tempLV = (int)(tempLV / 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)������ LV." + tempLV +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 20001000)
                        {
                            int tempLV = value.Key - 20000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 20000000 - (tempLV*1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)������ LV." + tempLV + " " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ī�װ� " + (ItemBitCategory)tempTableID +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 21001000)
                        {
                            int tempLV = value.Key - 21000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 21000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ī�װ� LV." + tempLV + " " + (ItemBitCategory)tempTableID +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)������ " + tempTableID + "��" +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 22001000)
                        {
                            int tempLV = value.Key - 22000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 22000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)������ LV." + tempLV + " " + tempTableID + "��" +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(��ȭ)ĳ����" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ĳ����" + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 10001000 && tempInt == 0)
                        {
                            int tempLV = value.Key - 10000000;
                            tempLV = (int)(tempLV / 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ĳ���� LV." + tempLV +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 10001000)
                        {
                            int tempLV = value.Key - 10000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 10000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ĳ���� LV." + tempLV + " " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                                jobStr = "����";
                            }
                            else if (tempTableID == 2)
                            {
                                jobStr = "������";
                            }

                          


                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)���� " + jobStr +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                                jobStr = "����";
                            }
                            else if (tempTableID == 2)
                            {
                                jobStr = "������";
                            }

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)���� LV." + tempLV + " " + jobStr +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ĳ���� " + tempTableID + "��" +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        }
                        else if (value.Key >= 12001000)
                        {
                            int tempLV = value.Key - 12000000;
                            tempLV = (int)(tempLV / 1000);
                            int tempTableID = value.Key - 12000000 - (tempLV * 1000);

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(��ȭ)ĳ���� LV." + tempLV + " " + tempTableID + "��" +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
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
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(�̱�)ĳ����/������ " +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else if (value.Key >= 20000 && value.Key < 30000)
                {
                    if (value.Key == 20000)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�̱�)������ " +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 20000 && value.Key < 21000)
                    {
                        

                            int tempTableID = value.Key - 20000;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�̱�)������ " + DataManager.ToS(TableType.ITEMTABLE, tempTableID, "NAME") +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                    }
                    else if (value.Key >= 21000 && value.Key < 22000)
                    {
                            int tempTableID = value.Key - 21000;
                            

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�̱�)ī�װ� " + (ItemBitCategory)tempTableID +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                        
                    }
                    else if (value.Key >= 22000 && value.Key < 23000)
                    {
                        
                            int tempTableID = value.Key - 22000;

                            tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                            string tempItemStr = "(�̱�)������ " + tempTableID + "��" +
                                                " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                            tempCount.Init();
                            tempCount.SetText(tempItemStr);
                            countList.Add(tempCount);

                    }


                }

                

                else if (value.Key >= 10000 && value.Key < 20000)
                {
                    if (value.Key == 10000)
                    {
                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�̱�)ĳ����" + 
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);
                    }
                    else if (value.Key > 10000 && value.Key < 11000)
                    {


                        int tempTableID = value.Key - 10000;

                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�̱�)ĳ���� " + DataManager.ToS(TableType.PLAYERTABLE, tempTableID, "NAME") +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }
                    else if (value.Key >= 11000 && value.Key < 12000)
                    {
                        int tempTableID = value.Key - 11000;

                        string jobStr = "";
                        if(tempTableID == 1)
                        {
                            jobStr = "����";
                        }
                        else if(tempTableID == 2)
                        {
                            jobStr = "������";
                        }

                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�̱�)���� " + jobStr +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);


                    }
                    else if (value.Key >= 12000 && value.Key < 13000)
                    {

                        int tempTableID = value.Key - 12000;

                        tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                        string tempItemStr = "(�̱�)ĳ���� " + tempTableID + "��" +
                                            " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                        tempCount.Init();
                        tempCount.SetText(tempItemStr);
                        countList.Add(tempCount);

                    }


                }


            




            }
        }
       



        if ( currQuest.GetQuestCount(QuestType.HUNT) != null)
        {
            foreach(var value in currQuest.GetQuestCount(QuestType.HUNT))
            {
                if(value.Key == 0)
                {
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(���) ����" + 
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                else
                {
                    tempCount = Instantiate(countPrefab, countBoxScroll.content.transform);
                    string tempItemStr = "(���)" + DataManager.ToS(TableType.MONSTERTABLE, value.Key, "NAME") +
                                        " " + value.Value[value.Value.Length - 2] + "/" + value.Value[value.Value.Length - 1];
                    tempCount.Init();
                    tempCount.SetText(tempItemStr);
                    countList.Add(tempCount);
                }
                
            }
             
        }
        
    }


    void RewardCreat()
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

            tempReward = Instantiate(rewardPrefab, rewardBoxScroll.content.transform);

            string tempGoldStr = string.Format("{0:#,0}", currQuest.GetRewardGold) + "G";

            tempReward.Init();
            tempReward.SetText(tempGoldStr);

            rewardList.Add(tempReward);

        }

        if (currQuest.GetRewardItem != null && currQuest.GetRewardItem.Count > 0)
        {
            for (int i = 0; i < currQuest.GetRewardItem.Count; ++i)
            {

                tempReward = Instantiate(rewardPrefab, rewardBoxScroll.content.transform);
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

                tempReward = Instantiate(rewardPrefab, rewardBoxScroll.content.transform);
                string tempItemStr = "LV. " + currQuest.GetRewardCharacter[i][1] +
                                    " " + DataManager.ToS(TableType.PLAYERTABLE, currQuest.GetRewardCharacter[i][0], "NAME");
                tempReward.Init();
                tempReward.SetText(tempItemStr);

                rewardList.Add(tempReward);
            }
        }


    }


    #region //�߻� �Լ� ���Ǻ�



    public override void Init()
    {
        UIquestState = UtilHelper.Find<TMP_Text>(transform, "Frontground/QuestState");

        questName = UtilHelper.Find<TMP_Text>(transform,"Frontground/QuestName");
        
        npcName = UtilHelper.Find<TMP_Text>(transform, "Frontground/NPCName");
       
        questExplain = UtilHelper.Find<TMP_Text>(transform, "Frontground/ExplainBack/Explain"); ;
        
        questCount = UtilHelper.Find<TMP_Text>(transform, "Frontground/CountBack/Explain");
      
        questReward = UtilHelper.Find<TMP_Text>(transform, "Frontground/RewardBack/Explain");


    
        countPrefab = Resources.Load<UICount>("Prefab/UI/UICount");
        countBoxScroll = UtilHelper.Find<ScrollRect>(transform, "Frontground/countBox/Scroll View");
        

       
        rewardPrefab = Resources.Load<UIReward>("Prefab/UI/UIReward");
        rewardBoxScroll = UtilHelper.Find<ScrollRect>(transform, "Frontground/rewardBox/Scroll View");
        

       
    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
        UIquestState.text = "";
        questName.text = "";
        npcName.text = "";
        questExplain.text = "";

        foreach (var value in countList)
        {
            value.Close();
        }
        countList.Clear();

        foreach (var value in rewardList)
        {
            value.Close();
        }
        rewardList.Clear();

        gameObject.SetActive(false);
    }

    #endregion
}
