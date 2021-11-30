using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class for all projectile
/// weapons, it needs to be implemented for every
/// type of weapons.
/// script by: Camilo Zambrano
/// </summary>
public abstract class ProjectileWeapon : MonoBehaviour
{
    /// <summary>
    /// This is the base position where the
    /// projectile will spawn from the weapon.
    /// </summary>
    protected Transform _projectileSpawnPoint;

    /// <summary>
    /// This is the queue where the object pool
    /// will store the bullets for the gun
    /// </summary>
    protected ObjectPool _bulletsPool;

    /// <summary>
    /// This is the position offset where the
    /// projectile will spawn from the weapon.
    /// </summary>
    protected Vector3 _projectileSpawnOffset;

    /// <summary>
    /// This is the initial speed used for the
    /// projectiles fired.
    /// </summary>
    protected float _speed;

    /// <summary>
    /// This flag stores if the player is allowed
    /// to shoot at the time of clicking or not.
    /// </summary>
    private bool _canShoot;

    /// <summary>
    /// This is the time (in seconds) the player
    /// needs to wait before firing again.
    /// </summary>
    private float _delayBetweenShoots;

    /// <summary>
    /// This method is used to set the properties of the
    /// projectile weapon. And creates the object
    /// pool for the bullets
    /// </summary>
    /// <param name="projectile">The projectile prefab the gun will shoot</param>
    /// <param name="delayBetweenShoots">The time between each shot</param>
    /// <param name="speed">How fast the projectile moves when shot</param>
    /// <param name="bulletsPoolSize">The pool size for the projectiles</param>
    /// <param name="IncreasePoolSize">Wether or not the pool size is allowed to grow</param>
    public virtual void StartWeapon(GameObject projectile,
                                    Vector3 projectileSpawnOffset,
                                    float delayBetweenShoots,
                                    float speed,
                                    int bulletsPoolSize,
                                    bool IncreasePoolSize)
    {
        _bulletsPool = new ObjectPool(projectile, bulletsPoolSize, IncreasePoolSize);
        _canShoot = true;
        _delayBetweenShoots = delayBetweenShoots;
        _speed = speed;
        _projectileSpawnOffset = projectileSpawnOffset;
        _projectileSpawnPoint = transform.GetChild(0);
    }

    /// <summary>
    /// This is the method called when the weapon
    /// fires a projectile.
    /// </summary>
    protected abstract void OnFireWeapon();

    /// <summary>
    /// We use this to detect when the player is
    /// firing the weapon.
    /// </summary>
    private void Update()
    {
        bool click = Input.GetButton("Fire1");

        if (click && _canShoot)
        {
            OnFireWeapon();
            _canShoot = false;
        }
    }

    /// <summary>
    /// This corutine clears the shooting flag to
    /// allow the player to fire again.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ClearShootFlag()
    {
        yield return new WaitForSeconds(_delayBetweenShoots);
        _canShoot = true;
    }
}
