using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter01_02 : WavePatten
{
    public Chapter01_02(short WaveNum)
    {
        maxWave = 2;
        WaveCreate(WaveNum);
    }

    protected override void Wave1()
    {
        monsterList.Add(CreateEnemy("Golem", new Vector3(3, 0, 3), Quaternion.identity));
     
    }

    protected override void Wave2()
    {
        monsterList.Add(CreateEnemy("CrabHydra", new Vector3(3, 0, 3), Quaternion.identity));
       
    }

    protected override void Wave3()
    {

    }
}
