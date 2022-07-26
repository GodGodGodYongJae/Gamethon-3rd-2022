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
        //Debug.Log("idle Begin Log");
        m_Owner.m_eCurState = DummyFSM.State.IDLE;
    }
    public override void Run()
    {

      

        if (FindTarget())
        {
            Vector3 targetDir = (m_Owner.m_TransTarget.position - m_Owner.transform.position).normalized;
            float dot = Vector3.Dot(m_Owner.transform.forward, targetDir);
            float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;
            float distance = Vector3.Distance(m_Owner.m_TransTarget.position, m_Owner.transform.position);
            if ( m_Owner.istargetMove == false || (m_Owner.isDealy == false&&distance < 2))
                m_Owner.ChangeFSM(DummyFSM.State.Walk);
            else if (m_Owner.isDealy == true && m_Owner.istargetMove == true)
            {
                if (theta <= 7)
                {
                    m_Owner.istargetMove = false;
                    m_Owner.ChangeFSM(DummyFSM.State.Walk);
                }
                else
                {
                    m_Owner.ChangeFSM(DummyFSM.State.Attack);
                }
               
            }
               
        }
           
        //else
        //    m_Owner.m_TransTarget = null;
    }
    public override void Exit()
    {
        //Debug.Log("idle Exit");
 
        m_Owner.m_ePrevState = DummyFSM.State.IDLE;
    }


    private bool FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange, (1 << LayerMask.NameToLayer("Player")));
        if(hitColliders.Length != 0 || m_Owner.m_TransTarget != null)
        {
            for (int i = 0; i < hitColliders.Length; i++)
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


}
