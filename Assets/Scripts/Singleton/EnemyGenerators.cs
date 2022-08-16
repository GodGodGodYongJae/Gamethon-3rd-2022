using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerators : Singleton<EnemyGenerators>
{
    //[Serializable]
    public class monsterData
    {
        public List<Tuple<string, Vector3, Vector3>> monData = new List<Tuple<string, Vector3, Vector3>>();
        public int Gold;
        public int Exp;
    }


    #region oldcode 
    //[Serializable]
    //public class Mondata2
    //{
    //    [SerializeReference] public List<string> name;
    //    [SerializeReference] public List<Vector3> pos;
    //    [SerializeReference] public List<Vector3> rot;
    //}

    //[System.Serializable]
    //public class Chapter : SerializableDictionary<int, Stage> { };
    //public Chapter chapter = new Chapter();

    //[System.Serializable]
    //public class Stage : SerializableDictionary<int,wave> { };


    //[System.Serializable]
    //public class wave : SerializableDictionary<int, Mondata2> { };
    #endregion
    public MultiKeyDictionary<int,int, int, monsterData> StageInfo = new MultiKeyDictionary<int,int, int, monsterData>();

    public sbyte CurrentMaxWave;
    public int CurrentGold;
    public int CurrentExp;
    monsterData mon = new monsterData();
    List<monsterData> listmon = new List<monsterData>();


    public void AddMonsterList(Tuple<string,Vector3,Vector3> data,int gold,int exp)
    {
        mon.Gold = gold;
        mon.Exp = exp;
        mon.monData.Add(data);
    }


    public void FinishList(int Chapter,int Stage, int wave)
    {
        monsterData ms = new monsterData();
        for (int i = 0; i < mon.monData.Count; i++)
        {
            ms.Gold = mon.Gold;
            ms.Exp = mon.Exp;
            ms.monData.Add(mon.monData[i]);
        }
        listmon.Insert(0, ms);
        StageInfo.Add(Chapter, Stage, wave, listmon[0]);
        mon.monData.Clear();
    }

    public bool FindNextStage(int ChapterNum,int StageNum)
    {
        foreach (var item in StageInfo)
        {
            if (item.Key.Equals(ChapterNum))
            {
                foreach (var item2 in item.Value)
                {
                    if (item2.Key.Equals(StageNum+1))
                    {
                        return true;
                    }
                }
            }
        }
        return false;

     }

    public void CreateUnit(int ChapterNum,int StageNum,int WaveNum)
    {
        #region oldCode
        //foreach (var item in chapter)
        //{
        //    if(item.Key.Equals(ChapterNum))
        //    {
        //        foreach (var item2 in item.Value)
        //        {
        //            if(item2.Key.Equals(StageNum))
        //            {
        //                foreach (var item3 in item2.Value)
        //                {
        //                    if(item3.Key.Equals(WaveNum))
        //                    {
        //                        for(int j = 0; j < item3.Value.name.Count ;j++)
        //                        {
        //                            CurrentMaxWave = (sbyte)item2.Value.Count;
        //                            Spawn(item3.Value.name[j], item3.Value.pos[j], item3.Value.rot[j]);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion 
        foreach (var item in StageInfo)
        {
            if (item.Key.Equals(ChapterNum))
            {
                foreach (var item2 in item.Value)
                {
                    if (item2.Key.Equals(StageNum))
                    {
                        foreach (var item3 in item2.Value)
                        {
                            if (item3.Key.Equals(WaveNum))
                            {
                                //Debug.Log("--re"+item3.Value.rewardMos);

                                for (int j = 0; j < item3.Value.monData.Count; j++)
                                {
                                    if (item3.Value.Exp != 0)
                                        CurrentExp = item3.Value.Exp;

                                    //Debug.Log("ex"+item3.Value.Gold + ""+item3.Value.Exp);
                                    CurrentMaxWave = (sbyte)item2.Value.Count;
                                    Spawn(item3.Value.monData[j].Item1, item3.Value.monData[j].Item2, item3.Value.monData[j].Item3);
                                }

                            }
                        }
                    }
                }
            }

        }
    }

    public void Spawn(string _name,Vector3 _pos,Vector3 _rot)
    {
        EnemyFactoryMethod.Instance.CreateEnemy(
              _name,
              _pos,
              Quaternion.Euler(_rot));

    }

}
