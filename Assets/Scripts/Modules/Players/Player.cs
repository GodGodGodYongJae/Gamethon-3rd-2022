using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    UnityEvent<bitFlags.PlayerMoveDirection,Transform> MoveEvents;
    public UnityEvent<Transform> AttackEvents;
    [SerializeField]
    Transform target;
    

    bitFlags.PlayerMoveDirection PlayerDirection;
    Animator anim;
    public float attackdistance;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.3f;
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
            else if(anim.GetBool("isAttack") == false)
                movement(PlayerDirection, target);

            PlayerDirection = bitFlags.PlayerMoveDirection.None;
        }
            
    }
    void Update()
    {
        if(target != EnemyFactoryMethod.Instance?.target)
        {
            anim.SetBool("isEnemyLoss", true);
            //anim.ResetTrigger("OnCloseAttackCombo");
            //anim.SetInteger("Movement",0);
            target = EnemyFactoryMethod.Instance?.target;
        }
        if(target != null)
            transform.LookAt(target);

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
        Debug.Log(Vector3.Distance(transform.position, target.position));
        if(pd.HasFlag(bitFlags.PlayerMoveDirection.Dash))
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > attackdistance)
                PlayerDirection = bitFlags.PlayerMoveDirection.Dash;
            else
                PlayerDirection = bitFlags.PlayerMoveDirection.Attack;
        }
        else if (anim.GetInteger("Movement") == 0 )
        {
            PlayerDirection = pd;        
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