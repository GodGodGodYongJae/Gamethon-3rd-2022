using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FSM_State<t>
{
    abstract public void Begin();
    abstract public void Run();
    abstract public void Exit();


}
