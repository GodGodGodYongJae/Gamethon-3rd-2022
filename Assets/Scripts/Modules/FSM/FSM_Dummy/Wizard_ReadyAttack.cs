using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_ReadyAttack : MonoBehaviour, FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;
    public List<GameObject> Summon = new List<GameObject>();
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    private short coolDownSummon;
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
        for (int i = 0; i < Summon.Count; i++)
        {
            if(Summon[i].GetComponent<Enemy>().isDeath)
            {
                Summon.RemoveAt(i);
            }
            //if(item.GetComponent<Enemy>().isDeath.Equals(true))
            //{
            //    int idx = Summon.IndexOf(item);
            //    Summon.RemoveAt(idx);
            //}
        }


        //Debug.Log(m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if(m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            m_Owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (Summon.Count.Equals(0))
            {
                if(coolDownSummon.Equals(0))
                {
                    coolDownSummon = 2;
                    m_Owner.ChangeFSM(DummyFSM.State.Summon, false);
                }
                  
                else
                {
                    coolDownSummon--;
                    m_Owner.ChangeFSM(DummyFSM.State.Attack2, false);
                }
            }
            else
            {
                m_Owner.ChangeFSM(DummyFSM.State.Attack2, false);
            }
        }

    }


}
