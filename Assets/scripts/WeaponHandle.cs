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
        ProjectileWeapon proyectileWeapon;
        foreach (WeaponPrefab weapon in _weaponData.weapons)
        {
            spawnedWeapon = Instantiate(weapon.weaponModel);
            spawnedWeapon.transform.parent = AttachPoint;
            spawnedWeapon.transform.localPosition = weapon.weaponPositionOffset;
            spawnedWeapon.transform.localRotation = weapon.weaponRotationOffset;
            spawnedWeapon.SetActive(false);
            proyectileWeapon = spawnedWeapon.GetComponent<ProjectileWeapon>();
            proyectileWeapon.StartWeapon(weapon.name,
                                         weapon.projectile,
                                         weapon.projectileSpawnOffset,
                                         weapon.projectileLifeTime,
                                         _weaponData.delayBetweenShoots,
                                         _weaponData.speed,
                                         _weaponData.bulletsPoolSize,
                                         _weaponData.IncreasePoolSize);

            _weaponsDict.Add(weapon.name, spawnedWeapon);
        }
    }
    /// <summary>
    /// When the player touches a weapon we need
    /// to update the weapon he's holding. With
    /// this listener we can detect when the player
    /// is touching a weapon and act accordingly.
    /// </summary>
    /// <param name="other">The collider fired received.</param>
    private void OnTriggerEnter(Collider other)
    {
        string objectName = other.gameObject.name;
        if (!_weaponsDict.ContainsKey(objectName))
            return;

        if (_currentWeaponName != null)
        {
            _weaponsDict[_currentWeaponName].SetActive(false);
        }

        _weaponsDict[objectName].SetActive(true); ;
        _currentWeaponName = objectName;
    }
}
