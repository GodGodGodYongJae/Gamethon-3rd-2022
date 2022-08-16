using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV_Stage : MonoBehaviour
{
    [SerializeField]
    TextAsset asset;
    private Tuple<int, int, int> currentData;
    private Tuple<int, int, int> LastData;
    private void Awake()
    {
        EnemyGenerators.monsterData mon = new EnemyGenerators.monsterData();
        MultiKeyDictionary<int, int, int, EnemyGenerators.monsterData > StageInfo = EnemyGenerators.Instance.StageInfo;
        //Tuple<string, Vector3, Vector3> mondata = new Tuple<string, Vector3, Vector3>("Golem", Vector3.zero, Vector3.zero);

        List<Dictionary<string, object>> data = CSVReader.Read(asset);
        for (int i = 0; i < data.Count; i++)
        {
            #region test
            //Vector3 pos = new Vector3();
            //object temp = data[i]["Position(x/y/z)"];
            //string[] result = temp.ToString().Split(new char[] { '/' });
            //pos.x = float.Parse(result[0]);
            //pos.y = float.Parse(result[1]);
            //pos.z = float.Parse(result[2]);

            //print("Chapter" + data[i]["Chapter"] + " " +
            //      "Stage" + data[i]["Stage"] + " " +
            //      "Wave" + data[i]["Wave"] + " " +
            //      "MonsterName" + data[i]["MonsterName"] + " " +
            //      "Position(x'y'z)" + data[i]["Position(x'y'z)"]
            //      );
            #endregion
            //ConvertCSVtoVector3()
            currentData = new Tuple<int, int, int>((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);
            if (currentData.Equals(LastData))
            {
                Tuple<string, Vector3, Vector3> datas = new Tuple<string, Vector3, Vector3>(data[i]["MonsterName"].ToString(), ConvertCSVtoVector3(data[i]["Position(x/y/z)"]), ConvertCSVtoVector3(data[i]["Rotation(x/y/z)"]));
                EnemyGenerators.Instance.AddMonsterList(datas);
            }

            else
            EnemyGenerators.Instance.FinishList((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);

                LastData = new Tuple<int, int, int>((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);
        }
    }

    //private Tuple<string,Vector3,Vector3> Create()
    //{
    //    return;
    //}

    private Vector3 ConvertCSVtoVector3(object _header)
    {
        Vector3 pos = new Vector3();
        string[] result = _header.ToString().Split(new char[] { '/' });
        pos.x = float.Parse(result[0]);
        pos.y = float.Parse(result[1]);
        pos.z = float.Parse(result[2]);

        return pos;
    }
}
