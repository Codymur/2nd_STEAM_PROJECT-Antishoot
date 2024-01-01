using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public float BlastRadius;
    public float Force = 750f;
    public float Damage;

    public GameObject ExplosionSound;


    

    public void Explode()
    {
        Instantiate(ExplosionSound, transform.position, Quaternion.identity);
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Target EnemyScript = nearbyObject.GetComponent<Target>();
            Bomb bombScript = nearbyObject.GetComponent<Bomb>();
            if (rb != null)
            {
                rb.mass = 1f;
                rb.drag = 0;
                rb.AddExplosionForce(Force, transform.position, BlastRadius, 0.05f);
            }
            if (EnemyScript != null)
            {
                EnemyScript.TakeDamage(50f);
            }
        }
        Destroy(gameObject, 0.1f);
    }
}
