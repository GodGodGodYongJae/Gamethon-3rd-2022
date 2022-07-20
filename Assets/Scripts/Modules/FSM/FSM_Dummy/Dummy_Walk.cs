using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummy_Walk : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    private NavMeshAgent agent;
    //private float currentAtkDealy;
    //private bool isDealy;
    public Dummy_Walk(DummyFSM _owner)
    {
        m_Owner = _owner;
    }
    public override void Begin()
    {
        //Debug.Log("Walk Begin");

        agent = m_Owner.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        m_Owner.m_eCurState = DummyFSM.State.Walk;

    }

    public override void Run()
    {
        if(m_Owner.m_TransTarget == null)
        {
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
        Vector3 targetDir = (m_Owner.m_TransTarget.position - m_Owner.transform.position).normalized;
        float dot = Vector3.Dot(m_Owner.transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (m_Owner.m_TransTarget != null && dot <= 1)
        {

                Vector3 lookPos = m_Owner.m_TransTarget.position - m_Owner.transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
            m_Owner.transform.rotation = Quaternion.Slerp(m_Owner.transform.rotation, rotation, 3.0f);
            
        }
            GotoTarget();
      
        if (m_Owner.m_TransTarget !=null 
            && m_Owner.m_fAttackRange >= Vector3.Distance(m_Owner.transform.position,m_Owner.m_TransTarget.position)
             && theta <= 7)
        {
            if(m_Owner.isDealy)

                m_Owner.ChangeFSM(DummyFSM.State.Attack);
            else
            {
                m_Owner.ChangeFSM(DummyFSM.State.IDLE);
                m_Owner.istargetMove = true;
            }
               
        }
    }

    public override void Exit()
    {
        //Debug.Log("WAlk EXit");
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
