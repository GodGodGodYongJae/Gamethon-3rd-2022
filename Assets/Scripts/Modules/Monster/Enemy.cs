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
    public GameObject effectObj;
    public MonsterType1 Type { get { return type; } }
    private float canTakeDamage;
    private float damageCooldown;

    public UnityEvent<int,int> getDamageEvent;
    public float DestoryTime;
    public bool isDeath;

    [HideInInspector]
    public int DamageNum;

    [Header("Qeust")]
    [SerializeField]
    private Category categorykill;
    [SerializeField]
    private TaskTarget taskTarget;

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Init();
    }

    private Rigidbody rigidbody;
    private void Update()
    {
        rigidbody.velocity = Vector3.zero;
    }
    protected virtual void Init()
    {
        damageCooldown = 0.5f;
        DamageNum = 1;
        health = type.Hp;
    }

    public void Damage(int _damage,GameObject tr)
    {
        if (Time.time >= canTakeDamage && isDeath.Equals(false))
        {
            for (int i = 0; i < DamageNum; i++)
            {
                int damage = Mathf.FloorToInt(_damage * (1 - (type.DefencePoint / 100)));
                canTakeDamage = Time.time + damageCooldown;
                Health -= damage;
                OnDamageEvent();
                DamageTextSettings(damage);
            }
            DamageNum = 1;
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
        getDamageEvent?.Invoke(Health, type.Hp);
    }

    public IEnumerator OnRateDestory(GameObject deathObj )
    {
        isDeath = true;
        Destroy(this.gameObject.GetComponent<Collider>());
        this.gameObject.transform.parent = deathObj.transform;
        yield return new WaitForSeconds(DestoryTime);
        this.gameObject.SetActive(false);

        if(categorykill != null && taskTarget != null)
        QuestSystem.Instance.ReceiveReport(categorykill, taskTarget, 1);
      
    }
    public int Health { get { return health; } set { health = value; } }
}
