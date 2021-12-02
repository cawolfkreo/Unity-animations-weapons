using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If you want to make a grenade launcher this
/// script will make an explosive projectile that
/// will explode when touching any platform on a
/// scene, pushing away objects close to the
/// explosion.
/// script by: Camilo Zambrano
/// </summary>
public class GrenadeProjectile : BasicPoolProjectile
{
    /// <summary>
    /// This is the objects that will be affected
    /// by the effects of this projectile
    /// </summary>
    [SerializeField]
    [Tooltip("This is the objects that will be affected")]
    private LayerMask ObjectsLayer;

    /// <summary>
    /// This is how far the effects of gravity
    /// object will extend to.
    /// </summary>
    private float _explosionRadiousEffect;

    /// <summary>
    /// This is the base strength for the gravity.
    /// </summary>
    private float _explosionStrength;

    /// <summary>
    /// Sets the values for the effects of the
    /// gravity on the component.
    /// </summary>
    /// <param name="radiousEffect"></param>
    /// <param name="effectStrength"></param>
    public override void SetAttractValues(float radiousEffect, float effectStrength)
    {
        _explosionRadiousEffect = radiousEffect;
        _explosionStrength = effectStrength;
    }

    /// <summary>
    /// When we collide with something we check if
    /// it is the floor and if it is, we trigger
    /// the explosion effect.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("called!!!");
        if (IsFloor(collision.gameObject.layer))
        {
            Debug.Log("I touched the floor!");
            ExplosionEffect();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// This method creates a gravity effect, it
    /// attracts other objects to it based on the
    /// effects properties of the projectile.
    /// </summary>
    private void ExplosionEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadiousEffect);

        // Optimization for garbagge collection
        Rigidbody colliderRB;
        foreach (Collider collider in colliders)
        {
            if(collider.transform == transform)
            {
                continue;
            }

            if (!IsObjectLayer(collider.gameObject.layer))
            {
                continue;
            }

            collider.TryGetComponent(out colliderRB);
            if (colliderRB == null)
            {
                continue;
            }
            Debug.Log("Exploding!");
            colliderRB.AddExplosionForce(_explosionStrength * 10f, transform.position, _explosionRadiousEffect, 10f);
        }
    }

    /// <summary>
    /// This checks if a layer value is part of the
    /// mask used to detect objects.
    /// </summary>
    /// <param name="layer">The layer to check</param>
    /// <returns>True if it is part of the object layer mask. false otherwise</returns>
    private bool IsObjectLayer(int layer)
    {
        return ObjectsLayer == (ObjectsLayer | (1 << layer));
    }

    /// <summary>
    /// This detects if the given layer is part of
    /// the platforms layer.
    /// </summary>
    /// <param name="layer">The layer to check</param>
    /// <returns>True if it is part of the platform layer mask. false otherwise</returns>
    private bool IsFloor(int layer)
    {
        int floorLayer = LayerMask.NameToLayer("Platforms");
        return floorLayer == layer;
    }

    /// <summary>
    /// Draws the sphere of influence where object
    /// will be attracted to the this game object.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Color gizmoColor = Color.blue;
        gizmoColor.a /= 2;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, _explosionRadiousEffect);
    }
}
