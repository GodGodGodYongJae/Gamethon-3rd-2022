using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/MonsterType", fileName = "Target_")]
public class MonsterTypesTaskTarget : TaskTarget
{
    [SerializeField]
    private MonsterType1 value;
    public override object Value => value;

    public override bool IsEqual(object target)
    {
        var targetAsGameObject = target as MonsterType1;
        if (targetAsGameObject == null)
            return false;
        return targetAsGameObject.Name == value.Name;// 프리팹 같은 경우 이름이 다를 수 있기 때문에 Contains를 사용하여 ' 포함 ' 되어있는지를 확인한다.
    }
}
