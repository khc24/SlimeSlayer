using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class GameDB 
{

    // 화면 터치하면 타겟에게 다가가가, 퀘스트 읽기
    public static bool targetRead = false;
    public static Unit targetUnit = null;

    public static Player player = null;


    // 게임 활성화/비활성화
    public static bool isGameState = false;

    // 게임 전체 애니메이션 활성화/비활성화
    public static bool isAni = false;

    // 게임 전체 네비게이션 활성화/비활성화
    public static bool isNav = false;

    // 화면상에 적이 있는지 없는지
    public static bool isEnemy = false;

    // 플레이어가 가리키는 타켓의 정보
    public static Transform targeting = null;
    
    // 플레이어의 위치정보
    public static Transform  playerPos = null;

    // mng 이너블 상태를 비트로 계산
    public static int MngEnabled = 0;

    public static SceneType currSceneType = SceneType.None;

    public static int getGold = 0;


    #region // 인벤토리 창 등


    public static UserInfo userInfo = new UserInfo();

    // 아이템 테이블의 전체  리스트
    public static Dictionary<int, ItemInfo> itemDicAll = new Dictionary<int, ItemInfo>();

    // 캐릭터 테이블의 전체 리스트
    public static Dictionary<int, PlayerInfo> charDicAll = new Dictionary<int, PlayerInfo>();


    // 현재 계정이 갖고 있는 아이템 리스트
    public static Dictionary<int, ItemInfo> itemDic = new Dictionary<int, ItemInfo>();

    // 현재 계정이 갖고 있는 아이템 리스트
    public static Dictionary<int, PlayerInfo> charDic = new Dictionary<int, PlayerInfo>();

    // 현재 계정이 갖고 있는 머니
    public static int money = 10000;

    

    // 유니크 넘버를 통해 아이템의 정보를 받아온다(캐릭터가 있다면)
    public static PlayerInfo GetChar(int uniqueID)
    {
        
        if (charDic.ContainsKey(uniqueID))
            return charDic[uniqueID];

        return null;
    }

    public static void charAllUpdate()
    {
        LowBase charLowData = DataManager.Get(TableType.PLAYERTABLE);
        
        if (charLowData == null)
            return;
        charDicAll.Clear();
        for (int i = 1; i <= charLowData.Table.Count; i++)
        {
            SetCharAllInfo(i);
        }
    }

    public static void SetCharInfo(SaveCharacter info)
    {
        SetCharInfo(info.tableID, info.level, info.uniqueID, info.equip , info.equipItemArray, info.skillList, info.isSkill1 ,info.isSkill2,info.isSkill3);
    }

    public static void SetCharAllInfo(int tableID)
    {
        PlayerInfo info = new PlayerInfo();

        info.tableID = tableID;
        info.level = 1;
        info.uniqueID = -1;
        info.equip = false;
       

        System.Enum.TryParse<Job>(DataManager.ToS(TableType.PLAYERTABLE, tableID, "JOB"), out info.job);
        System.Enum.TryParse<JobBit>(DataManager.ToS(TableType.PLAYERTABLE, tableID, "JOB"), out info.jobBit);
        info.jobType = (int)(info.jobBit);
        info.iconCount = DataManager.ToI(TableType.PLAYERTABLE, tableID, "ICON");
        info.sprite = DataManager.items[info.iconCount];
        info.basicAttack = DataManager.ToI(TableType.PLAYERTABLE, tableID, "ATTACK");
        info.basicHp = DataManager.ToI(TableType.PLAYERTABLE, tableID, "HP");
        info.basicDefence = DataManager.ToI(TableType.PLAYERTABLE, tableID, "DEFENCE");
        info.speed = DataManager.ToF(TableType.PLAYERTABLE, tableID, "SPEED");

        info.explain = DataManager.ToS(TableType.PLAYERTABLE, tableID, "EXPLAIN");
        info.name = DataManager.ToS(TableType.PLAYERTABLE, tableID, "NAME");

        info.grade = DataManager.ToI(TableType.PLAYERTABLE, tableID, "GRADE");
        info.model = DataManager.ToS(TableType.PLAYERTABLE, tableID, "MODEL");

        info.price = DataManager.ToI(TableType.PLAYERTABLE, tableID, "PRICE");

        info.IsUpdate = false;
        info.hp = (int)((info.basicHp * 9) + (info.basicHp * info.level));
       
        info.attack = (int)((info.basicAttack * 9) + (info.basicAttack * info.level));
        info.defence = (int)((info.basicDefence * 9) + (info.basicDefence * info.level));
        info.currSpeed = 3f * info.speed;

        
        charDicAll.Add(tableID,info);


        
    }

    public static void SetCharInfo(int tableID, int level, int uniqueID, bool equip, int [] equipItemArray, List<SaveSkill> sList, bool skill1= true, bool skill2 = true,bool skill3 = true)
    {
        PlayerInfo info = new PlayerInfo();

        info.tableID = tableID;
        info.level = level;
        info.uniqueID = uniqueID;
        info.equip = equip;
        info.equipItemArray = equipItemArray;
        info.skillList.Clear();
        info.skillList.AddRange(sList);

        info.isSkill1 = skill1;
        info.isSkill2 = skill2;
        info.isSkill3 = skill3;

        System.Enum.TryParse<Job>(DataManager.ToS(TableType.PLAYERTABLE, tableID, "JOB"), out info.job);
        System.Enum.TryParse<JobBit>(DataManager.ToS(TableType.PLAYERTABLE, tableID, "JOB"), out info.jobBit);
        info.jobType = (int)(info.jobBit);
        info.iconCount = DataManager.ToI(TableType.PLAYERTABLE, tableID, "ICON");
        info.sprite = DataManager.items[info.iconCount];
        info.basicAttack = DataManager.ToI(TableType.PLAYERTABLE, tableID, "ATTACK");
        info.basicHp = DataManager.ToI(TableType.PLAYERTABLE, tableID, "HP");
        info.basicDefence = DataManager.ToI(TableType.PLAYERTABLE, tableID, "DEFENCE");
        info.speed = DataManager.ToF(TableType.PLAYERTABLE, tableID, "SPEED");
        
        info.explain = DataManager.ToS(TableType.PLAYERTABLE, tableID, "EXPLAIN");
        info.name = DataManager.ToS(TableType.PLAYERTABLE, tableID, "NAME");

        info.grade = DataManager.ToI(TableType.PLAYERTABLE, tableID, "GRADE");
        info.model = DataManager.ToS(TableType.PLAYERTABLE, tableID, "MODEL");

        info.price = DataManager.ToI(TableType.PLAYERTABLE, tableID, "PRICE");

        


        if (charDic.ContainsKey(info.uniqueID))
        {
            charDic[info.uniqueID] = info;
            if(info.equip == true)
            {
                SetCharInfoUpdate(info.uniqueID);
                UICharacterState tempState = GameObject.FindObjectOfType<UICharacterState>(true);
                if (tempState != null)
                {

                    tempState.EquipItem(info);
                }

                UIEquipPanel tempPanel = GameObject.FindObjectOfType<UIEquipPanel>(true);
                if (tempPanel != null)
                {

                    tempPanel.TakeOffEquipmentAll();
                    tempPanel.EquipItemAll(info.equipItemArray);
                }
            }
        }
        else
        {
            charDic.Add(info.uniqueID, info);
            if(info.equip == true)
            {
                SetCharInfoUpdate(info.uniqueID);
                UICharacterState tempState = GameObject.FindObjectOfType<UICharacterState>(true);
                if (tempState != null)
                {
                    
                    tempState.EquipItem(info);
                }
                
                UIEquipPanel tempPanel = GameObject.FindObjectOfType<UIEquipPanel>(true);
                if(tempPanel != null)
                {
                    
                    tempPanel.TakeOffEquipmentAll();
                    tempPanel.EquipItemAll(info.equipItemArray);
                }
            }
        }

        
        SetCharInfoUpdate(info.uniqueID);
}

  


    public static void SetCharInfoUpdate(int uniqueID)
    {
       
        

        PlayerInfo player = GetChar(uniqueID);
        if (player == null)
        {
            
            return;
        }
            

        player.IsUpdate = false;

        

        player.hp = (int)((player.basicHp * 9) + (player.basicHp * player.level));

        player.attack = (int)((player.basicAttack * 9) + (player.basicAttack * player.level));
        player.defence = (int)((player.basicDefence * 9) + (player.basicDefence * player.level));
        player.currSpeed = 3f * player.speed;

        
        player.weaponObj =  GetItem(player.equipItemArray[0]);
        player.shieldObj = GetItem(player.equipItemArray[1]);
        player.petObj = GetItem(player.equipItemArray[2]);

        player.lastState[0] = player.attack;
        player.lastState[1] = player.defence;
        player.lastState[2] = player.hp;

        player.itemAttack = 0;
        player.itemDefence = 0;
        player.itemHP = 0;

        if (player.weaponObj != null)
        {
            
            player.itemAttack += player.weaponObj.attack;
            player.itemDefence += player.weaponObj.defence;
            player.itemHP += player.weaponObj.hp;
        }
        if (player.shieldObj != null)
        {
            player.itemAttack += player.shieldObj.attack;
            player.itemDefence += player.shieldObj.defence;
            player.itemHP += player.shieldObj.hp;
        }
        if (player.petObj != null)
        {
            player.itemAttack += player.petObj.attack;
            player.itemDefence += player.petObj.defence;
            player.itemHP += player.petObj.hp;
        }

        player.lastState[0] = player.attack + player.itemAttack;
        player.lastState[1] = player.defence + player.itemDefence;
        player.lastState[2] = player.hp + player.itemHP;
        player.currHp = player.lastState[2];

        player.skillPoint = player.level;

        foreach(var saveSkillValue in player.skillList)
        {
            player.skillPoint -= saveSkillValue.level;
        }

       

}

public static void SetItemInfoUpdate(int uniqueID)
    {
        ItemInfo equItem = GetItem(uniqueID);
        if (equItem == null)
            return;

        equItem.hp = (int)(equItem.basicHp * equItem.level);
        equItem.attack = (int)(equItem.basicAttack * equItem.level);
        equItem.defence = (int)(equItem.basicDefence * equItem.level);
        equItem.color = new Color((float)equItem.R / 255f, (float)equItem.G / 255f, (float)equItem.B / 255f, (float)equItem.A / 255f);


    }



    // 유니크 넘버를 통해 아이템의 정보를 받아온다(아이템이 있다면)
    public static ItemInfo GetItem(int uniqueID)
    {
        if (itemDic.ContainsKey(uniqueID))
            return itemDic[uniqueID];

        return null;
    }

    

    public static void SetInfo(SaveInfo info)
    {
        SetInfo(info.tableID, info.level, info.uniqueID, info.equip, info.equipCharacter);
    }

    public static void itemAllUpdate()
    {
        LowBase itemLowData = DataManager.Get(TableType.ITEMTABLE);
        if (itemLowData == null)
            return;
        itemDicAll.Clear();
        for (int i = 1; i <= itemLowData.Table.Count-2; i++)
        {
            SetItemAllInfo(i);
        }
    }
    public static void SetItemAllInfo(int tableID)
    {
        ItemInfo info = new ItemInfo();

        info.uniqueID = -1;
        info.tableID = tableID;
        info.level = 1;

        System.Enum.TryParse<ItemCategory>(DataManager.ToS(TableType.ITEMTABLE, tableID, "CATEGORY"), out info.category);
        info.iconCount = DataManager.ToI(TableType.ITEMTABLE, tableID, "ICON");
        info.sprite = DataManager.items[info.iconCount];

        info.wearType = 0;
        info.wearFull = DataManager.ToS(TableType.ITEMTABLE, tableID, "WEAR");
        string[] jobTypeArr = info.wearFull.Split('|');
        for (int j = 0; j < jobTypeArr.Length; ++j)
        {
            JobBit jobType;
            System.Enum.TryParse<JobBit>(jobTypeArr[j], out jobType);
            info.wearType += (int)jobType;
        }

        info.R = DataManager.ToI(TableType.ITEMTABLE, tableID, "R");
        info.G = DataManager.ToI(TableType.ITEMTABLE, tableID, "G");
        info.B = DataManager.ToI(TableType.ITEMTABLE, tableID, "B");
        info.A = DataManager.ToI(TableType.ITEMTABLE, tableID, "A");
        info.basicAttack = DataManager.ToI(TableType.ITEMTABLE, tableID, "ATTACK");
        info.basicHp = DataManager.ToI(TableType.ITEMTABLE, tableID, "HP");
        info.basicDefence = DataManager.ToI(TableType.ITEMTABLE, tableID, "DEFENCE");
        info.grade = DataManager.ToI(TableType.ITEMTABLE, tableID, "GRADE");
        info.explain = DataManager.ToS(TableType.ITEMTABLE, tableID, "EXPLAIN");
        info.name = DataManager.ToS(TableType.ITEMTABLE, tableID, "NAME");

        System.Enum.TryParse<ItemBitCategory>(DataManager.ToS(TableType.ITEMTABLE, tableID, "CATEGORY"), out info.bitCategory);
        info.color = new Color((float)info.R / 255, (float)info.G / 255, (float)info.B / 255);
        info.price =  DataManager.ToI(TableType.ITEMTABLE, tableID, "PRICE");

        info.hp = (int)(info.basicHp * info.level);
        info.attack = (int)(info.basicAttack * info.level);
        info.defence = (int)(info.basicDefence * info.level);

       
        info.color = new Color((float)info.R / 255f, (float)info.G / 255f, (float)info.B / 255f, (float)info.A / 255f);

        if (!itemDicAll.ContainsKey(tableID))
        {
            itemDicAll.Add(tableID, info);
        }

    }

    public static void SetInfo(int tableID, int level, int uniqueID, bool equip, int equipCharacter)
    {
        ItemInfo info = new ItemInfo();

        info.tableID = tableID;
        info.level = level;
        info.uniqueID = uniqueID;
        info.equip = equip;
        info.equipCharacter = equipCharacter;

        System.Enum.TryParse<ItemCategory>(DataManager.ToS(TableType.ITEMTABLE, tableID, "CATEGORY"), out info.category);
        info.iconCount = DataManager.ToI(TableType.ITEMTABLE, tableID, "ICON");
        info.sprite = DataManager.items[info.iconCount];

        info.wearType = 0;
        info.wearFull = DataManager.ToS(TableType.ITEMTABLE, tableID, "WEAR");
        string[] jobTypeArr = info.wearFull.Split('|');
        for (int j = 0; j < jobTypeArr.Length; ++j)
        {
            JobBit jobType;
            System.Enum.TryParse<JobBit>(jobTypeArr[j], out jobType);
            info.wearType += (int)jobType;
        }

        info.R = DataManager.ToI(TableType.ITEMTABLE, tableID, "R");
        info.G = DataManager.ToI(TableType.ITEMTABLE, tableID, "G");
        info.B = DataManager.ToI(TableType.ITEMTABLE, tableID, "B");
        info.A = DataManager.ToI(TableType.ITEMTABLE, tableID, "A");
        info.basicAttack = DataManager.ToI(TableType.ITEMTABLE, tableID, "ATTACK");
        info.basicHp = DataManager.ToI(TableType.ITEMTABLE, tableID, "HP");
        info.basicDefence = DataManager.ToI(TableType.ITEMTABLE, tableID, "DEFENCE");
        info.grade = DataManager.ToI(TableType.ITEMTABLE, tableID, "GRADE");
        info.explain = DataManager.ToS(TableType.ITEMTABLE, tableID, "EXPLAIN");
        info.name = DataManager.ToS(TableType.ITEMTABLE, tableID, "NAME");

        System.Enum.TryParse<ItemBitCategory>(DataManager.ToS(TableType.ITEMTABLE, tableID, "CATEGORY"), out info.bitCategory);
        info.color = new Color((float)info.R / 255, (float)info.G / 255, (float)info.B / 255);
        info.price  = DataManager.ToI(TableType.ITEMTABLE, tableID, "PRICE");


        if (!itemDic.ContainsKey(uniqueID))
        {
            itemDic.Add(uniqueID, info);
        }
        else
        {
            itemDic[uniqueID] = info;
        }
        
        
        SetItemInfoUpdate(uniqueID);
    }

    public static List<ItemInfo> randomCreate(int count)
    {
        List<ItemInfo> tempList = new List<ItemInfo>();
        int ID = userInfo.uniqueCount;
        for (int i = 0; i < count; i++)
        {
            int randomCount = Random.Range(1, 27);
            int level = Random.Range(1, 2);
            SetInfo(randomCount, level, ID++, false, 0);
            tempList.Add(GetItem(ID-1));
        }
        
        userInfo.uniqueCount = ID;

        return tempList;

    }

    public static List<PlayerInfo> RandomCharCreate(int count)
    {
        List<PlayerInfo> tempList = new List<PlayerInfo>();
        int ID = userInfo.uniqueCount;
        for (int i = 0; i < count; i++)
        {
            int randomCount = Random.Range(1, 4);
            int level = Random.Range(1, 2);
            int[] equipCharacter = new int[] { 0, 0, 0 };


            List<SaveSkill> tempSaveSkillList = new List<SaveSkill>();
            string skillStr = DataManager.ToS(TableType.PLAYERTABLE, randomCount, "SKILLLIST");
            string[] skillList = skillStr.Split('|');

            for (int skillNum = 0; skillNum < skillList.Length; skillNum++)
            {
                SaveSkill tempSaveSkill = new SaveSkill();
                string[] eachSkill = skillList[skillNum].Split('@');
                int tempInt = 0;
                if (int.TryParse(eachSkill[0], out tempInt))
                {
                    tempSaveSkill.tableID = tempInt;

                    if (int.TryParse(eachSkill[1], out tempInt))
                    {
                        tempSaveSkill.level = tempInt;
                        tempSaveSkillList.Add(tempSaveSkill);
                    }
                }
            }


            SetCharInfo(randomCount, level, ID++, false, equipCharacter , tempSaveSkillList);
            tempList.Add(GetChar(ID-1));
        }

  
        userInfo.uniqueCount = ID;
        return tempList;
    }



    public static List<ItemInfo> GambleRandomCreate(int count)
    {
        List<ItemInfo> tempList = new List<ItemInfo>();

        List<int[]> list = new List<int[]>();

        int[] star1 = { 1, 9, 17 };
        int[] star2 = { 2, 10, 18 };
        int[] star3 = { 3, 11, 19, 26};
        int[] star4 = { 5, 13, 21};
        int[] star5 = { 4, 12,  20};
        int[] star6 = { 6,7,8,14,15,16,22,23,24,25};
      



        


        int ID = userInfo.uniqueCount;
        for (int i = 0; i < count; i++)
        {
            int tempNum = Random.Range(1, 101);
            
            int gambleNum = 1;
            if (tempNum >= 1 && tempNum <= 30)
            {
                tempNum = Random.Range(0, 3);
                gambleNum = star1[tempNum];
            }
            else if (tempNum >= 31 && tempNum <= 55)
            {
                tempNum = Random.Range(0, 3);
                gambleNum = star2[tempNum];
            }
            else if (tempNum >= 56 && tempNum <= 75)
            {
                tempNum = Random.Range(0, 4);
                gambleNum = star3[tempNum];
            }
            else if (tempNum >= 76 && tempNum <= 90)
            {
                tempNum = Random.Range(0, 3);
                gambleNum = star4[tempNum];
            }
            else if (tempNum >= 91 && tempNum <= 99)
            {
                tempNum = Random.Range(0, 3);
                gambleNum = star5[tempNum];
            }
            else if (tempNum == 100)
            {
                tempNum = Random.Range(0, 10);
                gambleNum = star6[tempNum];
            }


           
            SetInfo(gambleNum, 1, ID++, false, 0);
            tempList.Add(GetItem(ID - 1));
        }
        
        userInfo.uniqueCount = ID;

        return tempList;

    }

    public static List<PlayerInfo> GambleRandomCharCreate(int count)
    {
        List<PlayerInfo> tempList = new List<PlayerInfo>();
        int ID = userInfo.uniqueCount;
        for (int i = 0; i < count; i++)
        {

            int tempNum = Random.Range(1, 101);
            int gambleNum = 1;
            if(tempNum >= 1 && tempNum <= 73)
            {
                gambleNum = 1;
            }
            else if (tempNum >= 74 && tempNum <= 99)
            {
                gambleNum = 3;
            }
            else if (tempNum == 100)
            {
                gambleNum = 2;
            }
            
            int[] equipCharacter = new int[] { 0, 0, 0 };


            List<SaveSkill> tempSaveSkillList = new List<SaveSkill>();
            string skillStr = DataManager.ToS(TableType.PLAYERTABLE, gambleNum, "SKILLLIST");
            string[] skillList = skillStr.Split('|');

            for (int skillNum = 0; skillNum < skillList.Length; skillNum++)
            {
                SaveSkill tempSaveSkill = new SaveSkill();
                string[] eachSkill = skillList[skillNum].Split('@');
                int tempInt = 0;
                if (int.TryParse(eachSkill[0], out tempInt))
                {
                    tempSaveSkill.tableID = tempInt;

                    if (int.TryParse(eachSkill[1], out tempInt))
                    {
                        tempSaveSkill.level = tempInt;
                        tempSaveSkillList.Add(tempSaveSkill);
                    }
                }
            }

            SetCharInfo(gambleNum, 1, ID++, false, equipCharacter , tempSaveSkillList);
            tempList.Add(GetChar(ID - 1));
        }

      
        userInfo.uniqueCount = ID;
        return tempList;
    }


    public static List<PlayerInfo> GetCharAllList(int job)
    {

        List<PlayerInfo> charlist = new List<PlayerInfo>();

        foreach (var pair in charDicAll)
        {
            int state = job & (int)pair.Value.jobBit;
            if (state == (int)pair.Value.jobBit)
            {
                charlist.Add(pair.Value);
            }

        }

        charlist.Sort(CharSort2);

        return charlist;
    }

    public static List<PlayerInfo> GetCharList(int job)
    {
        
        List<PlayerInfo> charlist = new List<PlayerInfo>();
        
        foreach (var pair in charDic)
        {
            int state = job & (int)pair.Value.jobBit;
            if (state == (int)pair.Value.jobBit)
            {
                charlist.Add(pair.Value);
            }

        }

        charlist.Sort(CharSort);
        


        return charlist;
    }


    public static List<ItemInfo> GetAllItems(int category)
    {
        
        List<ItemInfo> itemlist = new List<ItemInfo>();
        
        foreach (var pair in itemDicAll)
        {
            int state = category & (int)pair.Value.bitCategory;
            if (state == (int)pair.Value.bitCategory)
            {

                itemlist.Add(pair.Value);
            }

        }


        itemlist.Sort(Sort2);

        return itemlist;
    }
    public static List<ItemInfo> GetItems(int category)
    {
        // 카테고리에 대한 아이템을 찾는다.
        List<ItemInfo> itemlist = new List<ItemInfo>();
        
        foreach (var pair in itemDic)
        {
            int state = category & (int)pair.Value.bitCategory;
            if (state == (int)pair.Value.bitCategory)
            {
                
                itemlist.Add(pair.Value);
            }

        }

        
        itemlist.Sort(Sort);
        
        return itemlist;
    }

    public static string GetPath(string filename)
    {
        string filePath = "";

        if (Application.isMobilePlatform)
        {
            filePath = Application.persistentDataPath;
        }
        else
        {
            filePath = Application.dataPath;
        }

        // 아래의 함수를 사용하면 플랫폼에 따라 경로가 설정되기 때문에 보다 안정성있게 사용할 수 있게 돤다.
        return Path.Combine(filePath, filename);
    }

    public static void WriteFile(string path, string content)
    {

        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

  

    public static string ReadFile(string path)
    {
        if (!File.Exists(path))
            return null;
        FileStream fileStream = new FileStream(path, FileMode.Open);
        string readAllText = string.Empty;
        using (StreamReader reader = new StreamReader(fileStream))
        {
            readAllText = reader.ReadToEnd();
        }

        return readAllText;
    }

    public static void Load(string filename)
    {
        string json = ReadFile(GetPath(filename));
        userInfo = JsonConvert.FromJson<UserInfo>(json);

        money = userInfo.money;
        UIMng.Instance.SetMoney(money);

        foreach (var item in userInfo.listOfItems)
        {

            SetInfo(item);

        }

        foreach (var value in userInfo.listOfChar)
        {

            SetCharInfo(value);

        }
       
        foreach (var value in userInfo.listOfQuests)
        {

            AddQuest(value);

        }

       

    }
    public static void Save(string filename)
    {
     
        userInfo.listOfChar.Clear();
        userInfo.listOfChar.AddRange(charDic.Values);

        userInfo.listOfItems.Clear();
        userInfo.listOfItems.AddRange(itemDic.Values);

        userInfo.listOfQuests.Clear();

        foreach(var value in currQuestDic)
        {
            foreach(var subValue in value.Value)
            {
                SaveQuest newSaveQuest = new SaveQuest();
                newSaveQuest.NPCNAME = subValue.Value.GetString("NPCNAME");
                newSaveQuest.LEVEL = subValue.Value.GetLEVEL;
                newSaveQuest.questState = subValue.Value.GetQuestState;
                newSaveQuest.questDialogMode = subValue.Value.GetQuestDialogMode;


                

                if (subValue.Value.GetQuestCount(QuestType.SHOP) != null)
                {
                    foreach (var countValue in subValue.Value.GetQuestCount(QuestType.SHOP))
                    {
                        CountValue questCountValue = new CountValue();
                        questCountValue.tableID = countValue.Key;
                        questCountValue.count = countValue.Value[countValue.Value.Length - 2];
                        

                        newSaveQuest.shopCount.Add(questCountValue);
                    }
                }

                if (subValue.Value.GetQuestCount(QuestType.UPGRADE) != null)
                {
                    foreach (var countValue in subValue.Value.GetQuestCount(QuestType.UPGRADE))
                    {
                        CountValue questCountValue = new CountValue();
                        questCountValue.tableID = countValue.Key;
                        questCountValue.count = countValue.Value[countValue.Value.Length - 2];
                        

                        newSaveQuest.upgradeCount.Add(questCountValue);
                    }
                }

                if (subValue.Value.GetQuestCount(QuestType.GAMBLE) != null)
                {
                    foreach (var countValue in subValue.Value.GetQuestCount(QuestType.GAMBLE))
                    {
                        CountValue questCountValue = new CountValue();
                        questCountValue.tableID = countValue.Key;
                        questCountValue.count = countValue.Value[countValue.Value.Length - 2];
                        

                        newSaveQuest.gambleCount.Add(questCountValue);
                    }
                }


                if (subValue.Value.GetQuestCount(QuestType.HUNT) != null)
                {
                    foreach (var countValue in subValue.Value.GetQuestCount(QuestType.HUNT))
                    {
                        CountValue questCountValue = new CountValue();
                        questCountValue.tableID = countValue.Key;
                        questCountValue.count = countValue.Value[countValue.Value.Length - 2];
                        

                        newSaveQuest.huntCount.Add(questCountValue);
                    }
                }

                userInfo.listOfQuests.Add(newSaveQuest);
            }
        }

   


        userInfo.money = GameDB.money;

        string json = JsonConvert.ToJson<UserInfo>(userInfo, true);
        WriteFile(GetPath(filename), json);
    }


    public static int CharSort(PlayerInfo left, PlayerInfo right)
    {
       
        if (left.job > right.job)
        {
            return 1;
        }

        else if (left.job < right.job)
        {
            return -1;
        }

        else
        {
           
            int leftVal = left.grade * 10000000 + left.level * 10000 - left.uniqueID;
            int rightVal = right.grade * 10000000 + right.level * 10000 - right.uniqueID;

            if (leftVal > rightVal)
                return -1;
            else if (leftVal < rightVal)
                return 1;

            return 0;
        }

    }

    public static int CharSort2(PlayerInfo left, PlayerInfo right)
    {
        
        if (left.job > right.job)
        {
            return 1;
        }

        else if (left.job < right.job)
        {
            return -1;
        }

        else
        {
            
            int leftVal = left.grade * 10000000 + left.level * 10000;
            int rightVal = right.grade * 10000000 + right.level * 10000;

            
            if (leftVal > rightVal)
                return 1;
            else if (leftVal < rightVal)
                return -1;

            return 0;
        }

    }

    public static int Sort(ItemInfo left, ItemInfo right)
    {
        if (left.bitCategory > right.bitCategory)
        {
            return 1;
        }

        else if (left.bitCategory < right.bitCategory)
        {
            return -1;
        }

        else
        {
        
            int leftVal = left.grade * 10000000 + left.level * 10000 - left.uniqueID;
            int rightVal = right.grade * 10000000 + right.level * 10000 - right.uniqueID;

   
            if (leftVal > rightVal)
                return -1;
            else if (leftVal < rightVal)
                return 1;

            return 0;
        }

    }

    public static int Sort2(ItemInfo left, ItemInfo right)
    {
       
        if (left.bitCategory > right.bitCategory)
        {
            return 1;
        }

        else if (left.bitCategory < right.bitCategory)
        {
            return -1;
        }

        else
        {
           
            int leftVal = left.grade * 10000000 + left.level * 10000;
            int rightVal = right.grade * 10000000 + right.level * 10000;

       
            if (leftVal > rightVal)
                return 1;
            else if (leftVal < rightVal)
                return -1;

            return 0;
        }

    }



    #endregion

    #region //퀘스트

    public static Dictionary<string, Dictionary<int, Quest>> currQuestDic =
        new Dictionary<string, Dictionary<int, Quest>>();


    public static Quest QuestLoader(SaveQuest saveQuest)
    {
        Quest quest = QuestDataManager.CleatQuest(saveQuest.NPCNAME, saveQuest.LEVEL);

        if(quest != null)
        {

            if(saveQuest.shopCount.Count > 0 )
            {
                foreach(var shopItem in saveQuest.shopCount)
                {
                    for (int i = 0; i < shopItem.count; i++)
                    {
                        quest.LoaderUpdateCount(QuestType.SHOP, shopItem.tableID);
                        
                    }
                }
            }

            if (saveQuest.upgradeCount.Count > 0)
            {
                foreach (var upgradeItem in saveQuest.upgradeCount)
                {
                    for (int i = 0; i < upgradeItem.count; i++)
                    {
                        quest.LoaderUpdateCount(QuestType.UPGRADE, upgradeItem.tableID);
                    }
                }
            }

            if (saveQuest.gambleCount.Count > 0)
            {
                foreach (var gambleItem in saveQuest.gambleCount)
                {
                    for (int i = 0; i < gambleItem.count; i++)
                    {
                        quest.LoaderUpdateCount(QuestType.GAMBLE, gambleItem.tableID);
                    }
                }
            }

            if (saveQuest.huntCount.Count > 0)
            {
               
                foreach (var huntItem in saveQuest.huntCount)
                {
                    for (int i = 0; i < huntItem.count; i++)
                    {
                        quest.LoaderUpdateCount(QuestType.HUNT, huntItem.tableID);
                    }
                }
            }


            quest.SetQuestState = saveQuest.questState;
            quest.SetQuestDialogMode = saveQuest.questDialogMode;

           

            return quest;
        }


        return quest;
    }

    public static bool AddQuest(SaveQuest quest)
    {
        if(quest == null)
            return false;

        Quest newQuest =  QuestLoader(quest);

        if (newQuest == null)
            return false;
        
        
        if (currQuestDic.ContainsKey(quest.NPCNAME))
        {
            if (currQuestDic[quest.NPCNAME].ContainsKey(quest.LEVEL ))
            {
                currQuestDic[quest.NPCNAME][quest.LEVEL] = newQuest;
                return true;
            }
            else
            {
                currQuestDic[quest.NPCNAME].Add(quest.LEVEL, newQuest);
                return true;
            }
        }
        else
        {
            currQuestDic.Add(quest.NPCNAME, new Dictionary<int, Quest>());
            currQuestDic[quest.NPCNAME].Add(quest.LEVEL, newQuest);
            return true;
        }

        return false;
    }

    public static Quest FindQuest(string name)
    {
        int questLevelCount = QuestDataManager.GetQuestCount(name);

        if(questLevelCount > 0)
        {
            if (!currQuestDic.ContainsKey(name))
            {
                return QuestDataManager.CleatQuest(name, 1);
            }
            else
            {
                for(int levelNum = 1; levelNum <= questLevelCount; levelNum++)
                {
                    if (currQuestDic[name].ContainsKey(levelNum))
                    {
                        if (currQuestDic[name][levelNum].GetQuestState != QuestStateType.CLEAR)
                            return currQuestDic[name][levelNum];
                    }    
                    else
                    {
                        return QuestDataManager.CleatQuest(name, levelNum);
                    }
                }
            }
        }
        return null;
    }


   public static void UpdateCount(QuestType t, int tableID, int lv)
    {
        foreach(var value in currQuestDic)
        {
            foreach(var subValue in value.Value)
            {
                subValue.Value.UpdateCount(t, tableID, lv);
            }
        }
    }


    #endregion


    #region // 스킬 관리

    public static bool isSkillOn = true;
    private static int skillOrder = 101;
    public static int SkillOrder
    {
        set { skillOrder = value; }
        get { return skillOrder++; }
    }


    #endregion
}
