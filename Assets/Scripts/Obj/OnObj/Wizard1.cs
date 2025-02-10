using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard1 : Player
{

    int dard02Count = 0;

    Skill dark01;
    Skill dark02;
    Skill dark03;

    #region _추상함수목록_
    public override void Idle()
    {

    }
    public override void Move()
    {

    }




    public override void Attack()
    {
        if (getModel.IsTag("Attack"))
        {

            return;
        }

        getModel.Move(Vector2.zero);
        getModel.Attack();
        charDir(getModel.getTarget.getTargetPosDir);
    }


    public override void Patrol()
    {

    }


    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }



    #endregion _추상함수목록_

    public override void Init()
    {
        base.Init();
        dark01 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 5, "MODEL"));
        dark02 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 6, "MODEL"));
        dark03 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 7, "MODEL"));
    }

    public void Shoot()
    {
        
        
        Vector3 ShPos = getModel.getTarget.getCenter.position;
        Vector3 ShDir = getModel.getTarget.getTargetPosDir;
        ShDir.Normalize();


        ShPos += (ShDir * 0.6f);

        
        if(dark01 != null && playerInfo.isSkill1)

        {
            if (playerInfo.skillList.Count > 0)
            {
                if (playerInfo.skillList[0].level > 0)
                {
                    Skill tempDark01 = Instantiate(dark01, ShPos, getModel.getTarget.getCenter.rotation);


                    if (ShDir.x > 0)
                    {
                        tempDark01.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        tempDark01.transform.localScale = new Vector3(1, -1, 1);
                    }

                    tempDark01.Init();


                    tempDark01.setInfo(5, weapon.color, playerInfo.skillList[0].level, this.unitType, playerInfo.lastState[0]);
                    tempDark01.Shoot();
                }
            }

            
        }
        
       

        if(dark02 != null && playerInfo.isSkill2)
        {
                
                
                if (playerInfo.skillList.Count >= 2)
                {
                    if (playerInfo.skillList[1].level > 0)
                    {
                        Skill tempDark02_01 = Instantiate(dark02, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
                        Skill tempDark02_02 = Instantiate(dark02, transform.position + new Vector3(-1.5f, 0, 0), Quaternion.identity);
                        tempDark02_01.Init();
                        tempDark02_02.Init();
                        tempDark02_01.setInfo(6, weapon.color, playerInfo.skillList[1].level, this.unitType, playerInfo.lastState[0]);
                        tempDark02_02.setInfo(6, weapon.color, playerInfo.skillList[1].level, this.unitType, playerInfo.lastState[0]);
                        tempDark02_01.Shoot();
                        tempDark02_02.Shoot();
                    }

                }
            }

            
            if(dark03 != null && playerInfo.isSkill3)
        {
                if (getModel.getTarget.target != null)
                {
                    

                    
                    if (playerInfo.skillList.Count >= 3)
                    {
                        if (playerInfo.skillList[2].level > 0)
                        {
                            Skill tempDark03 = Instantiate(dark03, getModel.getTarget.target.transform.position, Quaternion.identity);

                            tempDark03.Init();
                            tempDark03.setInfo(7, weapon.color, playerInfo.skillList[2].level, this.unitType, playerInfo.lastState[0]);
                            tempDark03.Shoot();
                        }

                    }


                }
            }

    }


}
