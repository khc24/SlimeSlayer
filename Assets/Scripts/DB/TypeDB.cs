using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MngType
{
    None = 0,
    GameMng = 1,
    UIMng = 2,
    SceneMng = 4,
    UnitMng = 8,
    TargetMng = 16,
    AIPathMng = 32,
    ControlMng = 64,
    AudioMng = 128,

}



public enum UnitType
{
    None = 0,
    Player = 6,
    Monster = 7,
    NPC = 8,
    Item,
    Skill
}




// 타켓이 되는지 아닌지
public enum TargetType
{
    None,
    On
}

public enum TableType
{
    None,
    NONETABLE = 1,
    PLAYERTABLE = 6,
    MONSTERTABLE = 7,
    NPCTABLE = 8,
    ITEMTABLE = 9,
    SKILLTABLE = 10,
    QUESTTABLE = 11,
    QUESTTITLETABLE = 12
}

public enum TableBitType
{
    PLAYERTABLE = 1,
    ITEMTABLE = 2,
    ALL = PLAYERTABLE | ITEMTABLE 
}



public enum SceneType
{
    None,
    TitleScene,
    LobbyScene,
    GameScene,
    BossScene

}

public enum UIType
{
    FadeUI,
    LoadingUI,
    UIIngame,
    UIInfoBox,
    UIMenu,
    UIShop,
    UIUpgradeShop,
    UITitle,
    UIGamble,
    UIDialogMng,
    UIQuest,
    UIDungeon
}

public enum AIPathType
{
    AIGrid2DRenderer,
    AIPathFinding2D
}


public enum UnitState
{
    None = 0,
    Idle = 1,
    Move = 2,
    Attack = 4,
    Patrol = 8,
    Chase = 16,
    Skill1 = 32,
    Skill2 = 64
}

public enum ItemCategory
{
    
    WEAPON,
    SHIELD,
    PET,
    PORTION,
    ALL,
    NONE

}

public enum ItemBitCategory
{
    WEAPON = 1,
    SHIELD = 2,
    PET = 4,
    PORTION = 8,
    ALL = WEAPON | SHIELD | PET | PORTION
}

public enum JobBit
{
    WARRIOR = 1,
    WIZARD = 2,
    ALL = WARRIOR | WIZARD
}

public enum Job
{
    NONE = 0,
    WARRIOR,
    WIZARD,
    ALL
}

public enum QuestDialogMode
{
    START,
    PROCESS,
    CLEAR,
    GIVEUP
}

public enum QuestType
{
    NONE = 0,
    SHOP = 1,
    UPGRADE = 2,
    GAMBLE = 4,
    HUNT = 8
}
public enum QuestStateType
{
    TRUE,
    FALSE,
    CLEAR
}

public enum NPCNameType
{
    ShopMan = 1,
    UpgradeMan = 2,
    GambleMan = 3,
    KingMan = 4
}

