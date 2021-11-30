using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the script used for the player to
/// handle weapons in the scene.
/// script by: Camilo Zambrano
/// </summary>
public class WeaponHandle : MonoBehaviour
{
    /// <summary>
    /// This is the point where the character is
    /// going to grab the weapons it uses.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the point where the character is going to grab the weapons it uses. This should be a child object in the character object.")]
    private Transform AttachPoint;

    /// <summary>
    /// This is the data used to create the weapons
    /// the player uses. Since this is an 
    /// scriptable object that loads when the game
    /// starts, it should be fetched from the
    /// game manager.
    /// </summary>
    private WeaponScriptableObject _weaponData;

    /// <summary>
    /// This dictionary is used to store every type
    /// of weapon created for the player;
    /// </summary>
    private Dictionary<string, GameObject> _weaponsDict;

    /// <summary>
    /// This is the name of the weapon currently in
    /// used by the player.
    /// </summary>
    private string _currentWeaponName;

    /// <summary>
    /// When we start, we need to grab the weapon
    /// data from the Game Manager, since this
    /// scriptable object might load on a previous
    /// and not the current one.
    /// </summary>
    private void Start()
    {
        _weaponData = GameManagerSingleton.instance.weaponData;
        _weaponsDict = new Dictionary<string, GameObject>();

        CreateWeapons();
    }

    /// <summary>
    /// This method creates the necessary weapon
    /// objects from the list of weapon prefabs on
    /// the weapon data.
    /// </summary>
    private void CreateWeapons()
    {
        // small optimization for garbagge collection
        GameObject spawnedWeapon;
        ProjectileWeapon proyectile;
        foreach (WeaponPrefab weapon in _weaponData.weapons)
        {
            spawnedWeapon = Instantiate(weapon.weaponModel);
            spawnedWeapon.transform.parent = AttachPoint;
            spawnedWeapon.transform.localPosition = weapon.weaponPositionOffset;
            spawnedWeapon.transform.localRotation = weapon.weaponRotationOffset;
            spawnedWeapon.SetActive(false);
            proyectile = spawnedWeapon.GetComponent<ProjectileWeapon>();
            proyectile.StartWeapon(weapon.projectile,
                                   weapon.projectileSpawnOffset,
                                   _weaponData.delayBetweenShoots,
                                   _weaponData.speed,
                                   _weaponData.bulletsPoolSize,
                                   _weaponData.IncreasePoolSize);

            _weaponsDict.Add(weapon.name, spawnedWeapon);
            _currentWeaponName = weapon.name;
        }
    }
}
