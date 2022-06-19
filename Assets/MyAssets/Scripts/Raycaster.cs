using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private GameObject currHitObject;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform castOrigin;
    [SerializeField] private float castRadius = 1f;
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private Vector3 castDirection = Vector3.forward;

    [SerializeField] private float currHitDistance;

    private void FixedUpdate()
    {
        castDirection = transform.forward;
        Sphere(out var interactingWith);
    }

    public bool Sphere(out GameObject interactingWith)
    {
        if (Physics.SphereCast(castOrigin.position, castRadius, castDirection, out var hit, maxDistance, layerMask))
        {
            currHitObject = hit.transform.gameObject;
            currHitDistance = hit.distance;
            interactingWith = currHitObject;
            return true;
        }
        else
        {
            currHitDistance = maxDistance;
            currHitObject = null;
            interactingWith = null;
            return false;
        }
    }
    
    private void OnDrawGizmos()
    {
        var pos = castOrigin.position;
        Gizmos.color = new Color(1, 0, 0,0.3f);
        Debug.DrawLine(pos, pos + castDirection * currHitDistance, Color.red);
        Gizmos.DrawWireSphere(pos + castDirection * currHitDistance, castRadius);
    }
    
}
