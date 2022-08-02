using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter01_02 : WavePatten
{

//1	1	MiniGolem	-2,0,3	0,180,0
//	2	MiniGolem	2,0,3	0,180,0
//	3	CrabHydra	0,0,7	0,180,0
//2	1	MiniGolem	0,0,-2	0,0,0
//	2	MiniGolem	-6,0,-8.5	0,0,0
//	3	MiniGolem	6,0,8.5	0,180,0
//	4	Golem	6,0,-8.5	0,0,0
//	5	Golem	-6,0,8.5	0,180,0
//3	1	MiniGolem	-3,0,3	0,135,0
//	2	Golem	3,0,3	0,225,0
//	3	Golem	-3,0,-3	0,45,0
//	4	Golem	3,0,-3	0,315,0


    public Chapter01_02(short WaveNum)
    {
        maxWave = 3;
        StageMaxUnit = 3;
        WaveCreate(WaveNum);
    }

    protected override void Wave1()
    {
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(-2, 0, 3), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(2, 0, 3), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("CrabHydra", new Vector3(0, 0, 7), Quaternion.Euler(0, 180, 0)));

    }

    protected override void Wave2()
    {
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(0, 0, -2), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(-6, 0, -8.5f), Quaternion.Euler(0, 0, 0)));
        monsterList.Add(CreateEnemy("StoneMonster", new Vector3(6, 0, 8.5f), Quaternion.Euler(0, 180, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(6, 0, -8.5f), Quaternion.Euler(0, 0, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(-6, 0, 8.5f), Quaternion.Euler(0, 0, 0)));

    }

    protected override void Wave3()
    {
        monsterList.Add(CreateEnemy("MiniGolem", new Vector3(-3, 0, 3), Quaternion.Euler(0, 135, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(3, 0, 3), Quaternion.Euler(0, 225, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(-3, 0, -3), Quaternion.Euler(0, 45, 0)));
        monsterList.Add(CreateEnemy("Golem", new Vector3(3, 0, -3), Quaternion.Euler(0, 315, 0)));
    }
}
