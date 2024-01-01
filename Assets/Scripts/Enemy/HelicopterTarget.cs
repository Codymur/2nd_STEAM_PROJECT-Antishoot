using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterTarget : MonoBehaviour
{
    public float health = 50f;
    public GameObject ExplosionEffect;
    public float BlastRadius;
    public float Force = 750f;
    public GameObject ExplosionSound;

    public ScoreManager scoreManager;
    public int TargetPoint;

    private Outline outline;
    public bool isHeaderEnemy;
    public GameObject HeaderParent;
    public GameObject Parts;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        outline = gameObject.GetComponentInChildren<Outline>();


        if (isHeaderEnemy)
        {
            HeaderParent = this.transform.parent.gameObject;
        }
    }

    public void Explode(float amount)
    {
        StartCoroutine(TakingDamageEffect());
        health -= amount;
        if (health <= 0f)
        {
            Instantiate(Parts, transform.position, Quaternion.identity);
            scoreManager.AddToScore(TargetPoint);
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
                    rb.mass = 1f;
                    rb.drag = 0;
                    rb.AddExplosionForce(Force, transform.position, BlastRadius, 0.05f);
                }
                if (EnemyScript != null)
                {
                    EnemyScript.TakeDamage(25);
                }
            }
            if (!isHeaderEnemy)
            {
                Die();
            }
            else if (isHeaderEnemy)
            {
                HeaderDie();
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, BlastRadius);

    }

    IEnumerator TakingDamageEffect()
    {
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = outline.OutlineWidth + 2f;
        yield return new WaitForSeconds(0.3f);
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = outline.OutlineWidth - 2f;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void HeaderDie()
    {
        Destroy(HeaderParent);
    }
}
