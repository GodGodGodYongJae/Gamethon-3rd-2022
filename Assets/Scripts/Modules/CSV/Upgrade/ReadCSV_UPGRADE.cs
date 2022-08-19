using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReadCSV_UPGRADE : MonoBehaviour
{
    [SerializeField]
    TextAsset asset;

    protected List<Dictionary<string, object>> data;
    public List<UpgradData> upgradList = new List<UpgradData>();

    protected virtual void Awake()
    {
        data = CSVReader.Read(asset);
        ReadUpgrade();
    }


    protected abstract void ReadUpgrade();
}
