using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;






[System.Serializable]
public class MonsterInfo :ShareInfo
{
    public int uniqueID;
    public int tableID;
    public int LV = 1;
    public string name;
    public int basicAttack;
    public int attack;
    public int basicDefence;
    public int defence;
    public int basicHp;
  
    public float speed;
    public float currSpeed;

    public int weaponID = 4;

}


[RequireComponent(typeof(AIPath2D))]
public class Monster : Unit
{
    public UnitState tempState;
    public List<Weapon> weapons;
    public Weapon weapon;
    public AIPath2D path;
    public UnitState currState;

    private float elapseed = 0;
    private float targetTime = 1f;
    private float subTargetTime = 0f;
    
    // 노드 중앙에 도탁해야 공격 모드로 전환 가능하게 하는 변수
    protected bool arrCheck = false;
    private int pathFalseCount = 0;


    private bool damageState = false;
    private float damageElap = 0;
    private float damageTime = 1;
    private bool isDead = false;

    UIHp uiHp;
  


    [SerializeField]
    protected MonsterInfo monsterInfo = new MonsterInfo();


    public UnitState setCurrState
    {
        set
        {
            currState = value;
        }
    }


    public override void setWeapon(int tableID)
    {
        if (weapons != null)
        {
            foreach (var value in weapons)
            {
                value.Init();



               
            }
        }

    }

    public override void setInfo(int spawnID, int tableID, int LV)
    {
        
        setInfo(spawnID,
                    tableID,
                    LV,
                    DataManager.ToS(TableType.MONSTERTABLE, tableID, "NAME"),
                    DataManager.ToI(TableType.MONSTERTABLE, tableID, "ATTACK"),
                    DataManager.ToI(TableType.MONSTERTABLE, tableID, "DEFENCE"),
                    DataManager.ToI(TableType.MONSTERTABLE, tableID, "HP"),
                    DataManager.ToF(TableType.MONSTERTABLE, tableID, "SPEED")
                    );

        setUpdate();
    }

    public virtual void setInfo(int spawnID,
                                int tableID,
                                int LV,
                                string name,
                                int basicAttack,
                                int basicDefence,
                                int basicHp,
                                float speed
                                )
    {

        monsterInfo.spawnID = spawnID;
        monsterInfo.tableID = tableID;
        monsterInfo.LV = LV;
        monsterInfo.name = name;
        monsterInfo.basicAttack = basicAttack;
        monsterInfo.basicDefence = basicDefence;
        monsterInfo.basicHp = basicHp;
        monsterInfo.speed = speed;
     


        gameObject.layer = (int)unitType;
        gameObject.tag = unitType.ToString();

    }

    public override void setUpdate()
    {
        
        monsterInfo.hp = (int)((monsterInfo.basicHp * 9) + (monsterInfo.LV * monsterInfo.basicHp));
        monsterInfo.lastState[2] = monsterInfo.hp;
        monsterInfo.currHp = monsterInfo.lastState[2];
       
        monsterInfo.attack = (int)((monsterInfo.basicAttack * 9) + (monsterInfo.LV * monsterInfo.basicAttack));
        

        if(GameScene.GetDungeonMode == DungeonMode.EASY)
        {
            monsterInfo.lastState[1] = 0;
            monsterInfo.lastState[0] = monsterInfo.attack;
        }
        else if(GameScene.GetDungeonMode == DungeonMode.NORMAL)
        {
            monsterInfo.lastState[1] = 3;
            monsterInfo.lastState[0] = monsterInfo.attack + 3;
        }
        else if (GameScene.GetDungeonMode == DungeonMode.HARD)
        {
            monsterInfo.lastState[1] = 500;
            monsterInfo.lastState[0] = monsterInfo.attack + 30;
        }
        else if (GameScene.GetDungeonMode == DungeonMode.HELL)
        {
            monsterInfo.lastState[1] =1900;
            monsterInfo.lastState[0] = monsterInfo.attack + 300;
        }

        monsterInfo.currSpeed = 3f * monsterInfo.speed;

   
    }



    public override void Init()
    {

        base.Init();
        unitType = UnitType.Monster;
        currState = UnitState.Idle;
        targetType = TargetType.On;
       

        path = GetComponent<AIPath2D>();
        if (path != null) path.Init();
        weapons.AddRange(GetComponentsInChildren<Weapon>(true));

        
        

        if (weapons.Count >= 1)
        {
            foreach (var value in weapons)
            {
                value.Init();
                if (value.spriteColor != null)
                    weapon = value;
            }
        }

    }



    public override void SetDamage(int attack)
    {
        if (isDead)
            return;

        if(GameDB.player != null)
        {
            
            getModel.getTarget.target =  GameDB.player.getModel.getTarget.getCenter;
            currState = UnitState.Chase;
        }


        int damage = monsterInfo.lastState[1] - attack;
        if (damage < 0)
        {
            
            monsterInfo.currHp += damage;
            if( uiHp == null)
                uiHp = UIMng.Instance.UIHpCreate(HpPivot,monsterInfo);
        }
            
        if (monsterInfo.currHp <= 0)
        {

            isDead = true;
            AudioMng.Instance.PlayUI("GetGold");



            GameDB.getGold += (100 * monsterInfo.LV);
            UIMng.Instance.SetGetGold(GameDB.getGold);
            UIDestoryEffect gold =   Resources.Load<UIDestoryEffect>("Prefab/UI/GetGoldEffect");
            gold.isUpdate = false;
            gold.SegText(100 * monsterInfo.LV);


            Vector3 v = transform.position;
            v += Vector3.up;
                Vector3 viewPos = Camera.main.WorldToScreenPoint(v);
                
            gold = Instantiate(gold, viewPos , Quaternion.identity, UIMng.Instance.Get<UIIngame>(UIType.UIIngame).transform);


            // 퀘스트 업데이트 체크
            GameDB.UpdateCount(QuestType.HUNT, monsterInfo.tableID, 1);
            //

            getModel.getSpriteColor.Execute(Color.black,
                                new Color(0, 0, 0, 0),
                                ColorMode.One,
                                1.0f,
                                true);
            
            Destroy(gameObject, 1.0f);


            currState = UnitState.None;
            if (getModel.getRigid2D != null)
                getModel.getRigid2D.isKinematic = true;

            if (getModel.getColl2D != null)
                getModel.getColl2D.enabled = false;
            
            enabled = false;



        }
        else
        {
            AudioMng.Instance.PlayUI("SetDamage");
            getModel.getAnimator.SetTrigger("Damage");
            getModel.getSpriteColor.Execute(Color.black,
                    Color.white,
                    ColorMode.One,
                    0.2f);

        }
    }


    public override void Pause()
    {
       base.Pause();
        tempState = currState;
        currState = UnitState.None;

    }
    public override void Resume()
    {
        base .Resume();
        currState = tempState;
    }

    public override void setWeapon()
    {

    }
   
    public void currModeCheck()
    {
        if(currState ==UnitState.Idle)
        {
            Idle();
        }
        else if (currState == UnitState.Move)
        {

        }
        else if (currState == UnitState.Attack)
        {
            
            Attack();
        }
        else if (currState == UnitState.Patrol)
        {

        }
        else if (currState == UnitState.Chase)
        {
            Chase();
        }
        else if (currState == UnitState.Skill1)
        {

        }
        else if (currState == UnitState.Skill2)
        {

        }
        
    }

    public override void Run()
    {
        getModel.getRigid2D.velocity = Vector2.zero;
        
        currModeCheck();
    }


    public override float GetHP()
    {
        float hp = (float)monsterInfo.currHp / monsterInfo.hp;

        hp = Mathf.Floor(hp * 100) / 100f;

        return hp;
    }

    #region _추상함수목록_
    public override void Idle()
    {
        getModel.getAnimator.SetBool("Move",false);
        path.path.Clear();
        if (getModel.getTarget.target != null) currState = UnitState.Chase; 
    }
    public override void Move()
    {

    }

    public override void Move(Vector2 dir)

    {

    }

    public override void Attack()
    {

    }

    public override void Attack(Vector2 dir)
    {

    }

    public override void Patrol()
    {

    }

    public override void Chase()
    {
        if(GameDB.player == null)
        {
            currState = UnitState.Idle;
            return;
        }

        if (getModel.IsTag("Damage"))
        {

            return;
        }

        bool isPlayer = true;
       
        if (path.path.Count == 0)
        {
        
            if (getModel.getTarget.target != null)
            {
                charDir(getModel.getTarget.getTargetPosDir);
            }

            
            getModel.getAnimator.SetBool("Move", false);

            elapseed += Time.deltaTime;
            if (elapseed > targetTime && elapseed > subTargetTime)
            {
                AIPathMng.Instance.Find(path, GameDB.player.path);
                elapseed = 0;
                subTargetTime = 0;
                if (path.path.Count == 0)
                    arrCheck = true;
            }
        }
        else
        {
            
            List<AINode2D> tempList = new List<AINode2D>();
            tempList.AddRange(path.path);
            tempList.Reverse();


            
                

            elapseed += Time.deltaTime;
            if(elapseed > 5f)
            {
                if (tempList[0].OnCheckUnit(GameDB.player) == false)
                    isPlayer = false;
                
                elapseed = 0;
            }

            if (path.path[0].pathCheck(this, GameDB.player) || isPlayer == false)
            {
                isPlayer = true;
                


                if (getModel.getTarget.target != null)
                {
                    charDir(getModel.getTarget.getTargetPosDir);
                }

                pathFalseCount++;
                if(pathFalseCount == 10)
                {
                    List<AINode2D> tempPath = new List<AINode2D>();
                    //tempPath.Add(path.path[0]);
                    tempPath = AIPathMng.Instance.tempWallCreate(this , GameDB.player);
                    path.path.Clear();
                    AIPathMng.Instance.Find(path, GameDB.player.path, tempPath);
                    subTargetTime = 2f;
                    pathFalseCount = 0;
                    
                }
                else
                {
                    List<AINode2D> tempPath = new List<AINode2D>();
                    tempPath.Add(path.path[0]);
                    path.path.Clear();
                    AIPathMng.Instance.Find(path, GameDB.player.path, tempPath);
                    subTargetTime = 2f;
                    
                }

                

                

            }
            else
            {
                if(path.path.Count == 1)
                {
                    Vector3 dir = path.path[0].transform.position - transform.position;
                    dir.Normalize();
                    charDir(dir);
                    path.path.Clear();
                    arrCheck = true;
                }
                else
                {
                    Vector3 dir = path.path[0].transform.position - transform.position;
                    dir.Normalize();
                    charDir(dir);

                    getModel.getAnimator.SetBool("Move", true);

                    Vector3 v = Vector3.zero;
                    v.y = -0.45f;
                    v += path.path[0].transform.position;

                    transform.position = Vector3.MoveTowards(transform.position, v,
                                                             monsterInfo.currSpeed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, v) < 0.01f)
                    {
                        arrCheck = true;
                        path.path.RemoveAt(0);

                    }
                }

            
            }
        }



    }

    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }

    #endregion 기능 함수 목록


}
