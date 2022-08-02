using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerators : MonoBehaviour
{
    [Serializable]
    public class monData
    {
        public string name;
        [SerializeReference]public Vector3 pos;
        [SerializeReference]public Vector3 rot;
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

    public Chapter Cha = new Chapter();
    
    [System.Serializable]
    public class Stage : SerializableDictionary<int,wave> { };

    [System.Serializable]
    public class wave : SerializableDictionary<int, Mondata2> { };



    private void Start()
    {
        CreateUnit(1, 1, 2);
    }

    public void CreateUnit(int ChapterNum,int StageNum,int WaveNum)
    {
        foreach(var item in Cha)
        {
            if(item.Key.Equals(ChapterNum))
            {
                foreach (var item2 in item.Value)
                {
                    if(item.Key.Equals(StageNum))
                    {
                        foreach (var item3 in item2.Value)
                        {
                            if(item3.Key.Equals(ChapterNum))
                            {
                                Debug.Log(item3.Value.name.Count);
                                for(int j = 0; j < item3.Value.name.Count ;j++)
                                {
                                    Spawn(item3.Value.name[j], item3.Value.pos[j], item3.Value.rot[j]);
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
        Debug.Log(_name + "ÀÌ°¡" + _pos + "¿¡" + _rot);

    }

}
