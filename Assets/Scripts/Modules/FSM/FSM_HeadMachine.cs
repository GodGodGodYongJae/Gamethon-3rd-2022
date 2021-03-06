using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_HeadMachine<T>
{
    private T Owner;

    private FSM_State<T> m_CurState = null;
    private FSM_State<T> m_PrevState = null;

    public void Begin()
    {
        if (m_CurState != null)
            m_CurState.Begin();
    }

    public void Run()
    {
        CheckState();
    }

    public void CheckState()
    {
        if(m_CurState != null)
        {
            m_CurState.Run();
        }
    }

    public void Exit()
    {
        m_CurState.Exit();
        m_CurState = null;
        m_PrevState = null;
    }

    public void Change(FSM_State<T> _state)
    {
        if (_state == m_CurState)
            return;
        m_PrevState = m_CurState;

        if (m_CurState != null)
            m_CurState.Exit();

        m_CurState = _state;

        if (m_CurState != null)
            m_CurState.Begin();
    }

    public void Revert()
    {
        if (m_PrevState != null)
            Change(m_PrevState);
    }

    public void SetState(FSM_State<T> _state,  T _Owner)
    {
        Owner = _Owner;
        m_CurState = _state;

        if (m_CurState != _state && m_CurState != null)
            m_PrevState = m_CurState;
    }
}
