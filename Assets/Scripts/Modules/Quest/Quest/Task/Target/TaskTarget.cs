using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{

    //강화라는 것을 하거나 Slime을 다섯마리 잡아라 등 형태가 없는 것.

    public abstract object Value { get; }
    public abstract bool IsEqual(object target);
}
