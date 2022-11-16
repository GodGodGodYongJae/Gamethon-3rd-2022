using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Task/Action/GstartMosRuby", fileName = "GstartMosRuby")]
public class Item_GstarMosRuby : ItemTask
{
    public override void Run()
    {
        PlayFabData.Instance.AddAccountData("DM", 100);
        PlayFabData.Instance.AddAccountData("RU", 5);

        Debug.Log("æ∆¿Ã≈€æ∏2");
    }

}
