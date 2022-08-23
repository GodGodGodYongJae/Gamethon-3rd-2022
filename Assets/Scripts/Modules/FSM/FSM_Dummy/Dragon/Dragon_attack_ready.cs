using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_attack_ready : MonoBehaviour, FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;

    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    public void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Attack;
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    public void Run()
    {
        float dist = Vector3.Distance(m_Owner.m_TransTarget.position, transform.position);
        print(dist);
        m_Owner.ChangeFSM(DummyFSM.State.IDLE);
    }

    
}
