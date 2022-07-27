using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    //status.. ect
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected MonsterType1 type;

    public MonsterType1 Type { get { return type; } }
    private float canTakeDamage;
    private float damageCooldown;

    public UnityEvent<int> getDamageEvent;
    public float DestoryTime;
    public bool isDeath;

    protected void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        damageCooldown = 0.5f;
           health = type.Hp;
    }

    public void Damage(int _damage,GameObject tr)
    {
        if (Time.time >= canTakeDamage && isDeath.Equals(false))
        {
            int damage = Mathf.FloorToInt(_damage * (1 - (type.DefencePoint / 100)));
            canTakeDamage = Time.time + damageCooldown;
            Health -= damage;
            OnDamageEvent();
            DamageTextSettings(damage);
            //if (Health <= 0)
            //    EnemyFactoryMethod.Instance?.DeleteEnemy(this.gameObject);
        }
    }
    private void DamageTextSettings(int dmg)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        GameObject TextGameObj = ObjectPoolManager.Instance?.Get("Damage3DText", pos, Quaternion.identity);
        Damage3DText text = TextGameObj.GetComponent<Damage3DText>();
        text.Damage(dmg);
    }

    public void OnDamageEvent()
    {
        getDamageEvent?.Invoke(Health);
    }

    public IEnumerator OnRateDestory(GameObject deathObj )
    {
        isDeath = true;
        Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        this.gameObject.transform.parent = deathObj.transform;
        yield return new WaitForSeconds(DestoryTime);
        this.gameObject.SetActive(false);
      
    }
    public int Health { get { return health; } set { health = value; } }
}
