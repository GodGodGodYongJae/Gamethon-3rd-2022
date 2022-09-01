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
        return targetAsGameObject.Name == value.Name;// ������ ���� ��� �̸��� �ٸ� �� �ֱ� ������ Contains�� ����Ͽ� ' ���� ' �Ǿ��ִ����� Ȯ���Ѵ�.
    }
}
