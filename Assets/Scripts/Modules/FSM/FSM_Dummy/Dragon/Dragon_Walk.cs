using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Dragon_Walk : MonoBehaviour, FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    private NavMeshAgent agent;
    Vector3 point;

    [SerializeField]
    private float dotDis = 0.99f;
    //private float currentAtkDealy;
    //private bool isDealy;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }
    public void Begin()
    {
        //Debug.Log("Walk Begin");
        point = Vector3.zero;
        agent = m_Owner.agent;
        agent.isStopped = false;
        m_Owner.m_eCurState = DummyFSM.State.Walk;

    }

    public void Run()
    {
        if (m_Owner.m_TransTarget == null)
        {
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
        Vector3 targetDir = (m_Owner.m_TransTarget.position - m_Owner.transform.position).normalized;
        float dot = Vector3.Dot(m_Owner.transform.forward, targetDir);
        //float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (m_Owner.m_TransTarget != null && dot <= 1)
        {
            agent.updateRotation = false;
            Vector3 lookPos = m_Owner.m_TransTarget.position - m_Owner.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            m_Owner.transform.rotation = Quaternion.Slerp(m_Owner.transform.rotation, rotation, Time.deltaTime * 2f);

        }
        GotoTarget();
        //Debug.Log(Vector3.Distance(m_Owner.transform.position, m_Owner.m_TransTarget.position));
        //Debug.Log(dot);
        if (m_Owner.m_TransTarget != null )
        {
            if (m_Owner.isDealy)
            {
                m_Owner.ChangeFSM(DummyFSM.State.Attack);
                //Debug.Log("ooo" + dot);
            }

            else
            {
                // m_Owner.ChangeFSM(DummyFSM.State.IDLE);
                m_Owner.istargetMove = true;
            }

        }
        //Debug.Log(dot + "," + theta);
    }

    public void Exit()
    {
        //Debug.Log("WAlk EXit");
        agent.isStopped = true;
        m_Owner.m_ePrevState = DummyFSM.State.Walk;
    }


    private bool FindRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Owner.transform.position, m_Owner.m_fFindRange, (1 << LayerMask.NameToLayer("Player")));
        if (hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; ++i)
            {
                if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_Owner.m_TransTarget = hitColliders[i].transform;
                }
            }
            if (m_Owner.m_TransTarget == null)
                m_Owner.m_TransTarget = hitColliders[0].transform;
            return true;
        }
        return false;
    }

    private void GotoTarget()
    {
        float distance = Vector3.Distance(m_Owner.m_TransTarget.position, m_Owner.transform.position);
        NavMeshPath path = new NavMeshPath();

        if (m_Owner.isDealy.Equals(false) && distance < 1)
        {
            point = m_Owner.backpos.position;
        }
        else if (m_Owner.isDealy.Equals(true))
        {
            point = m_Owner.m_TransTarget.position;
        }
        //if(agent.remainingDistance < 1)
        //{
        //    m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        //}
        agent.ResetPath();
        agent.CalculatePath(point, path);
        agent.SetPath(path);
        //Debug.Log(agent.pathEndPosition);
        //https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=gudska4237&logNo=221454275833 응답지연문제 

    }

}
