using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// Update is called once per frame and we use
    /// it to apply the effects of the projectile.
    /// </summary>
    void Update()
    {
        GravityEffect();
    }

    /// <summary>
    /// This method creates a gravity effect, it
    /// attracts other objects to it based on the
    /// effects properties of the projectile.
    /// </summary>
    private void GravityEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadiousEffect);

        // Optimization for garbagge collection
        Vector3 forceVector;
        float distance;
        Rigidbody colliderRB;
        foreach (Collider collider in colliders)
        {
            if (!IsObjectLayer(collider.gameObject.layer))
            {
                continue;
            }
        }
    }

    /// <summary>
    /// This checks if a layer value is part of the
    /// mask used to detect objects.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    private bool IsObjectLayer(int layer)
    {
        return ObjectsLayer == (ObjectsLayer | (1 << layer));
    }

    /// <summary>
    /// Draws the sphere of influence where object
    /// will be attracted to the this game object.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Color gizmoColor = Color.green;
        gizmoColor.a /= 2;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, _explosionRadiousEffect);
    }
}
