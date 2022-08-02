using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Attack : MonoBehaviour,FSM_State<DummyFSM>
{

    public DummyFSM m_Owner;
    //private float AttackWaitTime;
    private float AttackWaittingTime;

    private void Start()
    {
        AttackWaittingTime = 0.0f;
    }

    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    public  void Begin()
    {
        //Debug.Log("Attack Begin");
        m_Owner.m_eCurState = DummyFSM.State.Attack;
    }

    public  void Exit()
    {
        m_Owner.indicator.SetActive(false);
        //Debug.Log("Attack Exit");
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    public  void Run()
    {

        m_Owner.indicator.SetActive(true);
        AttackWaittingTime += Time.deltaTime;
        if (AttackWaittingTime > m_Owner.AttackWaitTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange*3, (1 << LayerMask.NameToLayer("Player")));
            AttackWaittingTime = 0f;
            m_Owner.indicator.SetActive(false);
            if (m_Owner.indicatorRangeCircle.isCollisition(hitColliders[0].transform))
            {
                IDamageable damage = hitColliders[0].GetComponent<IDamageable>();
                damage.Damage(m_Owner.enemy.Type.AttackPoint,m_Owner.gameObject);
            }
           
            m_Owner.indicator.SetActive(false);
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
            m_Owner.isDealy = false;
            m_Owner.istargetMove = false;
        }

    }



}
