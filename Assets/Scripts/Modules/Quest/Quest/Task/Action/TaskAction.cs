using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TaskAction : ScriptableObject
{

    // 실행시킨 Task, 현재 성공횟수 , 성공 완료 횟수 
    public abstract int Run(Task task, int currentSuccess, int successCount);
}
