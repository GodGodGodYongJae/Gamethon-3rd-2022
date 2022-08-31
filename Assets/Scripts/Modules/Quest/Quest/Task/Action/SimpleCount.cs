using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Task/Action/SimpleCount",fileName ="Simple Count")]
//���� ���� ī���Ϳ� ���� ī���͸� ������ ���� Count�� ���ؼ� ��ȯ���ִ� Action ��.
public class SimpleCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return currentSuccess + successCount;
    }

}
