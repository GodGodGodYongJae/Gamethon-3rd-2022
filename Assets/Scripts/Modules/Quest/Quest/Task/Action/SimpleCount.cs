using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Task/Action/SimpleCount",fileName ="Simple Count")]
//현재 성공 카운터에 받은 카운터를 받은뒤 성공 Count를 더해서 반환해주는 Action 임.
public class SimpleCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return currentSuccess + successCount;
    }

}
