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
        Debug.Log("I'll wait and kill myself");
        _corutine = StartCoroutine(DisableProjectile());
    }

    /// <summary>
    /// This is called when the active value of the
    /// game object is set to false. This disables
    /// the component and it is important that the
    /// component clears the corutine if necesary.
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("I'm being killed!");
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
        Debug.Log("guess I'll die!");
    }
}
