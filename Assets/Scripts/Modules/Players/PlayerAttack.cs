using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Player playerData;
   // Player player;
    public Transform Target;
    
    private void Start()
    {
       // player = GetComponent<Player>();
        playerData = GetComponent<Player>();
    }
    public void OnAttackEvent(Transform target)
    {
        if (target == null)
            return;

        Target = target;

    }

    public void AttackAnimationSync()
    {
        //if (player.anim.GetInteger("Movement") == 0)
        //{
            IDamageable damge = Target.GetComponent<IDamageable>();
            damge.Damage(playerData.RandAtk, this.gameObject);
        //}
    }
}
