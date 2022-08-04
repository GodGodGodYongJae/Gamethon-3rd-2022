using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_ReadyAttack : MonoBehaviour, FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;
    private bool isSpawn;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    public void Begin()
    {
        //Bullet ���� ��ȯ�غ��� �ؼ�
        isSpawn = true;
        m_Owner.m_eCurState = DummyFSM.State.Attack;
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    public void Run()
    {
        //Debug.Log(m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if(m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (isSpawn.Equals(false))
            {
                isSpawn = true;
                m_Owner.ChangeFSM(DummyFSM.State.Summon);
            }
            else
            {
                m_Owner.ChangeFSM(DummyFSM.State.Attack2, false);
            }
        }

    }


}
