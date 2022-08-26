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
    [Tooltip("����ǥ�� ������Ʈ")]
    GameObject indicator;
    [SerializeField]
    [Tooltip("���� ���ݹ��� ǥ�ñ�")]
    CircleSector indicatorRange;
    [Tooltip("���ݽð� �ִϸ��̼� ��ũ �����ֱ� ����")]
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
