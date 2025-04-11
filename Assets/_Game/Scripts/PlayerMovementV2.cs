using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovementV2 : MonoBehaviour
{
    [SerializeField] private float _curSpeed    = 0;
    [SerializeField] private float _maxSpeed    = 32;
    [SerializeField] private float _maxTravDist = 8;

    public bool TurretIsControlled = true;

    private Rigidbody           _rigidbody;
    private GameController      _gameController;
    private GameHUDController   _gameHUDController;

    private float _xPos;
    private float _xGoal;

    private void Awake()
    {
        _rigidbody          = GetComponent<Rigidbody>();
        _gameController     = FindAnyObjectByType<GameController>();
        _gameHUDController  = FindAnyObjectByType<GameHUDController>();
    }

    private void Update()
    {
        //float sliderVal = _gameHUDController.TurretSliderValue;
        //_curSpeed = Mathf.Lerp(-_maxSpeed, _maxSpeed, sliderVal);

        // calculate where turret should move to
        //_xGoal = Mathf.Lerp(-_maxTravDist, _maxTravDist, _gameHUDController.TurretSliderValue);

        // update the intended position & clamp
        _xPos += _curSpeed * Time.deltaTime;
        _xPos = Mathf.Clamp(_xPos, -_maxTravDist, _maxTravDist);
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null) return;

        if (TurretIsControlled == true)
        {
            // _xPos is the x value for the turret, is constantly updated using UpdatePosition()
            // since we're in 3D space, _xPos must be converted to a Vector3 (offsetPos) before the rigidbody can actually attempt to move to that location
            Vector3 offsetPos = new Vector3(_xPos, transform.position.y, transform.position.z);
            _rigidbody.MovePosition(offsetPos);
        }
    }
}
