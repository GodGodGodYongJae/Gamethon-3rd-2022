using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTerrain : MonoBehaviour
{
    [SerializeField]
    GameObject[] ChpaterTerrain;
    private void Awake()
    {
        sbyte chpaterNum = (sbyte)((int)
            ScenesManager.Instance.StartChpater - 1);
        ChpaterTerrain[chpaterNum].SetActive(true);
    }
}
