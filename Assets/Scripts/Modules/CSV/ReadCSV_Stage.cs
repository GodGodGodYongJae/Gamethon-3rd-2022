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
            //       "No." + data[i]["No."] + " " +
            //      "Position(x/y/z)" + data[i]["Position(x/y/z)"]
            //      );
            #endregion
            currentData = new Tuple<int, int, int>((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);

            int gold = (data[i]["MosStone"].ToString() != "") ?  (int)data[i]["MosStone"] : 0;
            int exp = (data[i]["Exp"].ToString() != "") ? (int)data[i]["Exp"] : 0;


            if (LastData == null)
                LastData = currentData;

            if (currentData.Item1.Equals(LastData.Item1) &&
                currentData.Item2.Equals(LastData.Item2) &&
                currentData.Item3.Equals(LastData.Item3))
            {
                Tuple<string, Vector3, Vector3> datas = new Tuple<string, Vector3, Vector3>(data[i]["MonsterName"].ToString(), ConvertCSVtoVector3(data[i]["Position(x/y/z)"]), ConvertCSVtoVector3(data[i]["Rotation(x/y/z)"]));
                EnemyGenerators.Instance.AddMonsterList(datas, gold, exp);
            }

            else
            {
                EnemyGenerators.Instance.FinishList(LastData.Item1, LastData.Item2, LastData.Item3);
                LastData = new Tuple<int, int, int>((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);
                Tuple<string, Vector3, Vector3> datas = new Tuple<string, Vector3, Vector3>(data[i]["MonsterName"].ToString(), ConvertCSVtoVector3(data[i]["Position(x/y/z)"]), ConvertCSVtoVector3(data[i]["Rotation(x/y/z)"]));
                EnemyGenerators.Instance.AddMonsterList(datas,gold,exp);
            }
            LastData = new Tuple<int, int, int>((int)data[i]["Chapter"], (int)data[i]["Stage"], (int)data[i]["Wave"]);

        }
       EnemyGenerators.Instance.FinishList(LastData.Item1, LastData.Item2, LastData.Item3);
    }

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