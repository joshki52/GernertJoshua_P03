using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 32;
    [SerializeField] private float _maxTraversalDistance = 8;
    [SerializeField] private float _inputMult = 1;

    public bool TurretIsControlled = true;

    private Rigidbody _rigidbody;
    private InputHandlerV2 _input;
    private GameController _gameController;

    private float _xGoal;
    private float _touchPosPercent;
    private Vector2 _differ;

    private void Awake()
    {
        _rigidbody      = GetComponent<Rigidbody>();
        _gameController = FindAnyObjectByType<GameController>();
        _input          = FindAnyObjectByType<InputHandlerV2>();
    }

    private void Update()
    {
        _differ = _input.TouchPos - _input.StartPos;
        float percentGoal = ((_differ.x / Screen.width) * _inputMult) + 0.5f;

        // return to center if input is disabled
        if (TurretIsControlled == false) _xGoal = 0; // return turret to center when control disabled

        else if (_input.IsBeingTouched)
        {
            // calculate where turret should move to
            //_touchPosPercent = _input.TouchPos.x / Screen.width;
            _xGoal = Mathf.Lerp(-_maxTraversalDistance, _maxTraversalDistance, percentGoal); 
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null || _differ == Vector2.zero) return;

        // xPos is a temp x value, used for doing math before updating rigidbody
        // xPos converted to Vector3 (offsetPos) for use in scene

        float xPos = transform.position.x;
        if      (Mathf.Abs(_xGoal - xPos) < _moveSpeed * Time.fixedDeltaTime) { xPos = _xGoal; }
        else if (xPos < _xGoal) { xPos += _moveSpeed * Time.fixedDeltaTime; }
        else if (xPos > _xGoal) { xPos -= _moveSpeed * Time.fixedDeltaTime; }

        xPos = Mathf.Clamp(xPos, -_maxTraversalDistance, _maxTraversalDistance); // restrict xPos to range
        Vector3 offsetPos = new Vector3(xPos, transform.position.y, transform.position.z);
        _rigidbody.MovePosition(offsetPos);
    }
}
