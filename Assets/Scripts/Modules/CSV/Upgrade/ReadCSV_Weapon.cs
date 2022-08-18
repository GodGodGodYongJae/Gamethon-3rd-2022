using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV_Weapon : MonoBehaviour
{
    [SerializeField]
    TextAsset asset;

    [SerializeField]
    TextAsset defasset;
    List<Dictionary<string, object>> data;
    public List<UpgradData> upgradList = new List<UpgradData>();

    List<Dictionary<string, object>> equipdata;
    public List<UpgradData> upgradEquipList = new List<UpgradData>();
    private void Awake()
    {
        data = CSVReader.Read(asset);
        ReadWeaponData();
        equipdata = CSVReader.Read(defasset);
        ReadEquipData();
    }

    //방어구 함수는 방어구 함수로 만들기
    void ReadWeaponData()
    {
        for (int i = 0; i < data.Count; i++)
        {
            UpgradData upgrad = new UpgradData();
            upgrad.Level = (int)data[i]["Num"];
            upgrad.Ruby = (int)data[i]["Ruby"];
            upgrad.DM = (int)data[i]["DM"];
            upgrad.increase[0] = (int)data[i]["Atk"];
            upgrad.increase[1] = (int)data[i]["Cri"];
            
            upgradList.Add(upgrad);
        }
    }

    void ReadEquipData()
    {
        for (int i = 0; i < equipdata.Count; i++)
        {
            UpgradData upgrad = new UpgradData();
            upgrad.Level = (int)data[i]["Num"];
            upgrad.Ruby = (int)data[i]["Ruby"];
            upgrad.DM = (int)data[i]["DM"];
            upgrad.increase[0] = (int)data[i]["DEF"];
            upgrad.increase[1] = (int)data[i]["HP"];

            upgradEquipList.Add(upgrad);
        }
    }
 
}
