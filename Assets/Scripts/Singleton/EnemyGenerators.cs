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
    }
    [Serializable]
    public class Mondata2
    {
        [SerializeReference] public List<string> name;
        [SerializeReference] public List<Vector3> pos;
        [SerializeReference] public List<Vector3> rot;
    }

    [System.Serializable]
    public class Chapter : SerializableDictionary<int, Stage> { };
    public Chapter chapter = new Chapter();
    
    [System.Serializable]
    public class Stage : SerializableDictionary<int,wave> { };


    [System.Serializable]
    public class wave : SerializableDictionary<int, Mondata2> { };

   public MultiKeyDictionary<int,int, int, monsterData> StageInfo = new MultiKeyDictionary<int,int, int, monsterData>();

    public sbyte CurrentMaxWave;
    monsterData mon = new monsterData();



    private void Start()
    {
        //monsterData mon = new monsterData();
        //Tuple<string, Vector3, Vector3> data = new Tuple<string, Vector3, Vector3>("Golem", Vector3.zero, Vector3.zero);
        //mon.monData.Add(data);
        //mon.monData.Add(data);
        //StageInfo.Add(1,1,1, mon);
        //StageInfo.Add(1, 1, 2, mon);
        //StageInfo.Add(1, 2, 1, mon);
    }

    public void AddMonsterList(Tuple<string,Vector3,Vector3> data)
    {
        mon.monData.Add(data);
    }
    public void FinishList(int Chapter,int Stage, int wave)
    {
        StageInfo.Add(Chapter, Stage, wave, mon);
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
                                for (int j = 0; j < item3.Value.monData.Count; j++)
                                {
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
