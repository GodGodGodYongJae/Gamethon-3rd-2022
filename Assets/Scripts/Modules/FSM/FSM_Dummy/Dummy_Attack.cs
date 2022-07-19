using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Attack : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;

    public Dummy_Attack(DummyFSM _owner)
    {
        m_Owner = _owner;
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
        throw new System.NotImplementedException();
    }

}
