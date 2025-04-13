using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/********************************************** NOTE ***********************************************

UnityEngine.InputSystem doesn't seem to be necessary

The official documentation that uses things like "InputSystems_Actions.Player.TouchPoint.performed" is copyright 2023
    Source: https://docs.unity3d.com/Packages/com.unity.inputsystem@0.2/api/UnityEngine.InputSystem.InputActionPhase.html#top

The official documentation that completely bypasses this is copyright 2025, so this is more likely up to date
    Source: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/TouchPhase.html

I may be incorrect on this, but I believe that since this is the case, it is unnecessary to manually subscribe / unsubscribe to events associated with it
It's completely possible that this script includes things that are unnecessary or is missing elements, I just know that it seems to work for the most part

***************************************************************************************************/

public class InputHandlerV2 : MonoBehaviour
{
    public UnityEvent TouchBegan; // nothing is listening to these atm, may hook into later
    public UnityEvent TouchMoved;
    public UnityEvent TouchReleased;

    public Vector2 StartPos { get; private set; }
    public Vector2 TouchPos { get; private set; }
    public bool IsBeingTouched { get; private set; }
    public Touch Touch { get; private set; }

    private GameHUDController _gameHUDController;
    private UIDocument _document;

    private bool _blockInputs = false;

    private void Awake()
    {
        _gameHUDController = FindAnyObjectByType<GameHUDController>();
        _document = FindAnyObjectByType<UIDocument>();
    }

    private void OnEnable()
    {
        _gameHUDController.UIInteracted.AddListener(OnUIInteract);
    }

    private void OnDisable()
    {
        _gameHUDController.UIInteracted.RemoveListener(OnUIInteract);
    }

    private void Update()
    {
        UpdateTouch();
        // Debug.Log(StartPos + "  " + TouchPos);
    }

    private void UpdateTouch()
    {
        if (Input.touchCount > 0)
        {
            IsBeingTouched = true;
            Touch = Input.GetTouch(0);            // simultaneous touches may require a foreach statement, I haven't tested it

            switch (Touch.phase)                        // for each touchpoint, Unity stores an enum that describes its state
            {
                case TouchPhase.Began:                  // fires first frame touch is detected
                    if (_blockInputs) break;
                    StartPos = Touch.position;          // update public position variables
                    TouchPos = Touch.position;
                    break;

                case TouchPhase.Moved:
                    if (_blockInputs) break;
                    TouchPos = Touch.position;
                    break;

                case TouchPhase.Ended:                  // fires the frame touch is released
                    if (_blockInputs)
                    {
                        _blockInputs = false;
                        break;
                    }
                    StartPos = Vector2.zero;            // reset touch position (not necessary but keeps things clean)
                    TouchPos = Vector2.zero;            
                    break;
            }
        }
        else
        {
            IsBeingTouched = false;
        }
    }

    private void OnUIInteract(string buttonName)
    {
        _blockInputs = true;
    }
}
