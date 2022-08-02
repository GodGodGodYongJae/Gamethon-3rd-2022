using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface FSM_State<t>
{
     DummyFSM Owner { get; set; }
    public void Begin();
     public void Run();
     public void Exit();


}
