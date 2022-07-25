using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    UnityEvent<bitFlags.PlayerMoveDirection,Transform> MoveEvents;
    public UnityEvent<Transform> AttackEvents;
    [SerializeField]
    Transform target;
    

    public bitFlags.PlayerMoveDirection PlayerDirection;
   
    public Animator anim;
    public float attackdistance;

    Rigidbody rb;

    private int health;
    public bool isDeath;
    public int Health { get { return health; } set { health = value; }  }

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //Time.timeScale = 0.3f;
        Health = 30;
        anim = GetComponent<Animator>();
        PlayerDirection = bitFlags.PlayerMoveDirection.None;

    }




    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PlayerDirection != bitFlags.PlayerMoveDirection.None)
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

        //rb.velocity = Vector3.zero;
        if (target != EnemyFactoryMethod.Instance?.target)
        {
            //anim.SetBool("isEnemyLoss", true);
            target = EnemyFactoryMethod.Instance?.target;
        }
        if(target != null)
        {

            Vector3 targetPos = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
            if(!isDeath)
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

    public void OnSwordSkilBtn()
    {
        CutSceneManager.Instance.OnScene(true, CutSceneManager.Events.SwordSkill, true);
    }
    public void movement(bitFlags.PlayerMoveDirection pd, Transform target)
    {
            MoveEvents?.Invoke(pd,target);
    }

    public void OnAttackEvents()
    {
        AttackEvents?.Invoke(this.target);
    }
    public void OnTouchEvent(bitFlags.PlayerMoveDirection pd)
    {
        if (target == null)
            return;
        //Debug.Log(Vector3.Distance(transform.position, target.position));
        if(pd.HasFlag(bitFlags.PlayerMoveDirection.Dash))
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
        else if (anim.GetInteger("Movement") == 0 )
        {
            PlayerDirection = pd;        
        }

    }

    public void Damage(int damage,GameObject tr)
    {
        Health -= damage;
        Debug.Log(Health);
        if(Health <= 0)
        {
            CutSceneManager.Instance.OnScene(true,CutSceneManager.Events.Death);
            isDeath = true;
            transform.LookAt(tr.transform);
            Time.timeScale = 0.35f;
            anim.SetBool("isDeath", true);
        }
    }
}


//if (attackdistance < distance && anim.GetBool("OnCloseAttackCombo") == false)
//{
//    PlayerDirection = pd;
//    return;
//}
//else if(pd == bitFlags.PlayerMoveDirection.Dash)
//{
//    if (distance < attackdistance)
//        PlayerDirection = bitFlags.PlayerMoveDirection.Attack;
//    else
//        return;
//}
//else if(anim.GetBool("OnCloseAttackCombo") == false)
//{
//    PlayerDirection = pd;
//    return;
//}