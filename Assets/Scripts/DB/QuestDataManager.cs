using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CountValue
{
   public int tableID;
   public int level;
   public  int count;
}

[System.Serializable]
public class SaveQuest
{
    public string NPCNAME;
    public int LEVEL;


    public List<CountValue> shopCount = new List<CountValue>();
    public List<CountValue> upgradeCount = new List<CountValue>();
    public List<CountValue> gambleCount = new List<CountValue>();
    public List<CountValue> huntCount = new List<CountValue>();



    
    public QuestStateType questState = QuestStateType.FALSE;
    public QuestDialogMode questDialogMode = QuestDialogMode.START;
}

[System.Serializable]
public class Quest
{
    private QuestStateType questState = QuestStateType.FALSE;
    public QuestStateType SetQuestState
    {
        set { questState = value;}
    }

    public QuestStateType GetQuestState { get { return questState; } }
    private QuestDialogMode questDialogMode = QuestDialogMode.START;

    public QuestDialogMode GetQuestDialogMode
    {
        get { return questDialogMode; }
    }
    public QuestDialogMode SetQuestDialogMode
    {
        set { questDialogMode = value; }
    }
    private QuestDialogMode tempChangeMode;
    private bool isModeChangeOn = false;
    private int countType = 0;
    private Dictionary<QuestType, Dictionary<int, int[]>> questCount;
    public Dictionary<int ,int[]> GetQuestCount(QuestType questType)
    {
        if(questCount.ContainsKey(questType))
            return questCount[questType];

        return null;
    }
    private bool isReward = false;

    private int massageCount = 0;

    private int rewardGold;
    public int GetRewardGold
    {
        get { return rewardGold; }
    }
    private List<int[]> rewardItem;
    public List<int[]> GetRewardItem
    {
        get { return rewardItem; }
    }

    private List<int[]> rewardCharacter;
    public List<int[]> GetRewardCharacter
    {
        get { return rewardCharacter; }
    }



    public float getCountValue()
    {
        int valueAllCount = 0;
        int valueCount = 0;

        

        foreach(var value in questCount)
        {
            foreach(var subValue in value.Value)
            {
             

                valueAllCount += subValue.Value[subValue.Value.Length - 1];
                valueCount += subValue.Value[subValue.Value.Length - 2];

            }
            
        }



        return (float)valueCount / valueAllCount;
    }


    private string NPCNAME;
    private string NPCKNAME;
    private int LEVEL;
    public int GetLEVEL
    {
        get { return LEVEL; }
    }
    private string TITLE;
    private string QUESTNAME;
    private string CONTENT;
    private List<string> START = new List<string>();
    private List<string> PROCESS = new List<string>();
    private List<string> CLEAR = new List<string>();
    private List<string> GIVEUP  = new List<string>();

    public string GetString(string strName)
    {
        switch(strName)
        {
            case "NPCNAME":
                return NPCNAME;
                break;
            case "NPCKNAME":
                return NPCKNAME;
                break;
            case "TITLE":
                return TITLE;
                break;
            case "QUESTNAME":
                return QUESTNAME;
                break;
            case "CONTENT":
                return CONTENT;
                break;
        }

        return null;
    }

    public void SetInfo(string npcName, string npcKName, int level,
                        Dictionary<QuestType, Dictionary<int, int[]>> questCount,
                        string title, List<string> start, List<string> process, List<string> clear, List<string> giveup,
                        string questName, string content,
                        int rewardGold, List<int[]> rewardItem , List<int[]> rewardCharacter)
    {

        NPCNAME = npcName;
        NPCKNAME = npcKName;
        LEVEL = level;

        this.questCount = questCount;

        if (this.questCount.ContainsKey(QuestType.SHOP))
            countType += (int)QuestType.SHOP;
        if (this.questCount.ContainsKey(QuestType.UPGRADE))
            countType += (int)QuestType.UPGRADE;
        if (this.questCount.ContainsKey(QuestType.GAMBLE))
            countType += (int)QuestType.GAMBLE;
        if (this.questCount.ContainsKey(QuestType.HUNT))
            countType += (int)QuestType.HUNT;

        TITLE = title;
        START = start;
        PROCESS = process;
        CLEAR = clear;
        GIVEUP = giveup;
        QUESTNAME = questName;
        
        CONTENT = content;
        this.rewardGold = rewardGold;
        this.rewardItem = rewardItem;
        this.rewardCharacter = rewardCharacter;
        

    }

    public List<string> GetAutoMassage(bool path = true)
    {
        List<string> strList = new List<string>();
        
        switch(questDialogMode)
        {
            case QuestDialogMode.START:
                {
                    if (massageCount == 0)
                        isModeChangeOn = false;
                    if (START.Count == massageCount)
                    {
                       
                        massageCount = 0;
                        if (isModeChangeOn)
                            questDialogMode = tempChangeMode;
                        return strList;
                    }
                    else if (START.Count - 2 == massageCount)
                    {
                        
                        string[] tempStr = START[massageCount++].Split('@');
                        massageCount++;
                        strList.AddRange(tempStr);

                        if (GameDB.currQuestDic.ContainsKey(NPCNAME))
                        {
                            GameDB.currQuestDic[NPCNAME].Add(LEVEL, this);
                        }
                        else
                        {
                            GameDB.currQuestDic.Add(NPCNAME, new Dictionary<int, Quest>());
                            GameDB.currQuestDic[NPCNAME].Add(LEVEL, this);
                        }
                        isModeChangeOn = true;
                        tempChangeMode = QuestDialogMode.PROCESS;

                        return strList;
                    }
                    else if (START.Count - 1 == massageCount)
                    {
                        
                        string[] tempStr = START[massageCount++].Split('@');
                        strList.AddRange(tempStr);
                        return strList;
                    }
                    else
                    {
                        

                        if (path)
                        {
                            
                            string[] tempStr = START[massageCount++].Split('@');
                            
                            strList.AddRange(tempStr);
                            return strList;
                        }
                        else
                        {
                            
                            string[] tempStr = START[massageCount++].Split('@');
                            
                            massageCount++;
                            
                            strList.AddRange(tempStr);
                            return strList;
                        }
                    }
                }
                break;
            case QuestDialogMode.PROCESS:
                {
                    if (massageCount == 0)
                        isModeChangeOn = false;
                    if (PROCESS.Count == massageCount)
                    {
                        massageCount = 0;
                        return strList;
                    }
                    else if (PROCESS.Count - 2 == massageCount)
                    {
                        massageCount = 0;
                        string[] tempStr = GIVEUP[massageCount++].Split('@');
                        
                        strList.AddRange(tempStr);
                        
                        questDialogMode = QuestDialogMode.GIVEUP;

                        return strList;
                    }
                    else if (PROCESS.Count - 1 == massageCount)
                    {
                        string[] tempStr = PROCESS[massageCount++].Split('@');
                        strList.AddRange(tempStr);
                        return strList;
                    }
                    else
                    {
                        if (path)
                        {
                            string[] tempStr = PROCESS[massageCount++].Split('@');
                            strList.AddRange(tempStr);
                            return strList;
                        }
                        else
                        {
                            string[] tempStr = PROCESS[massageCount++].Split('@');
                            massageCount++;
                            strList.AddRange(tempStr);
                            return strList;
                        }
                    }
                }
                break;
            case QuestDialogMode.CLEAR:
                {
                    if (massageCount == 0)
                        isModeChangeOn = false;

                    if (CLEAR.Count == massageCount)
                    {
                        massageCount = 0;

                        GetReward();

                        return strList;
                    }
                    else
                    {
                        string[] tempStr = CLEAR[massageCount++].Split('@');
                        strList.AddRange(tempStr);
                        return strList;
                    }


                }
                break;
            case QuestDialogMode.GIVEUP:
                {
                    if (massageCount == 0)
                        isModeChangeOn = false;

                    if (GIVEUP.Count == massageCount)
                    {
                        massageCount = 0;

                        if (GameDB.currQuestDic.ContainsKey(NPCNAME))
                            if (GameDB.currQuestDic[NPCNAME].ContainsKey(LEVEL))
                                GameDB.currQuestDic[NPCNAME].Remove(LEVEL);

                        QuestReset();

                        return strList;
                    }
                    else
                    {
                            string[] tempStr = GIVEUP[massageCount++].Split('@');
                            strList.AddRange(tempStr);
                            return strList;   
                    }


                }
                break;
        }

        return strList;
    }

    private void GetReward()
    {
        if (isReward)
            return;
        isReward = true;
        GameDB.money += rewardGold;
         UIMng.Instance.SetMoney(GameDB.money);

        if(rewardItem != null)
        {
            foreach (var value in rewardItem)
            {
                int ID = GameDB.userInfo.uniqueCount;
                GameDB.SetInfo(value[0], value[1], ID++, false, 0);
                GameDB.userInfo.uniqueCount = ID;
            }
        }

        if (rewardCharacter != null)
        {
            foreach (var value in rewardCharacter)
            {
                int ID = GameDB.userInfo.uniqueCount;
                int[] equipCharacter = new int[] { 0, 0, 0 };

                List<SaveSkill> tempSaveSkillList = new List<SaveSkill>();
                string skillStr = DataManager.ToS(TableType.PLAYERTABLE, value[0], "SKILLLIST");
                string[] skillList = skillStr.Split('|');

                for(int skillNum = 0; skillNum < skillList.Length; skillNum++)
                {
                    SaveSkill tempSaveSkill = new SaveSkill();
                    string[] eachSkill = skillList[skillNum].Split('@');
                    int tempInt = 0;
                   if( int.TryParse(eachSkill[0],out tempInt))
                    {
                        tempSaveSkill.tableID = tempInt;

                        if(int.TryParse(eachSkill[1], out tempInt))
                        {
                            tempSaveSkill.level = tempInt;
                            tempSaveSkillList.Add(tempSaveSkill);
                        }

                    }
                    
                }


                GameDB.SetCharInfo(value[0], value[1], ID++, false, equipCharacter, tempSaveSkillList);
                GameDB.userInfo.uniqueCount = ID;
            }
        }

        questState = QuestStateType.CLEAR;

    }

    public void LoaderUpdateCount(QuestType questType, int tableID)
    {
        if (questCount == null || countType == 0)
            return;

        switch (questType)
        {
            case QuestType.SHOP:
                {
                    if (questCount.ContainsKey(QuestType.SHOP))
                    {
                        ++questCount[QuestType.SHOP][tableID][questCount[QuestType.SHOP][tableID].Length - 2];

                        int clearCount = 0;
                        foreach (var subValue in questCount[QuestType.SHOP])
                        {
                            if (subValue.Value[0] == subValue.Value[1])
                                ++clearCount;
                        }
                        if (clearCount == questCount[QuestType.SHOP].Count)
                            countType -= (int)QuestType.SHOP;
                    }
                }
                break;
            case QuestType.UPGRADE:
                {
                    if (questCount.ContainsKey(QuestType.UPGRADE))
                    {
                        ++questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 2];

                        int clearCount = 0;
                        foreach (var subValue in questCount[QuestType.UPGRADE])
                        {
                            if (subValue.Value[0] == subValue.Value[1])
                                ++clearCount;
                        }
                        if (clearCount == questCount[QuestType.UPGRADE].Count)
                            countType -= (int)QuestType.UPGRADE;
                    }
                }
                break;
            case QuestType.GAMBLE:
                {
                    if (questCount.ContainsKey(QuestType.GAMBLE))
                    {
                        ++questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 2];

                        int clearCount = 0;
                        foreach (var subValue in questCount[QuestType.GAMBLE])
                        {
                            if (subValue.Value[0] == subValue.Value[1])
                                ++clearCount;
                        }
                        if (clearCount == questCount[QuestType.GAMBLE].Count)
                            countType -= (int)QuestType.GAMBLE;
                    }
                }
                break;
            case QuestType.HUNT:
                {
                    if (questCount.ContainsKey(QuestType.HUNT))
                    {
                        ++questCount[QuestType.HUNT][tableID][questCount[QuestType.HUNT][tableID].Length - 2];

                        int clearCount = 0;
                        foreach (var subValue in questCount[QuestType.HUNT])
                        {
                            if (subValue.Value[0] == subValue.Value[1])
                                ++clearCount;
                        }
                        if (clearCount == questCount[QuestType.HUNT].Count)
                            countType -= (int)QuestType.HUNT;
                    }
                }
                break;

        }



        
    }

    public void UpdateCount(QuestType questType, int tableID, int level = 1)
    {
        if (questCount == null || countType == 0)
            return;

        switch(questType)
        {
            case QuestType.SHOP:
                {
                    if(questCount.ContainsKey(QuestType.SHOP))
                    {
                        if ((countType & (int)QuestType.SHOP) == (int)QuestType.SHOP)
                        {

                            if(questCount[QuestType.SHOP].ContainsKey(30300))
                            {
                                if (questCount[QuestType.SHOP][30300][questCount[QuestType.SHOP][30300].Length - 1]
                                            != questCount[QuestType.SHOP][30300][questCount[QuestType.SHOP][30300].Length - 2])
                                    ++questCount[QuestType.SHOP][30300][questCount[QuestType.SHOP][30300].Length - 2];


                                int clearCount = 0;
                                foreach (var subValue in questCount[QuestType.SHOP])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.SHOP].Count)
                                    countType -= (int)QuestType.SHOP;
                            }

                            int tableValue = 30000 + (level * 100);

                            if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                            {
                                if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                            != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                    ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                int clearCount = 0;
                                foreach (var subValue in questCount[QuestType.SHOP])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.SHOP].Count)
                                    countType -= (int)QuestType.SHOP;

                            }

                           


                            if(tableID >= 20000 && tableID < 30000)
                            {

                                if (questCount[QuestType.SHOP].ContainsKey(20300))
                                {
                                    if (questCount[QuestType.SHOP][20300][questCount[QuestType.SHOP][20300].Length - 1]
                                                != questCount[QuestType.SHOP][20300][questCount[QuestType.SHOP][20300].Length - 2])
                                        ++questCount[QuestType.SHOP][20300][questCount[QuestType.SHOP][20300].Length - 2];


                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }




                                tableValue = 20000 + (level * 100);

                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }


                                tableValue = tableID + 1000 + (level * 100);

                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }


                                string category = DataManager.ToS(TableType.ITEMTABLE, (tableID - 20000), "CATEGORY");
                                
                                
                                ItemBitCategory tempBitCategory = ItemBitCategory.WEAPON;
                                System.Enum.TryParse<ItemBitCategory>(category,out tempBitCategory);
                                int categoryNum = (int)tempBitCategory;

                                

                                tableValue = 20000 + 2000 + (level * 100) + categoryNum;


                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }


                            }


                            if (tableID >= 10000 && tableID < 20000)
                            {


                                if (questCount[QuestType.SHOP].ContainsKey(10300))
                                {
                                    if (questCount[QuestType.SHOP][10300][questCount[QuestType.SHOP][10300].Length - 1]
                                                != questCount[QuestType.SHOP][10300][questCount[QuestType.SHOP][10300].Length - 2])
                                        ++questCount[QuestType.SHOP][10300][questCount[QuestType.SHOP][10300].Length - 2];


                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }




                                tableValue = 10000 + (level * 100);

                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }


                                tableValue = tableID + 1000 + (level * 100);

                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }


                                string category = DataManager.ToS(TableType.PLAYERTABLE, (tableID - 10000), "JOB");


                                JobBit tempBitJob = JobBit.WARRIOR;
                                System.Enum.TryParse<JobBit>(category, out tempBitJob);
                                int jobNum = (int)tempBitJob;



                                tableValue = 10000 + 2000 + (level * 100) + jobNum;


                                if (questCount[QuestType.SHOP].ContainsKey(tableValue))
                                {
                                    if (questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 1]
                                                != questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2])
                                        ++questCount[QuestType.SHOP][tableValue][questCount[QuestType.SHOP][tableValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.SHOP])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.SHOP].Count)
                                        countType -= (int)QuestType.SHOP;
                                }

                            }

                        }

                    }
                }
                break;

            /*
             3 0  000 000 업그레이드 성공

             2 0 000 000 아이템 강화
             2 0 000 000
             2 0 000 001 아이템 종류
             2 0 001 000 레벨
             2 0 001 001 아이템 레벨
             2 1 000 001 카테고리 
             2 1 001 001 카테고리 레벨
             2 2 000 001 성급
             2 2 001 001 성급 레벨
             1 0 000 000 아이템 강화
             
             1 0 000 000 캐릭터 강화
             1 0 000 001 캐릭터 종류
             1 0 001 000 레벨
             1 0 001 001 캐릭터 레벨
             1 1 000 001 직업 
             1 1 001 001 직업 레벨
             1 2 000 001 성급
             1 2 001 001 성급 레벨

             */
            case QuestType.UPGRADE:
                {
                    if (questCount.ContainsKey(QuestType.UPGRADE))
                    {
                        if ((countType & (int)QuestType.UPGRADE) == (int)QuestType.UPGRADE)
                        {
                            if (questCount[QuestType.UPGRADE].ContainsKey(30000000))
                            {
                                if (questCount[QuestType.UPGRADE][30000000][questCount[QuestType.UPGRADE][30000000].Length - 1]
                                                != questCount[QuestType.UPGRADE][30000000][questCount[QuestType.UPGRADE][30000000].Length - 2])
                                    ++questCount[QuestType.UPGRADE][30000000][questCount[QuestType.UPGRADE][30000000].Length - 2];

                                int clearCount = 0;
                                foreach (var subValue in questCount[QuestType.UPGRADE])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.UPGRADE].Count)
                                    countType -= (int)QuestType.UPGRADE;
                            }


                            if(tableID >= 20000000 && tableID < 30000000)
                            {
                                if (questCount[QuestType.UPGRADE].ContainsKey(20000000))
                                {
                                    if (questCount[QuestType.UPGRADE][20000000][questCount[QuestType.UPGRADE][20000000].Length - 1]
                                                    != questCount[QuestType.UPGRADE][20000000][questCount[QuestType.UPGRADE][20000000].Length - 2])
                                        ++questCount[QuestType.UPGRADE][20000000][questCount[QuestType.UPGRADE][20000000].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }


                                if (questCount[QuestType.UPGRADE].ContainsKey(tableID))
                                {
                                    if (questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }



                                int tempValue = 20000000 + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }

                                 
                                 tempValue = tableID + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }


                                string category = DataManager.ToS(TableType.ITEMTABLE, (tableID - 20000000), "CATEGORY");


                                ItemBitCategory tempBitCategory = ItemBitCategory.WEAPON;
                                System.Enum.TryParse<ItemBitCategory>(category, out tempBitCategory);
                                int categoryNum = (int)tempBitCategory;

                                tempValue = 21000000 + categoryNum;


                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }

                                tempValue = 21000000 + categoryNum + (level * 1000);


                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }





                                int grade = DataManager.ToI(TableType.ITEMTABLE, (tableID - 20000000), "GRADE");


                                tempValue = 22000000 + grade;

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }

                                tempValue = 22000000 + grade + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }





                            }

                            else if (tableID >= 10000000 && tableID < 20000000)
                            {
                                if (questCount[QuestType.UPGRADE].ContainsKey(10000000))
                                {
                                    if (questCount[QuestType.UPGRADE][10000000][questCount[QuestType.UPGRADE][10000000].Length - 1]
                                                    != questCount[QuestType.UPGRADE][10000000][questCount[QuestType.UPGRADE][10000000].Length - 2])
                                        ++questCount[QuestType.UPGRADE][10000000][questCount[QuestType.UPGRADE][10000000].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }


                                if (questCount[QuestType.UPGRADE].ContainsKey(tableID))
                                {
                                    if (questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tableID][questCount[QuestType.UPGRADE][tableID].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }



                                int tempValue = 10000000 + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }


                                tempValue = tableID + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }


                                string job = DataManager.ToS(TableType.PLAYERTABLE, (tableID - 10000000), "JOB");


                                JobBit tempBitJob = JobBit.WARRIOR;
                                System.Enum.TryParse<JobBit>(job, out tempBitJob);
                                int jobNum = (int)tempBitJob;

                                tempValue = 11000000 + jobNum;


                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }

                                tempValue = 11000000 + jobNum + (level * 1000);


                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }





                                int grade = DataManager.ToI(TableType.PLAYERTABLE, (tableID - 10000000), "GRADE");


                                tempValue = 12000000 + grade;

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }

                                tempValue = 12000000 + grade + (level * 1000);

                                if (questCount[QuestType.UPGRADE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 1]
                                                    != questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2])
                                        ++questCount[QuestType.UPGRADE][tempValue][questCount[QuestType.UPGRADE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.UPGRADE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.UPGRADE].Count)
                                        countType -= (int)QuestType.UPGRADE;
                                }





                            }





                        }

                    }
                }
                break;

            /*
            3 0 000 뽑기 성공
            
            2 0 000 아이템 뽑기
            2 0 001 아이템 종류
            2 1 001 카테고리 
            2 2 001 성급
            
            1 0 000 캐릭터 뽑기
            1 0 001 캐릭터 종류
            1 1 001 직업 
            1 2 001 성급
            
            */
            case QuestType.GAMBLE:
                {
                    if (questCount.ContainsKey(QuestType.GAMBLE))
                    {
                        if ((countType & (int)QuestType.GAMBLE) == (int)QuestType.GAMBLE)
                        {
                            if (questCount[QuestType.GAMBLE].ContainsKey(30000))
                            {
                                if (questCount[QuestType.GAMBLE][30000][questCount[QuestType.GAMBLE][30000].Length - 1]
                                                != questCount[QuestType.GAMBLE][30000][questCount[QuestType.GAMBLE][30000].Length - 2])
                                    ++questCount[QuestType.GAMBLE][30000][questCount[QuestType.GAMBLE][30000].Length - 2];

                                int clearCount = 0;
                                foreach (var subValue in questCount[QuestType.GAMBLE])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.GAMBLE].Count)
                                    countType -= (int)QuestType.GAMBLE;
                            }


                            if (tableID >= 20000 && tableID < 30000)
                            {
                                if (questCount[QuestType.GAMBLE].ContainsKey(20000))
                                {
                                    if (questCount[QuestType.GAMBLE][20000][questCount[QuestType.GAMBLE][20000].Length - 1]
                                                    != questCount[QuestType.GAMBLE][20000][questCount[QuestType.GAMBLE][20000].Length - 2])
                                        ++questCount[QuestType.GAMBLE][20000][questCount[QuestType.GAMBLE][20000].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }


                                if (questCount[QuestType.GAMBLE].ContainsKey(tableID))
                                {
                                    if (questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }



                                

                               


                                string category = DataManager.ToS(TableType.ITEMTABLE, (tableID - 20000), "CATEGORY");


                                ItemBitCategory tempBitCategory = ItemBitCategory.WEAPON;
                                System.Enum.TryParse<ItemBitCategory>(category, out tempBitCategory);
                                int categoryNum = (int)tempBitCategory;

                                int tempValue = 21000 + categoryNum;


                                if (questCount[QuestType.GAMBLE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }

                               



                                int grade = DataManager.ToI(TableType.ITEMTABLE, (tableID - 20000), "GRADE");


                                tempValue = 22000 + grade;

                                if (questCount[QuestType.GAMBLE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }



                            }

                            else if (tableID >= 10000 && tableID < 20000)
                            {
                                if (questCount[QuestType.GAMBLE].ContainsKey(10000))
                                {
                                    if (questCount[QuestType.GAMBLE][10000][questCount[QuestType.GAMBLE][10000].Length - 1]
                                                    != questCount[QuestType.GAMBLE][10000][questCount[QuestType.GAMBLE][10000].Length - 2])
                                        ++questCount[QuestType.GAMBLE][10000][questCount[QuestType.GAMBLE][10000].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }


                                if (questCount[QuestType.GAMBLE].ContainsKey(tableID))
                                {
                                    if (questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tableID][questCount[QuestType.GAMBLE][tableID].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }



                              


                                string job = DataManager.ToS(TableType.PLAYERTABLE, (tableID - 10000), "JOB");


                                JobBit tempBitJob = JobBit.WARRIOR;
                                System.Enum.TryParse<JobBit>(job, out tempBitJob);
                                int jobNum = (int)tempBitJob;

                                int tempValue = 11000 + jobNum;


                                if (questCount[QuestType.GAMBLE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }

                                



                                int grade = DataManager.ToI(TableType.PLAYERTABLE, (tableID - 10000), "GRADE");


                                tempValue = 12000 + grade;

                                if (questCount[QuestType.GAMBLE].ContainsKey(tempValue))
                                {
                                    if (questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 1]
                                                    != questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2])
                                        ++questCount[QuestType.GAMBLE][tempValue][questCount[QuestType.GAMBLE][tempValue].Length - 2];

                                    int clearCount = 0;
                                    foreach (var subValue in questCount[QuestType.GAMBLE])
                                    {
                                        if (subValue.Value[0] == subValue.Value[1])
                                            ++clearCount;
                                    }
                                    if (clearCount == questCount[QuestType.GAMBLE].Count)
                                        countType -= (int)QuestType.GAMBLE;
                                }
                            }
                        }

                    }
                }
                break;
            case QuestType.HUNT:
                {
                    if (questCount.ContainsKey(QuestType.HUNT))
                    {
                        if ((countType & (int)QuestType.HUNT) == (int)QuestType.HUNT)
                        {

                            if(questCount[QuestType.HUNT].ContainsKey(0))
                            {

                                if (questCount[QuestType.HUNT][0][0] != questCount[QuestType.HUNT][0][1])
                                    ++questCount[QuestType.HUNT][0][0];

                                int clearCount = 0;
                                foreach (var subValue in questCount[QuestType.HUNT])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.HUNT].Count)
                                    countType -= (int)QuestType.HUNT;

                            }



                            if(questCount[QuestType.HUNT].ContainsKey(tableID))
                            {
                                if (questCount[QuestType.HUNT][tableID][0] != questCount[QuestType.HUNT][tableID][1])
                                    ++questCount[QuestType.HUNT][tableID][0];

                                int clearCount = 0;
                                foreach(var subValue in questCount[QuestType.HUNT])
                                {
                                    if (subValue.Value[0] == subValue.Value[1])
                                        ++clearCount;
                                }
                                if (clearCount == questCount[QuestType.HUNT].Count)
                                    countType -= (int)QuestType.HUNT;

                            }

                        }

                    }
                }
                break;
        }

        if (countType == 0)
        {
            questState = QuestStateType.TRUE;
            questDialogMode = QuestDialogMode.CLEAR;
        }
            
    }
    public void QuestReset()
    {
        questDialogMode = QuestDialogMode.START;

        foreach (var value in questCount)
        {
            
            foreach(var subValue in value.Value)
            {
                subValue.Value[0] = 0;
            }
        }
    }

}

public static class QuestDataManager
{
    

    public static Dictionary<string, Dictionary<int, int>> questLowList =
        new Dictionary<string, Dictionary<int, int>>();


    public static Quest CleatQuest(string name, int level)
    {
        
        if (questLowList.ContainsKey(name))
            if(questLowList[name].ContainsKey(level))
            {
                
                LowBase questData = DataManager.Get(TableType.QUESTTABLE);

                Quest newQuest = new Quest();
                newQuest.SetInfo(questData.ToS(questLowList[name][level], "NPCNAME"), questData.ToS(questLowList[name][level], "NPCKNAME"), questData.ToI(questLowList[name][level], "LEVEL"),
                              SetQuestConunt(questData.ToS(questLowList[name][level], "SHOP"), questData.ToS(questLowList[name][level], "UPGRADE"), questData.ToS(questLowList[name][level], "GAMBLE"), questData.ToS(questLowList[name][level], "HUNT")),
                              questData.ToS(questLowList[name][level], "TITLE"),
                              TextSplit(questData.ToS(questLowList[name][level], "START"), '|'),
                              TextSplit(questData.ToS(questLowList[name][level], "PROCESS"), '|'),
                              TextSplit(questData.ToS(questLowList[name][level], "CLEAR"), '|'),
                              TextSplit(questData.ToS(questLowList[name][level], "GIVEUP"), '|'),
                              questData.ToS(questLowList[name][level], "QUESTNAME"), questData.ToS(questLowList[name][level], "CONTENT"),
                              questData.ToI(questLowList[name][level], "REWARDGOLD"), GetItem(questData.ToS(questLowList[name][level], "REWARDITEM")),
                              GetCharacter(questData.ToS(questLowList[name][level], "REWARDCHARACTER"))
                              );

               
                return newQuest;
            }

   

        return null;
    }


    public static int GetQuestCount(string name)
    {
        if(questLowList.ContainsKey(name))
            return questLowList[name].Count;

        return 0;
    }


    private static Dictionary<QuestType, Dictionary<int, int[]>> 
        SetQuestConunt(string shop, string upgrade, string gamble, string hunt)
    {
        Dictionary<QuestType, Dictionary<int, int[]>> questCount = new Dictionary<QuestType, Dictionary<int, int[]>>();



        if (!string.IsNullOrEmpty(shop))
        {
            questCount.Add(QuestType.SHOP, new Dictionary<int, int[]>());
            string[] titleStr = shop.Split('|');

            for (int i = 0; i < titleStr.Length; i++)
            {
                string[] subStr = titleStr[i].Split('@');
                int keyNum = 0;
                int.TryParse(subStr[0], out keyNum);

                int[] valueNum = new int[subStr.Length];

                for (int j = 0; j < valueNum.Length; j++)
                {
                    valueNum[j] = 0;
                }
                int.TryParse(subStr[subStr.Length - 1], out valueNum[valueNum.Length - 1]);


                questCount[QuestType.SHOP].Add(keyNum, valueNum);
            }

        }







        if (!string.IsNullOrEmpty(upgrade))
        {
            questCount.Add(QuestType.UPGRADE, new Dictionary<int, int[]>());
            string[] titleStr = upgrade.Split('|');

            for (int i = 0; i < titleStr.Length; i++)
            {
                string[] subStr = titleStr[i].Split('@');
                int keyNum = 0;
                int.TryParse(subStr[0], out keyNum);

                int[] valueNum = new int[subStr.Length];

                for (int j = 0; j < valueNum.Length; j++)
                {
                    valueNum[j] = 0;
                }
                int.TryParse(subStr[subStr.Length - 1], out valueNum[valueNum.Length - 1]);


                questCount[QuestType.UPGRADE].Add(keyNum, valueNum);
            }
        }

        if (!string.IsNullOrEmpty(gamble))
        {
            questCount.Add(QuestType.GAMBLE, new Dictionary<int, int[]>());
            string[] titleStr = gamble.Split('|');

            for (int i = 0; i < titleStr.Length; i++)
            {
                string[] subStr = titleStr[i].Split('@');
                int keyNum = 0;
                int.TryParse(subStr[0], out keyNum);

                int[] valueNum = new int[subStr.Length];

                for (int j = 0; j < valueNum.Length; j++)
                {
                    valueNum[j] = 0;
                }
                int.TryParse(subStr[subStr.Length - 1], out valueNum[valueNum.Length - 1]);


                questCount[QuestType.GAMBLE].Add(keyNum, valueNum);

            }
        }

        if (!string.IsNullOrEmpty(hunt))
        {
            questCount.Add(QuestType.HUNT, new Dictionary<int, int[]>());
            string[] titleStr = hunt.Split('|');

            for (int i = 0; i < titleStr.Length; i++)
            {
                string[] subStr = titleStr[i].Split('@');
                int keyNum = 0;
                int.TryParse(subStr[0], out keyNum);

                int[] valueNum = new int[subStr.Length];
                
                for(int j = 0; j < valueNum.Length; j++)
                {
                    valueNum[j] = 0;
                }
                int.TryParse(subStr[subStr.Length-1], out valueNum[valueNum.Length-1]);
                

                questCount[QuestType.HUNT].Add(keyNum, valueNum);
            }

        }

        if (questCount.Count != 0)
            return questCount;

        return null;

    }

    private static List<int[]> GetItem(string rewardItem)
    {
        if (string.IsNullOrEmpty(rewardItem))
            return null;

        List<int[]> itemList = new List<int[]>();
        string[] rewardList = rewardItem.Split('|');

        for (int reNum = 0; reNum < rewardList.Length; reNum++)
        {
            string[] rewardData = rewardList[reNum].Split('@');

            int[] itemValue = { 0, 0 };
            int.TryParse(rewardData[0], out itemValue[0]);
            int.TryParse(rewardData[1], out itemValue[1]);
            itemList.Add(itemValue);
        }

        if (itemList.Count > 0)
            return itemList;

        return null;

    }

    private static List<int[]> GetCharacter(string rewardCahracter)
    {
        if (string.IsNullOrEmpty(rewardCahracter))
            return null;

        List<int[]> characterList = new List<int[]>();
        string[] rewardList = rewardCahracter.Split('|');

        for (int reNum = 0; reNum < rewardList.Length; reNum++)
        {
            string[] rewardData = rewardList[reNum].Split('@');

            int[] itemValue = { 0, 0 };
            int.TryParse(rewardData[0], out itemValue[0]);
            int.TryParse(rewardData[1], out itemValue[1]);
            characterList.Add(itemValue);
        }

        if (characterList.Count > 0)
            return characterList;

        return null;

    }

    private static List<string> TextSplit(string text, char split) 
    {
        string[] str = text.Split(split);
        List<string> list = new List<string>();
        list.AddRange(str);
        return list;
    }


    public static bool LoadLowAll()
    {
        LowBase questData = DataManager.Get(TableType.QUESTTABLE);
        questLowList.Clear();

        for (int i = 1; i <= questData.Table.Count; i++)
        {

            if (!questLowList.ContainsKey(questData.ToS(i, "NPCNAME")))
                questLowList.Add(questData.ToS(i, "NPCNAME"), new Dictionary<int, int>());

            
            questLowList[questData.ToS(i, "NPCNAME")].Add(questData.ToI(i, "LEVEL"), i);

        }

        if (questLowList.Count != 0)
            return true;
        return false;
    }

  



}
