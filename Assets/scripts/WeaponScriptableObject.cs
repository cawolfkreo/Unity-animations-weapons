using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the template for the weapon scriptable
/// objects that are used to store and load
/// properties for weapons.
/// script by: Camilo Zambrano
/// </summary>
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Shooting properties")]
    /// <summary>
    /// This is the time in seconds between each
    /// shot.
    /// </summary>
    [Tooltip("This is the time in seconds between each shot.")]
    [Range(0.02f, 200)]
    public float delayBetweenShoots = 0.5f;

    /// <summary>
    /// This is the initial speed used for
    /// projectile type weapons.
    /// </summary>
    [Tooltip("This is the initial speed used for projectile type weapons.")]
    public float speed = 50f;

    [Header("Pool properties")]

    /// <summary>
    /// This is the ammount of bullet objects that
    /// will be instatiated for the weapon's pool.
    /// </summary>
    [Tooltip("This is the ammount of bullet objects that will be instatiated for the weapon's pool.")]
    public int bulletsPoolSize = 32;

    /// <summary>
    /// This flag is used to tell the bullet pool
    /// if it should increase it's size when it ran
    /// out of objects or reuse old objects.
    /// </summary>
    [Tooltip("This flag is used to tell the bullet pool if it should increase it's size when it ran out of objects or reuse old objects.")]
    public bool IncreasePoolSize = false;

    /// <summary>
    /// This is the list of weapons that the
    /// player will use during gameplay.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the list of weapons that the player will use during gameplay.")]
    public WeaponPrefab[] weapons;

    /// <summary>
    /// This validates that attributes for this
    /// object stays on a certain range and don't
    /// have disallowed values.
    /// </summary>
    private void OnValidate()
    {
        speed = Mathf.Clamp(speed, 0f, float.MaxValue);
        bulletsPoolSize = (int)Mathf.Clamp(bulletsPoolSize, 0f, int.MaxValue);
        delayBetweenShoots = Mathf.Clamp(delayBetweenShoots, 0f, float.MaxValue);
    }
}

/// <summary>
/// This is the weapon prefab that will be used by
/// player when it equips it.
/// </summary>
[System.Serializable]
public class WeaponPrefab
{
    /// <summary>
    /// This is the name of the weapon.
    /// </summary>
    [Tooltip("This is the name of the weapon.")]
    public string name;

    /// <summary>
    /// This is the weapon prefab that will be
    /// instanciated when the player equips it.
    /// </summary>
    [Tooltip("This is the weapon prefab that will be instanciated when the player equips it.")]
    public GameObject weaponModel;

    /// <summary>
    /// This is the weapon position offset that
    /// will be used to offset the weapon from the
    /// attachment point.
    /// </summary>
    [Tooltip("This is the weapon position offset that will be used to offset the weapon from the attachment point.")]
    public Vector3 weaponPositionOffset;

    /// <summary>
    /// This is the weapon rotation offset that
    /// will be used to offset the weapon from the
    /// attachment point.
    /// </summary>
    [Tooltip("This is the weapon offset that will be used to offset the weapon from the attachment point.")]
    public Quaternion weaponRotationOffset;

    [Header("Projectile settings")]
    /// <summary>
    /// This is the projectile used for projectile
    /// based weapons.
    /// </summary>
    [Tooltip("This is the projectile used for projectile based weapons.")]
    public GameObject projectile;

    /// <summary>
    /// This is the position offset where the
    /// projectile will spawn from the weapon.
    /// </summary>
    [Tooltip("This is the position offset where the projectile will spawn from the weapon.")]
    public Vector3 projectileSpawnOffset;

    /// <summary>
    /// This is the projectile life time (in
    /// seconds) or the time it will be enabled
    /// after it was fired. Once this time passes
    /// the projectile will disable itself.
    /// </summary>
    [Tooltip("This is the projectile life time (in seconds) or the time it will be enabled after it was fired. Once this time passes the projectile will disable itself.")]
    public float projectileLifeTime;
}