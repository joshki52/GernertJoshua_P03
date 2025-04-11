using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions _inputSystemActions;
    public event Action<Vector2> TouchStarted;
    public event Action<Vector2> TouchEnded;
    public Vector2 TouchStartPosition { get; private set; }
    public Vector2 TouchCurrentPosition { get; private set; }
    public bool TouchHeld { get; private set; }

    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.TouchPoint.performed += OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled += OnTouchCancelled;
    }

    private void OnDisable()
    {
        _inputSystemActions.Player.TouchPoint.performed -= OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled -= OnTouchCancelled;
        _inputSystemActions.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch");
        TouchHeld = true;                                       // change public bool
        Vector2 TouchPosition = context.ReadValue<Vector2>();   // read position from our input action
        TouchStartPosition = TouchPosition;                     // save start position
        TouchCurrentPosition = TouchPosition;                   // update current position (now start position)
        TouchStarted?.Invoke(TouchPosition);                    // send event notification for listeners
        //Debug.Log("TouchStartPos: " + TouchStartPosition);
    }

    private void OnTouchCancelled(InputAction.CallbackContext context)
    {
        //Debug.Log("Release");
        TouchHeld = false;                          // revert public bool
        TouchEnded?.Invoke(TouchCurrentPosition);   // send notification for listeners of last known position
        //Debug.Log("Touch End Pos: " + TouchCurrentPosition);
        // clear out touch positions when there's no input
        TouchStartPosition = Vector2.zero;
        TouchCurrentPosition = Vector2.zero;
    }

    private void Update()
    {
        if (TouchHeld)
        {
            TouchCurrentPosition = _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();
        }
    }
}
