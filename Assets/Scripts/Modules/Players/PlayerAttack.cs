using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerData playerData;

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }
    public void OnAttackEvent(Transform target)
    {
        IDamageable damge = target.GetComponent<IDamageable>();
        damge.Damage(playerData.RandAtk);
    }
}
