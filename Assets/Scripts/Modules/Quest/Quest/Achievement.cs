using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Achievement", fileName = "Achievement_")]
public class Achievement : Quest
{

    public override bool IsSaveAble => true;
    public override bool IsCancelable => false;
    public override void Cancel()
    {
        Debug.LogAssertion("Achievement Can`t be Canceled");
    }

}
