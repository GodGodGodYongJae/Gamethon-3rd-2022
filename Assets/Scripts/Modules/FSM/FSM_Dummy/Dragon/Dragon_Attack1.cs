using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Attack1 : MonoBehaviour
{

    [SerializeField]
    GameObject indicator;
    [Tooltip("공격범위")]
    [SerializeField] float Range;
    [Tooltip("공격거리")]
    [SerializeField] float distance;
    [Tooltip("공격시간")]
    [SerializeField] float attackTime;

    [SerializeField]
    CircleSector indicatorRange;
    // Start is called before the first frame update
    private float AttackWaittingTime;

    private bool isRun;
     void Start()
    {
       
        //indicatorRange = new RangeCircle(transform, Range, distance);
    }

    public void Run(DummyFSM m_Owner,int attackPoint)
    {
        this.m_Owner = m_Owner;
        this.attackPoint = attackPoint;
        indicatorRange.target = m_Owner.m_TransTarget;
        indicatorRange.angleRange = Range;
        indicatorRange.distance = distance;
        isRun = true;


    }

    void Update()
    {
        if(isRun)
        {
            Actions();
        }
    }

    DummyFSM m_Owner;
    int attackPoint;
    void Actions()
    {
       
        m_Owner.m_Animator.SetInteger("attack", 1);
        indicator.SetActive(true);
        AttackWaittingTime += Time.deltaTime;
        if (AttackWaittingTime > attackTime)
        {
            AttackWaittingTime = 0;
            indicator.SetActive(false);
            if (indicatorRange.isCollisition())
            {
                IDamageable damage = m_Owner.m_TransTarget.GetComponent<IDamageable>();
                damage.Damage(attackPoint, gameObject);
            }

            indicator.SetActive(false);
            m_Owner.m_Animator.SetInteger("attack", 0);
            m_Owner.isDealy = false;
            m_Owner.istargetMove = false;
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
            isRun = false;
        }
    }
}
