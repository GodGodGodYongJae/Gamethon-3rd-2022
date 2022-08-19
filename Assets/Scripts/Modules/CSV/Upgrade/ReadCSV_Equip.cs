using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV_Equip : ReadCSV_UPGRADE
{


    protected override void ReadUpgrade()
    {
        for (int i = 0; i < data.Count; i++)
        {
            UpgradData upgrad = new UpgradData();
            upgrad.Level = (int)data[i]["Num"];
            upgrad.Ruby = (int)data[i]["Ruby"];
            upgrad.DM = (int)data[i]["DM"];
            upgrad.increase[0] = (int)data[i]["DEF"];
            upgrad.increase[1] = (int)data[i]["HP"];

            upgradList.Add(upgrad);
        }
    }
}
