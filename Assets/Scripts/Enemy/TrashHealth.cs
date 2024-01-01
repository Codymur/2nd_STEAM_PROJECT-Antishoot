using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrashHealth : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public float BlastRadius;
    public float Force = 750f;
    public GameObject ExplosionSound;

    public ScoreManager scoreManager;
    public int TargetPoint;
    public float MinigunFillerPoints;
    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void Explode()
    {
        scoreManager.AddToScore(TargetPoint);
        scoreManager.MinigunSliderFiller(MinigunFillerPoints);
        Instantiate(ExplosionSound, transform.position, Quaternion.identity);
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Target EnemyScript = nearbyObject.GetComponentInParent<Target>();
            Bomb bombScript = nearbyObject.GetComponent<Bomb>();
            if (rb != null)
            {
                rb.AddExplosionForce(Force, transform.position, BlastRadius, 0.05f);
            }
            if (EnemyScript != null)
            {
                EnemyScript.TakeDamage(25);
            }
        }
        Destroy(gameObject, 0.05f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, BlastRadius);

    }


}
