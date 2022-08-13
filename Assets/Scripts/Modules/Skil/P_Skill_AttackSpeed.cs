using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Skill_AttackSpeed : MonoBehaviour,ISkil
{
    public float atkSpeed = 1f;
    Transform parent;
    Player player;
    void Start()
    {
        parent = this.transform.parent;
        player = parent.transform.GetComponent<Player>();
        ChangeAtkSpeed();
    }

    void ChangeAtkSpeed()
    {
        player.atkSpeed = atkSpeed;
    }
}
