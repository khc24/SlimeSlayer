using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slayer : Player
{
    int dard02Count = 0;

    Skill wind01;
    Skill wind02;
    Skill wind03;


    Vector3 skillLeft = new Vector3(-1.5f, 1.5f, 0);


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
        wind01 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 8, "MODEL"));
        wind02 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 9, "MODEL"));
        wind03 = Resources.Load<Skill>(DataManager.ToS(TableType.SKILLTABLE, 10, "MODEL"));

    }

    public void Shoot()
    {

        Vector3 ShPos = getModel.getTarget.getCenter.position;
        Vector3 ShDir = getModel.getTarget.getTargetPosDir;
        ShDir.Normalize();


        ShPos += (ShDir * 0.5f);

        if (wind02 != null && playerInfo.isSkill2)

        {
            if (playerInfo.skillList.Count > 1)
            {
                if (playerInfo.skillList[1].level > 0)
                {

                    Skill tempWind02_01 = Instantiate(wind02, transform.position + new Vector3(1.1f, -0.2f, 0), Quaternion.identity);
                    Skill tempWind02_02 = Instantiate(wind02, transform.position - new Vector3(1.1f, 0.2f, 0), Quaternion.identity);

                    tempWind02_01.Init();
                    tempWind02_02.Init();


                    tempWind02_02.transform.localScale = skillLeft;




                    tempWind02_01.setInfo(9, weapon.color, playerInfo.skillList[1].level, this.unitType, playerInfo.lastState[0]);
                    tempWind02_02.setInfo(9, weapon.color, playerInfo.skillList[1].level, this.unitType, playerInfo.lastState[0]);

                }

            }


        }


        if (wind03 != null && playerInfo.isSkill3)

        {
            if (playerInfo.skillList.Count > 2)
            {
                if (playerInfo.skillList[2].level > 0)
                {
                    Skill tempWind03 = Instantiate(wind03, ShPos, getModel.getTarget.getCenter.rotation);


                    if (ShDir.x > 0)
                    {
                        tempWind03.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                    }
                    else
                    {
                        tempWind03.transform.localScale = new Vector3(1.5f, -1.5f, 1);
                    }

                    tempWind03.Init();


                    tempWind03.setInfo(10, weapon.color, playerInfo.skillList[2].level, this.unitType, playerInfo.lastState[0]);
                    tempWind03.Shoot();

                    
                }
            }


        }



        ShPos += (ShDir * 0.5f);

        if (wind01 != null && playerInfo.isSkill1)

        {
            if (playerInfo.skillList.Count > 0)
            {
                if (playerInfo.skillList[0].level > 0)
                {
                    Skill tempWind01 = Instantiate(wind01, ShPos, getModel.getTarget.getCenter.rotation);


                    if (ShDir.x > 0)
                    {
                        tempWind01.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        tempWind01.transform.localScale = new Vector3(1, -1, 1);
                    }

                    tempWind01.Init();


                    tempWind01.setInfo(8, weapon.color, playerInfo.skillList[0].level, this.unitType, playerInfo.lastState[0]);
                    tempWind01.Shoot();
                }
            }


        }


    }


}
