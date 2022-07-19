using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Idle : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;

    public Dummy_Idle(DummyFSM _owner)
    {
        m_Owner = _owner;
    }
    public override void Begin()
    {
        Debug.Log("idle Begin Log");
        m_Owner.m_eCurState = DummyFSM.State.IDLE;
    }
    public override void Run()
    {
        if (FindTarget())
            m_Owner.ChangeFSM(DummyFSM.State.Walk);
        else
            m_Owner.m_TransTarget = null;
    }
    public override void Exit()
    {
        Debug.Log("idle Exit");
        m_Owner.m_ePrevState = DummyFSM.State.IDLE;
    }


    private bool FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange, (1 << LayerMask.NameToLayer("Player")));
        if(hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if(hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_Owner.m_TransTarget = hitColliders[i].transform;
                }
                if (m_Owner.m_TransTarget == null)
                    m_Owner.m_TransTarget = hitColliders[0].transform;

                return true;
            }
        }
        return false;
    }


}
