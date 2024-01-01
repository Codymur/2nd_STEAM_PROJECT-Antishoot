using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketJumpBullet : MonoBehaviour
{
    public float BlastRadius;
    public float Force = 750f;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(-Force, transform.position, BlastRadius, 0.05f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, BlastRadius);

    }
}
