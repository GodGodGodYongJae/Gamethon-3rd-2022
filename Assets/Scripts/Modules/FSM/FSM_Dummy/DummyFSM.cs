using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyFSM : MonoBehaviour
{

    public enum State { IDLE,Walk,Back,Death,Attack,Hit,End}
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
    private Enemy enemy;
    public bool isDebug;

    private NavMeshAgent agent; 
    public DummyFSM()
    {
        init();
    }

    public void init()
    {
        m_state = new FSM_HeadMachine<DummyFSM>();
        m_arrState[(int)State.IDLE] = new Dummy_Idle(this);
        m_arrState[(int)State.Walk] = new Dummy_Walk(this);
        m_arrState[(int)State.Hit] = new Dummy_Hit(this);
        m_arrState[(int)State.Death] = new Dummy_Death(this);
        m_arrState[(int)State.Attack] = new Dummy_Attack(this);

        m_state.SetState(m_arrState[(int)State.IDLE], this);
    }

    public void ChangeFSM(State st)
    {
        for (int i = 0; i < (int)State.End; ++i)
        {
            if (i == (int)st)
            {
                m_state.Change(m_arrState[(int)st]);
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

    public void OnGetDamageEvent(int Health)
    {
       
        if (Health <= 0)
            ChangeFSM(State.Death);
        else
        {
            // ���� ���� �ƴϰų� ���� ��Ÿ���� ��á�� �� �ǰ� ��� �ȶ�.
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
        if(m_TransTarget == null || m_TransTarget.GetComponent<Player>().isDeath == false)
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
        Enemy e = GetComponent<Enemy>();
        //AttackWaitTime = e.Type.AttackDelay;
        m_fFindRange = e.Type.FindRange;
        m_fAttackRange = e.Type.AttackFindRange;
        attackDleay = e.Type.AttackDelay;
        attackRange = e.Type.AttackRange;
        attackDistance = e.Type.AttackDistance;
        agent.speed = e.Type.MoveSpeed;
        agent.stoppingDistance = e.Type.StopDistance;

    }
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        m_Animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        if (!isDebug)
            mappingStatus();

        Begin();
        enemy = GetComponent<Enemy>(); 
        indicatorRangeCircle = new RangeCircle(this.transform, attackRange, attackDistance);
    }
    private void Update()
    {
        Run();
    }
}
