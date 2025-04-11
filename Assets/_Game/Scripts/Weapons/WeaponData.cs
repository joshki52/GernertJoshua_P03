using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData_", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    [SerializeField] int _damage = 1;
    [SerializeField] int _moveSpeed = 4;    // projectile speed (added here instead of prefab for consistency)
    [Range(1, 10)]
    [SerializeField] float _lifetime = 2;   // seconds before projectile is destroyed
    [SerializeField] float _cooldown = 1;   // attack frequency
    [SerializeField] Projectile _projectile = null;

    [Header("Audio")]
    [SerializeField] AudioClip _audioClip = null;
    [SerializeField] float _volume = 0.5f;

    [Header("Collision")]
    [SerializeField] private ContactFilter2D _targetFilter;

    public int Damage                   => _damage;
    public float MoveSpeed              => _moveSpeed;
    public float Lifetime               => _lifetime;
    public float Cooldown               => _cooldown;
    public Projectile Projectile        => _projectile;
    public AudioClip AudioClip          => _audioClip;
    public float Volume                 => _volume;
    public ContactFilter2D TargetFilter => _targetFilter;
}

