using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TaskAction : ScriptableObject
{

    // �����Ų Task, ���� ����Ƚ�� , ���� �Ϸ� Ƚ�� 
    public abstract int Run(Task task, int currentSuccess, int successCount);
}
