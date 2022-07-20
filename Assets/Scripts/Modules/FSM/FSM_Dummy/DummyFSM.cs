using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyFSM : MonoBehaviour
{

    public enum State { IDLE,Walk,Back,Die,Attack,Damage,End}
    public FSM_HeadMachine<DummyFSM> m_state;
    public FSM_State<DummyFSM>[] m_arrState = new FSM_State<DummyFSM>[(int)State.End];

    public Transform m_TransTarget;


    public State m_eCurState;
    public State m_ePrevState;

    public float m_fFindRange;
    public float m_fAttackRange;
    public Animator m_Animator;

    public GameObject indicator;
    public RangeCircle indicatorRangeCircle;

    public DummyFSM()
    {
        init();
    }

    public void init()
    {
        m_state = new FSM_HeadMachine<DummyFSM>();
        m_arrState[(int)State.IDLE] = new Dummy_Idle(this);
        m_arrState[(int)State.Walk] = new Dummy_Walk(this);
        m_arrState[(int)State.Attack] = new Dummy_Attack(this);

        m_state.SetState(m_arrState[(int)State.IDLE], this);
    }

    public void ChangeFSM(State st)
    {
        for (int i = 0; i < (int)State.End; ++i)
        {
            if (i == (int)st)
                m_state.Change(m_arrState[(int)st]);
        }
    }

    public void Begin()
    {
        m_state.Begin();
    }
    public void Run()
    {
        m_state.Run();
    }
    public void Exit()
    {
        m_state.Exit();
    }
    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        Begin();
        indicatorRangeCircle = new RangeCircle(this.transform, 41.5f, 1.04f);
    }
    private void Update()
    {
        Run();


    }
}
