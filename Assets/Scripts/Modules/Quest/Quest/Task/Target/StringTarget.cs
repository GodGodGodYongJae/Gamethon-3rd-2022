using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Task/Target/string",fileName ="Target_")]
public class StringTarget : TaskTarget
{

    [SerializeField]
    private string value;
    public override object Value => value;

    public override bool IsEqual(object target)
    {
        string targetAsString = target as string;
        //같은 타입이 아니라면.
        if (targetAsString == null)
            return false;
        return value == targetAsString; // 내가 설정한 value가 slime이고, 호출한 target값이 slime이라면 true를 반환 아니라면 false 
    }
}
