using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Attack : MonoBehaviour, FSM_State<DummyFSM>
{

    private DummyFSM m_Owner;
    public DummyFSM Owner { get { return m_Owner; } set { m_Owner = value; } }

    Wizard_ReadyAttack wr;

    private void Start()
    {
        wr = GetComponent<Wizard_ReadyAttack>();
    }

    public void Begin()
    {
        m_Owner.m_eCurState = DummyFSM.State.Summon;

        Vector3 SpawnPoint = transform.right * 1f;
        GameObject obj = EnemyFactoryMethod.Instance.CreateEnemy("StoneMonster", SpawnPoint, transform.rotation,true);
        wr.Summon.Add(obj);

        SpawnPoint = transform.right * -1f;
        obj = EnemyFactoryMethod.Instance.CreateEnemy("StoneMonster", SpawnPoint, transform.rotation,true);
        wr.Summon.Add(obj);

        m_Owner.isDealy = false;
        m_Owner.istargetMove = false;
        m_Owner.ChangeFSM(DummyFSM.State.IDLE);
    }

    public void Exit()
    {
        m_Owner.m_ePrevState = DummyFSM.State.Summon;
    }

    public void Run()
    {
        //m_Owner.ChangeFSM(DummyFSM.State.IDLE);
    }


}
