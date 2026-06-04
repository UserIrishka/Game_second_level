using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public abstract class StateInspector <EState> where EState : Enum 
{
    protected readonly Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurentState;
    private bool _isTransitionOnProcess;

    protected void TransitionToNextState(EState nextState)
    {
        _isTransitionOnProcess = true;
        CurentState?.ExitToState();
        CurentState.OnStateChanged -= TransitionToNextState;
        CurentState = States[nextState];
        CurentState.OnStateChanged += TransitionToNextState;
        CurentState.EnterToState();
        _isTransitionOnProcess = false;
    }

    protected void StartStateMachine(EState startState)
    {
        if (CurentState != null)
        {
            CurentState.OnStateChanged -= TransitionToNextState;
        }
        CurentState = States[startState];
        CurentState.OnStateChanged += TransitionToNextState;
        CurentState.EnterToState();
    }

    public virtual void Update()
    {
        if (_isTransitionOnProcess == false)
        {
            CurentState.UpdateState();
        }
    }

}
