using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int minAtk = 100;
    private int maxAtk = 200;

    public int RandAtk { get { return Random.Range(minAtk, maxAtk); } }

}
