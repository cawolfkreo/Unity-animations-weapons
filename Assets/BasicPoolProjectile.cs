using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is made for any projectile that is
/// stored on an object pool. This ensures that
/// after some seconds the projectile will be
/// disabled on it's own.
/// script by: Camilo Zambrano
/// </summary>
public class BasicPoolProjectile : MonoBehaviour
{
    /// <summary>
    /// This is the time the projectile will be
    /// enabled after it is shoot.
    /// </summary>
    public float TimeIsEnabled { get; set; }

    /// <summary>
    /// This is the reference of the disable
    /// projectile corutine, used when the
    /// component is disabled by a third party.
    /// </summary>
    private Coroutine _corutine;

    /// <summary>
    /// This is the flag used to tell if the
    /// disable event was triggered by the corutine
    /// or by an external source.
    /// </summary>
    private bool _disabledMyself;

    /// <summary>
    /// When the projectile is fired we need to
    /// wait for a few seconds 
    /// </summary>
    private void OnEnable()
    {
        _corutine = StartCoroutine(DisableProjectile());
    }

    /// <summary>
    /// This sets the values for any effects
    /// a projectile might have.
    /// </summary>
    /// <param name="radiousEffect">The readious for the effect</param>
    /// <param name="effectStrength">thre strength of the effect</param>
    public virtual void SetAttractValues(float radiousEffect, float effectStrength)
    {
        //Since the base projectile doesn't have effects, this method
        //is up to projectiles that inherit from the class to implement
        //and this is why it has an empty body
    }

    /// <summary>
    /// This is called when the active value of the
    /// game object is set to false. This disables
    /// the component and it is important that the
    /// component clears the corutine if necesary.
    /// </summary>
    private void OnDisable()
    {
        if (_corutine != null || _disabledMyself)
            return;

        StopCoroutine(_corutine);
    }

    /// <summary>
    /// Waits for a number of seconds before
    /// disabling the projectile.
    /// </summary>
    IEnumerator DisableProjectile()
    {
        yield return new WaitForSeconds(TimeIsEnabled);
        _disabledMyself = true;
        gameObject.SetActive(false);
    }
}
