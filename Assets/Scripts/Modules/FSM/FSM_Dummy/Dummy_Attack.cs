using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Attack : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    private float AttackWaitTime;
    private float AttackWaittingTime;
 
    public Dummy_Attack(DummyFSM _owner)
    {
        m_Owner = _owner;
        AttackWaitTime = 3.0f;
        AttackWaittingTime = 0.0f;
    }

    public override void Begin()
    {
        Debug.Log("Attack Begin");
        m_Owner.m_eCurState = DummyFSM.State.Attack;
    }

    public override void Exit()
    {
        Debug.Log("Attack Exit");
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    public override void Run()
    {
        m_Owner.indicator.SetActive(true);
        AttackWaittingTime += Time.deltaTime;
        if (AttackWaittingTime > AttackWaitTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange*3, (1 << LayerMask.NameToLayer("Player")));
            AttackWaittingTime = 0f;
            m_Owner.indicator.SetActive(false);
            if (m_Owner.indicatorRangeCircle.isCollisition(hitColliders[0].transform))
            {
                Debug.Log("¸ÂÀ½");
            }
            m_Owner.indicator.SetActive(false);
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }

    }



}
