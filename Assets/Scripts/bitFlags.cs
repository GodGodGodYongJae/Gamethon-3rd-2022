using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// test 
public class bitFlags
{
    [Flags]
    public enum PlayerMoveDirection {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Front = 1 << 2,
        Back = 1 << 3,
        Dash = 1 << 4,
        Attack = 1 << 5,

    }

    public enum Weapon { 
    None = 0,
    OneHandSword = 1
    }


}
