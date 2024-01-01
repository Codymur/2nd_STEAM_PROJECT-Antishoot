using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOrgan : MonoBehaviour
{
    public float BlastRadius;
    public float Force = 50f;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Destroyer Organ = nearbyObject.GetComponent<Destroyer>();
            if (rb != null && Organ != null)
                rb.AddExplosionForce(Force, transform.position, BlastRadius, 0.05f);
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, BlastRadius);

    }
}
