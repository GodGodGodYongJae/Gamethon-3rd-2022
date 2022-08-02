using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Death : MonoBehaviour,FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;
    bool isDeath;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }
    public  void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Death;
        isDeath = true;
    }

    public  void Exit()
    {
        //m_Owner.m_Animator.SetBool("isAnimDeathEnd", false);
        m_Owner.m_ePrevState = DummyFSM.State.Death;
    }

    public  void Run()
    {
        if(isDeath)
        {
            EnemyFactoryMethod.Instance?.DeleteEnemy(m_Owner.gameObject);
            isDeath = false;
        }
        
       
    }

    //bool EndAnimationDone()
    //{
    //    return m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
    //        m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f;
    //}
}
