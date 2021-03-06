using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushAttack_Attack : MonoBehaviour,FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    public float AttackWaittingTime;
    private float timer;
    private NavMeshAgent agent;
    Vector3 point;
    private bool StartColision;
    private bool isPoint;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    public void Begin()
    {
        timer = 0;
        agent = m_Owner.agent;
        m_Owner.m_eCurState = DummyFSM.State.Attack;
        StartColision = true;
        isPoint = true;
    }

    public void Exit()
    {
        m_Owner.indicator.SetActive(false);
        m_Owner.m_ePrevState = DummyFSM.State.Attack;
    }

    public void Run()
    {
      
        
        timer += Time.deltaTime;
        if (timer > AttackWaittingTime)
        {
            if (isPoint) Point();
            bool isEnd = Move();
            if(isEnd.Equals(false))
            {
                m_Owner.isDealy = false;
                m_Owner.istargetMove = false;
                Debug.Log("????");
                m_Owner.ChangeFSM(DummyFSM.State.IDLE);

            }
        }

    }
    private void Point()
    {
        point = m_Owner.m_TransTarget.position;//m_Owner.transform.forward * 3f;
        Debug.Log(point);
        isPoint = false;
    }
    private bool Move()
    {
       
        NavMeshPath path = new NavMeshPath();
        if (agent.remainingDistance < 1)
        {
            return false;
        }
        agent.ResetPath();
        agent.CalculatePath(point, path);
        agent.SetPath(path);
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && StartColision)
        {
            StartColision = false;
            IDamageable damage = collision.transform.GetComponent<IDamageable>();
            damage.Damage(m_Owner.enemy.Type.AttackPoint, m_Owner.gameObject);
        }
    }


 
}
