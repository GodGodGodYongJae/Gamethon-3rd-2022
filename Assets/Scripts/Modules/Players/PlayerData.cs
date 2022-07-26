using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int minAtk = 1000;
    private int maxAtk = 1000;

    public int RandAtk { get { return Random.Range(minAtk, maxAtk); } }

}
