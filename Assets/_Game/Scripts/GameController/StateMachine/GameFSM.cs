using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;

    public GameSetupState SetupState { get; private set; }
    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<GameController>();
        // instantiate here
        SetupState = new GameSetupState(this, _controller);
        GameplayState = new GameplayState(this, _controller);
        WinState = new WinState(this, _controller);
        LoseState = new LoseState(this, _controller);
    }

    private void Start()
    {
        ChangeState(SetupState);
    }
}
