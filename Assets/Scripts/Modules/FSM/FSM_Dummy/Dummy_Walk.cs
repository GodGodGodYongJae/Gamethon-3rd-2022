using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummy_Walk : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    private NavMeshAgent agent;
    private Animator m_Animator;

    public Dummy_Walk(DummyFSM _owner)
    {
        m_Owner = _owner;
    }
    public override void Begin()
    {
        Debug.Log("Walk Begin");
        agent = m_Owner.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        m_Owner.m_eCurState = DummyFSM.State.Walk;

    }

    public override void Run()
    {
        if(m_Owner.m_TransTarget ==null)
        {
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
        if (m_Owner.m_TransTarget != null)
            GotoTarget();
        if(m_Owner.m_TransTarget !=null && m_Owner.m_fAttackRange >= Vector3.Distance(m_Owner.transform.position,m_Owner.m_TransTarget.position))
        {
            Debug.Log("АјАн!");
            //m_Owner.ChangeFSM
        }
    }

    public override void Exit()
    {
        Debug.Log("WAlk EXit");
        agent.isStopped = true;
        m_Owner.m_ePrevState = DummyFSM.State.Walk;
    }


    private bool FindRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange, (1 << LayerMask.NameToLayer("Player")));
        if(hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; ++i)
            {
                if(hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_Owner.m_TransTarget = hitColliders[i].transform;
                }
            }
            if (m_Owner.m_TransTarget == null)
                m_Owner.m_TransTarget = hitColliders[0].transform;
            return true;
        }
        return false;
    }

    private void GotoTarget()
    {
        agent.SetDestination(m_Owner.m_TransTarget.position);
    }

}
