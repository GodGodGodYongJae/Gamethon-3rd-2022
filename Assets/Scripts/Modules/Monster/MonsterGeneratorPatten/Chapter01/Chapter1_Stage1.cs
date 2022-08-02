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
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(0,0,2), Quaternion.Euler(0, 180, 0)));

    }

    protected override void Wave2()
    {
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(0, 0, 3), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(3, 0, 5), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(-3, 3, 3), Quaternion.Euler(0, 180, 0)));
    }

    protected override void Wave3()
    {
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(-3, 0, 5), Quaternion.Euler(0, 135, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(3, 0, 5), Quaternion.Euler(0, 225, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(-3, 0, -3), Quaternion.Euler(0, 45, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(3, 0, -3), Quaternion.Euler(0, 315, 0)));
    }
}
