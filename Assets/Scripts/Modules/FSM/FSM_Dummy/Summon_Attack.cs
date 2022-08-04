using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Attack : MonoBehaviour, FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    public void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Summon;
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Summon;
    }

    public void Run()
    {
        m_Owner.ChangeFSM(DummyFSM.State.IDLE);
    }


}
