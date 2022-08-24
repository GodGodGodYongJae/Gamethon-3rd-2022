using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon_Attack2 : MonoBehaviour
{

    Vector3 TargetPos;
    Vector3 BulletCreatePos;
    // Start is called before the first frame update
    [Tooltip("공격시간")]
    [SerializeField] float attackTime;
    DummyFSM m_Owner;
    int attackPoint;
    private float AttackWaittingTime;
    private bool isRun;
    private NavMeshAgent agent;
    public void Run(DummyFSM m_Owner, int attackPoint)
    {
        agent = m_Owner.agent;
        this.m_Owner = m_Owner;
        this.attackPoint = attackPoint;
        isRun = true;
    }

    void Update()
    {
        if (isRun)
        {
            Actions();
        }
    }

    short bulletCount = 0;

    void Actions()
    {

         
        Vector3 targetDir = (m_Owner.m_TransTarget.position - m_Owner.transform.position).normalized;
        float dot = Vector3.Dot(m_Owner.transform.forward, targetDir);
        //float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (m_Owner.m_TransTarget != null && dot <= 1)
        {
            agent.updateRotation = false;
            Vector3 lookPos = m_Owner.m_TransTarget.position - m_Owner.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            m_Owner.transform.rotation = Quaternion.Slerp(m_Owner.transform.rotation, rotation, Time.deltaTime*2f);
            
        }

        AttackWaittingTime += Time.deltaTime;

        if (AttackWaittingTime > attackTime && bulletCount.Equals(0))
        {
            AttackWaittingTime = 0;
            StartCoroutine(CreateBullet());

        }
    }

    IEnumerator CreateBullet()
    {
        TargetPos = m_Owner.m_TransTarget.position;
        BulletCreatePos = new Vector3(transform.position.x, TargetPos.y + 0.3f, transform.position.z);
        bulletCount++;
        if(bulletCount < 4)
        {
            m_Owner.m_Animator.Play("Basic Attack", -1,0f);
            m_Owner.m_Animator.SetInteger("attack", 2);
            yield return new WaitForSeconds(1.5f);
            for (int i = -2; i < 3; i++)
            {
                GameObject bullet;
                Quaternion rot = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (i*10), transform.eulerAngles.z));
                bullet = ObjectPoolManager.Instance.Get("FireBullet", BulletCreatePos, rot);
                bullet.GetComponent<ParticleCollisionInstance>().Damage = m_Owner.enemy.Type.AttackPoint;
            }
            
            StartCoroutine(CreateBullet());
        }
        else
        {
             bulletCount = 0;
            m_Owner.m_Animator.SetInteger("attack", 0);
            m_Owner.isDealy = false;
            m_Owner.istargetMove = false;
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
            isRun = false;
            yield return null;
        }
    }
}
