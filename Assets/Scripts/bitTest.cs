using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bitTest : MonoBehaviour
{
    [Flags]
    public enum Col {
    None = 0,
    Left = 1 << 0,
    Right = 1 << 1,
    Front = 1 << 2,
    Back = 1 << 3,
    }
    Col bittypes;
    private void Start()
    {
        bittypes = Col.None;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            bittypes = Col.Left;
            Debug.Log("Left실행"+ bittypes);
            bittypes &= ~Col.Left;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            bittypes = Col.Right;
            Debug.Log("Right실행" + bittypes);
            bittypes &= ~Col.Right;
        }

    }

}
