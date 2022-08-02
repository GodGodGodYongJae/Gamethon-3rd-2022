using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1_Stage1 : WavePatten
{
    public Chapter1_Stage1(short WaveNum)
    {
        maxWave = 2;
        StageMaxUnit = 2;
        WaveCreate(WaveNum);
    }

    protected override void Wave1()
    {
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(-4.5f,0,-6), Quaternion.Euler(0, 45.9f, 0)));
        //monsterList.Add(CreateEnemy("Golem", new Vector3(6, 0, 6), Quaternion.identity));
        //monsterList.Add(CreateEnemy("CrabHydra", new Vector3(9, 0, 9), Quaternion.identity));
    }

    protected override void Wave2()
    {
        monsterList.Add(CreateEnemy("CrabHydra", new Vector3(3, 0, 3), Quaternion.identity));
      
    }

    protected override void Wave3()
    {
        
    }
}
