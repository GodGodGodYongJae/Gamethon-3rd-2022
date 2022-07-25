using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Death : FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;
    public Dummy_Death(DummyFSM _owner)
    {
        m_Owner = _owner;
    }
    public override void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Death;
    }

    public override void Exit()
    {
        //m_Owner.m_Animator.SetBool("isAnimDeathEnd", false);
        m_Owner.m_ePrevState = DummyFSM.State.Death;
    }

    public override void Run()
    {
        EnemyFactoryMethod.Instance?.DeleteEnemy(m_Owner.gameObject);
       
    }

    //bool EndAnimationDone()
    //{
    //    return m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
    //        m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f;
    //}
}