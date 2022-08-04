using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Attack : MonoBehaviour, FSM_State<DummyFSM>
{
    private DummyFSM m_Owner;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    private float timer;
    private float closeDistance = 0.8f;
    Vector3 TargetPos;
    Vector3 BulletCreatePos;

    private GameObject bullet;
    public void Begin()
    {
        //m_Owner.m_Animator.SetInteger("State", (int)DummyFSM.State.Attack);
        m_Owner.m_eCurState = DummyFSM.State.Attack2;
        TargetPos = m_Owner.m_TransTarget.position;
        BulletCreatePos = new Vector3(transform.position.x, TargetPos.y, transform.position.z);
        //BulletCreatePos += transform.forward;
        //BulletCreatePos += transform.forward;
        CreateBullet();
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Attack2;
    }

    public void Run()
    {
        //https://blog.naver.com/yoohee2018/221159368623
        //Vector3 offset = bullet.transform.position - TargetPos;
        //float sqrLen = offset.sqrMagnitude;
        //Debug.Log(sqrLen + "," + closeDistance * closeDistance + "," + Mathf.Sqrt(closeDistance));

        if(bullet.activeSelf.Equals(false))
        {
            m_Owner.isDealy = false;
            m_Owner.istargetMove = false;
            m_Owner.ChangeFSM(DummyFSM.State.IDLE);
        }
        // indicator Create.

    }

    private void CreateBullet()
    {
        bullet = ObjectPoolManager.Instance.Get("FireBullet", BulletCreatePos, transform.rotation);
    }
 
}
