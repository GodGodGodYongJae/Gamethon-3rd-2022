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
        //���� Ÿ���� �ƴ϶��.
        if (targetAsString == null)
            return false;
        return value == targetAsString; // ���� ������ value�� slime�̰�, ȣ���� target���� slime�̶�� true�� ��ȯ �ƴ϶�� false 
    }
}
