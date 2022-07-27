using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WavePatten
{


    public sbyte maxWave;
    protected Tuple<string, Vector3, Quaternion> monsterInfo;
    public List<Tuple<string, Vector3, Quaternion>> monsterList = new List<Tuple<string, Vector3, Quaternion>>();

    public virtual void CreateUnit()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            EnemyFactoryMethod.Instance.CreateEnemy(
                monsterList[i].Item1,
                monsterList[i].Item2,
                monsterList[i].Item3);
        }
    }

    protected virtual Tuple<string, Vector3, Quaternion> CreateEnemy(string name, Vector3 pos, Quaternion quaternion)
    {
        monsterInfo = new Tuple<string, Vector3, Quaternion>(name, pos, quaternion);
        return monsterInfo;
    }

    protected virtual void WaveCreate(short WaveNum)
    {
        switch (WaveNum)
        {
            case 1:
                Wave1();
                break;
            case 2:
                Wave2();
                break;
            case 3:
                Wave3();
                break;
            default:
                break;
        }
    }

    protected abstract void Wave1();
    protected abstract void Wave2();
    protected abstract void Wave3();
}
