using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyFSM : MonoBehaviour
{

    public enum State { IDLE,Walk,empty,Death,Attack,Hit,Summon, Attack2,End}


    public FSM_HeadMachine<DummyFSM> m_state;
    public FSM_State<DummyFSM>[] m_arrState = new FSM_State<DummyFSM>[(int)State.End];

    public Transform m_TransTarget;

    public float AttackWaitTime;
    public State m_eCurState;
    public State m_ePrevState;

    public float m_fFindRange;
    public float m_fAttackRange;

    public float attackDleay;
    private float currentAtkDealy;

    public bool isDealy;
    public bool istargetMove;
    public Animator m_Animator;

    public GameObject indicator;
    public RangeCircle indicatorRangeCircle;

    public float attackRange;
    public float attackDistance;
    public Enemy enemy;

    public NavMeshAgent agent;

    public Transform backpos;


    [System.Serializable]
    public class FSMLIST : SerializableDictionary<State, MonoBehaviour> { };

    public FSMLIST e_FsmList = new FSMLIST();
    //public DummyFSM()
    //{
    //    init();
    //}

    public void init()
    {
        m_state = new FSM_HeadMachine<DummyFSM>();
        foreach (var item in e_FsmList)
        {
            FSM_State<DummyFSM> fSM_State = item.Value as FSM_State<DummyFSM>;
            fSM_State.Owner = this;
            m_arrState[(int)item.Key] = fSM_State;
            //m_arrState[(int)item.Key].Owner = this;
        }
        //m_arrState[(int)State.IDLE] = new Dummy_Idle(this);
        //m_arrState[(int)State.Walk] = new Dummy_Walk(this);
        //m_arrState[(int)State.Hit] = new Dummy_Hit(this);
        //m_arrState[(int)State.Death] = new Dummy_Death(this);
        //m_arrState[(int)State.Attack] = new Dummy_Attack(this);

        m_state.SetState(m_arrState[(int)State.IDLE], this);
    }

    public void ChangeFSM(State st, bool ChangeState = true)
    {

        //Debug.Log(st);
        for (int i = 0; i < (int)State.End ; ++i)
        {
            if (i == (int)st)
            {
               // Debug.Log(m_arrState[(int)st]);
                m_state.Change(m_arrState[(int)st]);
                if(ChangeState.Equals(true))
                m_Animator.SetInteger("State", (int)st);
            }
                
        }
      
    }


    private void currentAttackDealy()
    {
        currentAtkDealy += Time.deltaTime;
        if (currentAtkDealy > attackDleay && isDealy == false)
        {
            isDealy = true;
            currentAtkDealy = 0;
        }
    }

    public void OnGetDamageEvent(int Health,int maxHelath)
    {
       
        if (Health <= 0)
            ChangeFSM(State.Death);
        else
        {
            // 공격 중이 아니거나 공격 쿨타임이 다찼을 땐 피격 모션 안뜸.
            if(m_eCurState != State.Attack && !isDealy)
            {

                ChangeFSM(State.Hit);
            }
        }
    }

    public void Begin()
    {
        m_state.Begin();
    }

    public void Run()
    {
        if (enemy.isDeath)
           ChangeFSM(DummyFSM.State.Death);

        if (m_TransTarget == null || m_TransTarget.GetComponent<Player>().isDeath == false)
        m_state.Run();
        if(!isDealy)
        currentAttackDealy();
    }
    public void Exit()
    {
        m_state.Exit();
    }

    private void mappingStatus()
    {
        enemy = GetComponent<Enemy>();
        //AttackWaitTime = e.Type.AttackDelay;
        m_fFindRange = enemy.Type.FindRange;
        m_fAttackRange = enemy.Type.AttackFindRange;
        attackDleay = enemy.Type.AttackDelay;
        attackRange = enemy.Type.AttackRange;
        attackDistance = enemy.Type.AttackDistance;
        agent.speed = enemy.Type.MoveSpeed;
        agent.stoppingDistance = enemy.Type.StopDistance;

    }
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        m_Animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        mappingStatus();
        enemy = GetComponent<Enemy>(); 
        indicatorRangeCircle = new RangeCircle(this.transform, attackRange, attackDistance);
        init();
    }
    private void Update()
    {
        Run();
    }
}
