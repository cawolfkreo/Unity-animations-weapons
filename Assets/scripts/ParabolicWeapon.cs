using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This weapon shoots a projectile that will move
/// in a parabolic motion (X velocity constan and Y
/// velocity will increase cuadratic based on
/// gravity and time) using unity's physics.
/// </summary>
public class ParabolicWeapon : ProjectileWeapon
{
    protected override void OnFireWeapon()
    {
        GameObject bullet = _bulletsPool.GetAnInstance();
        bullet.transform.position = _projectileSpawnPoint.position + _projectileSpawnOffset;

        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        Vector3 projectileForce = _projectileSpawnPoint.up * _speed * 50f;
        bulletRB.AddForce(projectileForce);

        _ = StartCoroutine(ClearShootFlag());
    }
}
