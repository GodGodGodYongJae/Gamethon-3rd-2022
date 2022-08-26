using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_AttackUpDateVer : MonoBehaviour, FSM_State<DummyFSM>
{
    [HideInInspector]
    public DummyFSM m_Owner;
    //private float AttackWaitTime;
    private float AttackWaittingTime;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }
    [SerializeField]
    [Tooltip("범위표시 오브젝트")]
    GameObject indicator;
    [SerializeField]
    [Tooltip("실제 공격범위 표시기")]
    CircleSector indicatorRange;
    [Tooltip("공격시간 애니메이션 싱크 맞춰주기 위함")]
    [SerializeField] float attackTime;



    public void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Attack;
        AttackWaittingTime = 0;
        indicatorRange.target = m_Owner.m_TransTarget;
    }

    public void Run()
    {
        indicator.SetActive(true);
        AttackWaittingTime += Time.deltaTime;
        if (AttackWaittingTime > attackTime)
        {
            AttackWaittingTime = 0;
            indicator.SetActive(false);
            if (indicatorRange.isCollisition())
            {
                IDamageable damage = m_Owner.m_TransTarget.GetComponent<IDamageable>();
                damage.Damage(m_Owner.enemy.Type.AttackPoint, gameObject);
            }
            indicator.SetActive(false);
            m_Owner.isDealy = false;
            m_Owner.istargetMove = false;
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }
}
