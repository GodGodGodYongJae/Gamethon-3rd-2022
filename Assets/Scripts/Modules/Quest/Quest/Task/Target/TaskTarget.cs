using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{

    //��ȭ��� ���� �ϰų� Slime�� �ټ����� ��ƶ� �� ���°� ���� ��.

    public abstract object Value { get; }
    public abstract bool IsEqual(object target);
}
