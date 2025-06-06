using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class State
{
    public float StateDuration { get; private set; } = 0;

    // run on enter state
     public virtual void Enter()
    {
        PrintStateName();
        StateDuration = 0;
    }

    // run on exit state
    public virtual void Exit()
    {

    }

    // for Physics
    public virtual void FixedTick()
    {

    }

    // for Update
    public virtual void Tick()
    {
        StateDuration += Time.deltaTime;
    }

    // prints state (used on enter)
    public virtual void PrintStateName()
    {
        string stateName = "STATE: " + this.GetType().ToString();
        Debug.Log(stateName);
    }
}