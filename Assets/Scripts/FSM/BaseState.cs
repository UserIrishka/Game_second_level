using System;
using UnityEngine;

public abstract class BaseState <EState> where EState : Enum
{
    public EState StateKey { get; private set; }
    public event Action <EState> OnStateChanged;
    protected bool IsTransitionStarted;
    protected BaseState(EState estate)
    {
        StateKey = estate;
    }

    protected void ChangeState(EState estate)
    {
        OnStateChanged?.Invoke(estate);
    }

    public abstract void EnterToState();
    public abstract void ExitToState();
    public virtual void UpdateState()
    {

    }

}
