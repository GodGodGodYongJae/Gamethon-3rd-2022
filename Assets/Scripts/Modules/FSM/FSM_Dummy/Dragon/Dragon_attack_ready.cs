using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_attack_ready : MonoBehaviour, FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;

    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    /// <summary>
    /// 공격 타입 선언
    /// </summary>
    Dragon_Attack1 DA1;
    Dragon_Attack2 DA2;
    Dragon_Attack3 DA3;

    void Start()
    {
        DA1 = GetComponent<Dragon_Attack1>();
        DA2 = GetComponent<Dragon_Attack2>();
        DA3 = GetComponent<Dragon_Attack3>();
    }
    public void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Attack;
        SelectPatten = false;
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    bool SelectPatten;
    public void Run()
    {
        float dist = Vector3.Distance(m_Owner.m_TransTarget.position, transform.position);
        //print(dist); // 2.5? 
        if(SelectPatten.Equals(false))
        {
            SelectPatten = true;
            if (dist < 4)
            {
                int randAtk = Random.Range(0, 2);
                if (randAtk.Equals(0))
                    DA1.Run(m_Owner, m_Owner.enemy.Type.AttackPoint);
                else
                    StartCoroutine(PushPlayer());
                //DA1.Run(m_Owner, m_Owner.enemy.Type.AttackPoint);
            }
            else
            {
                DA2.Run(m_Owner, m_Owner.enemy.Type.AttackPoint);
            }
        }
  

    }


    IEnumerator PushPlayer()
    {
        m_Owner.m_Animator.SetInteger("attack", 3);
        yield return new WaitForSeconds(1.2f);
        //CameraShake shake = Camera.main.gameObject.GetComponent<CameraShake>();
        //shake.Shake();
        m_Owner.m_Animator.SetInteger("attack", 0);
        m_Owner.m_TransTarget.GetComponent<Player>().isNotMove = true;
        m_Owner.m_TransTarget.GetComponent<PlayerMove>().Jump();
        m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        m_Owner.isDealy = false;
        m_Owner.istargetMove = false;
    }
    
}
