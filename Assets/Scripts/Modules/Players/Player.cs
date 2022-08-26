using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    public bool isTestMode;
    [SerializeField]
    UnityEvent<bitFlags.PlayerMoveDirection, Transform> MoveEvents;
    public UnityEvent<Transform> AttackEvents;
    [SerializeField]
    public Transform target;


    public bitFlags.PlayerMoveDirection PlayerDirection;

    public Animator anim;
    public float attackdistance;

    Rigidbody rb;

    private int exp;
    private int level;
    public bool isDeath;

    public GameObject effectParent;
    // Start is called before the first frame update

    #region 캐릭터 스텟 & 스킬에 필요한 데이터
    [HideInInspector]
    public float atkSpeed; // 공격속도
    [HideInInspector]
    public int maxHealth; // 최대 HP 
    private int health; // 현재 체력

    public int Health { get { return health; } set { health = value; } } // 프로퍼티

    int minAtk;
    int maxAtk;
    int def;

    #endregion
    [SerializeField]
    private GameObject bloodWindow;
    private void Awake()
    {
        EnemyFactoryMethod.Instance.player = this;
    }

    void Start()
    {

        //rb = GetComponent<Rigidbody>();
        //Time.timeScale = 0.3f;

        exp = 0;
        level = 1;
        minAtk = (isTestMode.Equals(false)) ? PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.atk] : 100;
        maxAtk = minAtk + (minAtk / 2);
        def = (isTestMode.Equals(false)) ? PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.def] : 10;
        atkSpeed = 1f;
        Health = (isTestMode.Equals(false)) ? 100:999;
        maxHealth = Health;
        anim = GetComponent<Animator>();
        PlayerDirection = bitFlags.PlayerMoveDirection.None;
        string Hptext = Health + "/" + maxHealth;
        UIManager.Instance.TextChange(UIManager.UI.HPText, Hptext);
        // UIManager.Instance.ChangeExpBar(ref exp,ref level);
    }

    public bool ChangeExp(int _exp)
    {
        exp += _exp;
        return UIManager.Instance.ChangeExpBar(ref exp, ref level);
    }
    public void RequestPlayerData(int hp)
    {
        Health = hp;
        maxHealth = hp;
        string Hptext = Health + "/" + maxHealth;
        UIManager.Instance.TextChange(UIManager.UI.HPText, Hptext);
    }

    private void FixedUpdate()
    {
        if (PlayerDirection != bitFlags.PlayerMoveDirection.None && isDeath.Equals(false))
        {
            if (PlayerDirection == bitFlags.PlayerMoveDirection.Attack)
                OnAttackEvents();
            else //if(anim.GetBool("isAttack") == false)
                movement(PlayerDirection, target);

            PlayerDirection = bitFlags.PlayerMoveDirection.None;
        }

    }
    void Update()
    {

        //Debug.Log(transform.TransformDirection(Vector3.forward));
        //rb.velocity = Vector3.zero;
        if (target != EnemyFactoryMethod.Instance?.target)
        {
            //anim.SetBool("isEnemyLoss", true);
            target = EnemyFactoryMethod.Instance?.target;
        }
        if (target != null)
        {

            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            if (!isDeath)
                transform.LookAt(targetPos);
        }

        #region KeyBoard
        //if (anim.GetInteger("Movement") == 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.D))
        //        PlayerDirection = bitFlags.PlayerMoveDirection.Right;
        //    else if (Input.GetKeyDown(KeyCode.A))
        //        PlayerDirection = bitFlags.PlayerMoveDirection.Left;
        //    else if (Input.GetKeyDown(KeyCode.W))
        //        PlayerDirection = bitFlags.PlayerMoveDirection.Front;
        //    else if (Input.GetKeyDown(KeyCode.S))
        //        PlayerDirection = bitFlags.PlayerMoveDirection.Back;

        //}
        #endregion

    }


    public void movement(bitFlags.PlayerMoveDirection pd, Transform target)
    {
        MoveEvents?.Invoke(pd, target);
    }

    public void OnAttackEvents()
    {
        AttackEvents?.Invoke(this.target);
    }
    public bool isNotMove;
    public void OnTouchEvent(bitFlags.PlayerMoveDirection pd)
    {
        if (target == null || isNotMove.Equals(true))
            return;
        //Debug.Log(Vector3.Distance(transform.position, target.position));
        if (pd.HasFlag(bitFlags.PlayerMoveDirection.Dash))
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > attackdistance)
            {
                if (anim.GetInteger("Movement") == 0)
                    PlayerDirection = bitFlags.PlayerMoveDirection.Dash;
            }

            else
                PlayerDirection = bitFlags.PlayerMoveDirection.Attack;
        }
        else if (anim.GetInteger("Movement") == 0 || anim.GetInteger("Movement") == 5)
        {
            PlayerDirection = pd;
        }

    }

    public void Damage(int _damage, GameObject tr)
    {
        int damage = Mathf.FloorToInt(_damage * (1 - (def / 100)));
        Health -= damage;
        UIManager.Instance.ChangeHpBar(UIManager.UI.HpBar, Health, maxHealth);
        string Hptext = Health + "/" + maxHealth;
        UIManager.Instance.TextChange(UIManager.UI.HPText, Hptext);
        StartCoroutine(Damages());
        if (Health <= 0)
        {
            CutSceneManager.Instance.OnScene(true, CutSceneManager.Events.Death);
            isDeath = true;
            transform.LookAt(tr.transform);
            Time.timeScale = 0.35f;
            anim.SetBool("isDeath", true);
            SoundManager.Inst.PlayBGM("Death - Bass, Tension & Voices");
        }
    }

    public void Heal(int heal)
    {
        Health += heal;
        if (Health > maxHealth)
            Health = maxHealth;

        UIManager.Instance.ChangeHpBar(UIManager.UI.HpBar, Health, maxHealth);
        string Hptext = Health + "/" + maxHealth;
        UIManager.Instance.TextChange(UIManager.UI.HPText, Hptext);
    }

    public int RandAtk { get { return Random.Range(minAtk, maxAtk); } }

    public void Respawn()
    {
        Time.timeScale = 1;
        Health = maxHealth;
        isDeath = false;
        anim.SetBool("isDeath", false);
        UIManager.Instance.ChangeHpBar(UIManager.UI.HpBar, Health, maxHealth);
        string Hptext = Health + "/" + maxHealth;
        UIManager.Instance.TextChange(UIManager.UI.HPText, Hptext);
        SoundManager.Inst.PlayBGM("Space Threat (Electronic Dramatic Version)");
    }

    //차후 수정해야함
    private IEnumerator Damages()
    {
        CameraShake shake = Camera.main.gameObject.GetComponent<CameraShake>();
        shake.Shake();
        bloodWindow.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        bloodWindow.SetActive(false);
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Ground"))
        {
            isNotMove = false;
        }
    }
}

