using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    public float BlastRadius;
    public float Force;

    public Rigidbody PlayerRb;

    private void Start()
    {
        PlayerRb = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "MainPlayerTag")
        {
            PlayerRb.AddExplosionForce(Force, transform.position, BlastRadius, 2f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, BlastRadius);

    }
}
