using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Task/Action/GstarSkin", fileName = "GstarSkin")]
public class Item_GstarSkin : ItemTask
{
    public override void Run()
    {
        PlayFabData.Instance.isSkin = true;
        Debug.Log("æ∆¿Ã≈€æ∏1");
    }
}
