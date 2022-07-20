using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Hit : FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;

    public Dummy_Hit(DummyFSM _owner)
    {
        m_Owner = _owner;
    }
    public override void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Hit;
    }

    public override void Exit()
    {
        m_Owner.m_Animator.SetBool("isAnimHitEnd", false);
        m_Owner.m_ePrevState = DummyFSM.State.Hit;
    }

    public override void Run()
    {
        if (m_Owner.m_Animator.GetBool("isAnimHitEnd"))
        {
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
    }


}
