using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMonster : MonoBehaviour,IDamageable
{

    public MonsterType1 monsterdata;
    private int health;
    private float canTakeDamage = 0f;
    // 다음데미지를 받을 검사 시간.
    private float damageCooldown;
    public int Health { get { return health; }set { health = value; } }

    public void Damage(int dmg)
    {
        if(Time.time >= canTakeDamage)
        {
            canTakeDamage = Time.time + damageCooldown;
            Health -= dmg;
            DamageTextSettings(dmg);
        }
    }

    private void DamageTextSettings(int dmg)
    {
      Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
      GameObject TextGameObj = ObjectPoolManager.Instance?.Get("Damage3DText", pos,Quaternion.identity);
        Damage3DText text = TextGameObj.GetComponent<Damage3DText>();
        text.Damage(dmg);
    }

    void Start()
    {
        damageCooldown = 0.5f;
        health = monsterdata.Hp;
        Damage(552);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
