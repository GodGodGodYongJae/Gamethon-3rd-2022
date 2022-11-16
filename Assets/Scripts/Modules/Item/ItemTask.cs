using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTask : ScriptableObject
{
    // 실행시킬 Action
    public abstract void Run();
}
