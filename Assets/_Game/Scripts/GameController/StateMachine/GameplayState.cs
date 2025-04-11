using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    // constructor, called when this state is created
    public GameplayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
    }
}