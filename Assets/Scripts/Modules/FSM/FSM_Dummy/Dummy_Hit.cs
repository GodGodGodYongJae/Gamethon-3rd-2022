using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Hit : MonoBehaviour,FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;

    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }
    public  void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Hit;
    }

    public  void Exit()
    {
        m_Owner.m_Animator.SetBool("isAnimHitEnd", false);
        m_Owner.m_ePrevState = DummyFSM.State.Hit;
    }

    public  void Run()
    {
        if (m_Owner.m_Animator.GetBool("isAnimHitEnd"))
        {
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
    }


}
