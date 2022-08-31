using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Task/Action/SimpleSet",fileName ="Simple Set")]

// 호출한 성공 값을 그대로 대입해주는 액션.
public class SimpleSet : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount;
    }
}
