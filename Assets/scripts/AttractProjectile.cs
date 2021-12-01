using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script adds a gravity object that attracts
/// other objects around it and makes them orbit
/// the game object this script is attached to.
/// script by: Camilo Zambrano
/// </summary>
public class AttractProjectile : BasicPoolProjectile
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
    private float _gravityRadiousEffect;

    /// <summary>
    /// This is the base strength for the gravity.
    /// </summary>
    private float _gravityStrength;

    /// <summary>
    /// Sets the values for the effects of the
    /// gravity on the component.
    /// </summary>
    /// <param name="radiousEffect"></param>
    /// <param name="effectStrength"></param>
    public override void SetAttractValues(float radiousEffect, float effectStrength)
    {
        _gravityRadiousEffect = radiousEffect;
        _gravityStrength = effectStrength;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _gravityRadiousEffect);

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

            forceVector = collider.transform.position;
            distance = Vector3.Distance(transform.position, forceVector);
            if(distance == 0)
            {
                continue;
            }
            forceVector -= transform.position;      //The from this object to the collider.
            forceVector /= distance * distance;     //Further away objects will have less force applied.
            forceVector *= _gravityStrength * 10;   //We increase the force that will be applied.
            forceVector *= -1;                      //We make the vector point from the collision to this gameObject.

            collider.TryGetComponent(out colliderRB);
            colliderRB?.AddForce(forceVector);      //We add the force to the rigid body.
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
        Gizmos.DrawSphere(transform.position, _gravityRadiousEffect);
    }
}
