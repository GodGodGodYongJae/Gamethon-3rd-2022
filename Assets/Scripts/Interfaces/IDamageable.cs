using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
   int Health { get; set; }

    // 데미지와 공격자
    void Damage(int damage,GameObject attacker);
}
