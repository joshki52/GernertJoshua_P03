using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTemplate_Gun : Weapon
{
    [SerializeField] private WeaponData _weaponData;

    private Vector3 _direction = new Vector3(0, 1, 0);

    private int _damage;
    private float _moveSpeed;
    private float _lifetime;
    private float _cooldown;
    private Projectile _projectile;
    private AudioClip _audioClip;
    private float _volume;

    private void Awake()
    {
        SetupWeapon(_weaponData);
        _direction.Normalize();
    }

    public override void Fire()
    {
        Projectile newProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        newProjectile.Spawn(_direction, _damage, _moveSpeed);
        Destroy(newProjectile.gameObject, _lifetime);   // remove newProjectile gameObject after x seconds
    }

    public void SetupWeapon(WeaponData data)
    {
        // assign data into local variable
        _damage = data.Damage;
        _moveSpeed = data.MoveSpeed;
        _lifetime = data.Lifetime;
        _cooldown = data.Cooldown;
        _projectile = data.Projectile;
        _audioClip = data.AudioClip;
        _volume = data.Volume;
    }
}
