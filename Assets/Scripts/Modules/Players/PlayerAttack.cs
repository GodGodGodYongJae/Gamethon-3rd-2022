using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerData playerData;
    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        playerData = GetComponent<PlayerData>();
    }
    public void OnAttackEvent(Transform target)
    {
        if(player.anim.GetInteger("Movement") == 0)
        {
            IDamageable damge = target.GetComponent<IDamageable>();
            damge.Damage(playerData.RandAtk);
        }
       
    }
}
