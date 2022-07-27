using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int minAtk = 80;
    private int maxAtk = 100;

    public int RandAtk { get { return Random.Range(minAtk, maxAtk); } }

}
