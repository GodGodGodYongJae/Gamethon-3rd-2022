using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Player player;
   // Player player;
    public Transform Target;
    
    private void Start()
    {
        // player = GetComponent<Player>();
        player = GetComponent<Player>();
    }
    public void OnAttackEvent(Transform target)
    {
        if (target == null)
            return;

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist < 3)
            Target = target;
        else
            Target = null;
    }

    public void AttackAnimationSync()
    {
        //if (player.anim.GetInteger("Movement") == 0)
        //{
        //Target = player.target;
        if (Target == null)
            return;

        IDamageable damge = Target.GetComponent<IDamageable>();
            damge.Damage(player.RandAtk, this.gameObject);
        //}
    }
}
