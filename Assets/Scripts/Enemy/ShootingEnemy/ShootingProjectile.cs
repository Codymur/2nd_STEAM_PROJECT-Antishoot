using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 1f;

    private float shootingTimer;

    public Transform Player;

    public PlayerHealth PlayerHealthScript;

    private void Start()
    {
        PlayerHealthScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerHealth>();
        Player = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
    }

    void Update()
    {
        if (PlayerHealthScript.HealthBar.fillAmount > 0f)
        {
            shootingTimer += Time.deltaTime;

            if (shootingTimer >= shootingInterval)
            {
                Shoot();
                shootingTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Apply force to the projectile to make it move forward
            bulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
