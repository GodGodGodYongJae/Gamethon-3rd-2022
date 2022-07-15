using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    //status.. ect
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected MonsterType1 type;


    private float canTakeDamage;
    private float damageCooldown;
    protected void Start()
    {
        Init();
    }
    protected virtual void Init()
    {
        damageCooldown = 0.5f;
           health = type.Hp;
    }

    public void Damage(int damage)
    {
        if (Time.time >= canTakeDamage)
        {
            canTakeDamage = Time.time + damageCooldown;
            Health -= damage;
            DamageTextSettings(damage);
        }
    }
    private void DamageTextSettings(int dmg)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        GameObject TextGameObj = ObjectPoolManager.Instance?.Get("Damage3DText", pos, Quaternion.identity);
        Damage3DText text = TextGameObj.GetComponent<Damage3DText>();
        text.Damage(dmg);
    }

    public int Health { get { return health; } set { health = value; } }
}
