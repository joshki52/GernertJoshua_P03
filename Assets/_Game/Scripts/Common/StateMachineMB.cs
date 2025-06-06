using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMB : MonoBehaviour
{
    public State CurrentState { get; private set; }
    private State _previousState;

    private bool _inTransition = false;

    public void ChangeState(State newState)
    {
        // ensure ready for new state
        if (CurrentState == newState || _inTransition) return;
        ChangeStateSequence(newState);
    }

    private void ChangeStateSequence(State newState)
    {
        _inTransition = true;
        // run exit sequence before changing state
        CurrentState?.Exit();
        StoreStateAsPrevious(newState);

        CurrentState = newState;

        // begin enter state
        CurrentState?.Enter();
        _inTransition = false;
    }

    private void StoreStateAsPrevious(State newState)
    {
        // if no previous state, this is first
        if (_previousState == null && newState != null)
            _previousState = newState;
        // otherwise, store state as previous
        else if (_previousState != null && CurrentState != null)
            _previousState = CurrentState;
    }

    public void ChangeStateToPrevious()
    {
        if (_previousState != null)
            ChangeState(_previousState);
        else
        {
            Debug.LogWarning("No previous state to change to");
        }
    }

    protected virtual void Update()
    {
        // simulate update ticks in states
        if (CurrentState != null && !_inTransition)
            CurrentState.Tick();
    }

    protected virtual void FixedUpdate()
    {
        // simulate FixedUpdate in states
        if (CurrentState != null && !_inTransition)
            CurrentState.FixedTick();
    }

    protected virtual void OnDestroy()
    {
        CurrentState?.Exit();
    }
}
