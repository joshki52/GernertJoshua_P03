using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Projectile : MonoBehaviour
{
    private Rigidbody   _rb;
    private Collider    _cl;

    private int         _damage     = 1;
    private float       _moveSpeed  = 2;
    private Vector3     _direction  = new Vector3();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cl = GetComponent<Collider>();
    }

    public void Spawn(Vector3 direction, int damage, float moveSpeed)
    {
        _direction = direction.normalized;
        _damage = damage;
        _moveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 distThisFrame = _direction * _moveSpeed * Time.deltaTime;   // calculate distance traveled this frame
        _rb.MovePosition(_rb.position + distThisFrame);                     // add distance to current (local)
    }
}
