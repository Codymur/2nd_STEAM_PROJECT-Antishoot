using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTriple : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 1f;

    private float shootingTimer;

    public Transform Player;

    public PlayerHealth PlayerHealthScript;

    public GameObject UImage;

    private void Start()
    {
        UImage.SetActive(false);
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
                StartCoroutine(FirstBullet());
                StartCoroutine(SecondBullet());
                StartCoroutine(ThirdBullet());
                shootingTimer = 0f;
            }
        }
    }

    void ShootOne()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRigidbody1 = bullet1.GetComponent<Rigidbody>();
        if (bulletRigidbody1 != null)
        {
            // Apply force to the projectile to make it move forward
            bulletRigidbody1.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
    void ShootTwo()
    {
        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRigidbody2 = bullet2.GetComponent<Rigidbody>();
        if (bulletRigidbody2 != null)
        {
            // Apply force to the projectile to make it move forward
            bulletRigidbody2.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
    void ShootThree()
    {
        GameObject bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRigidbody3 = bullet3.GetComponent<Rigidbody>();
        if (bulletRigidbody3 != null)
        {
            // Apply force to the projectile to make it move forward
            bulletRigidbody3.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator FirstBullet()
    {
        UImage.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        UImage.SetActive(false);
        ShootOne();
    }
    IEnumerator SecondBullet()
    {
        yield return new WaitForSeconds(1.5f);
        ShootTwo();
    }
    IEnumerator ThirdBullet()
    {
        yield return new WaitForSeconds(1.75f);
        ShootThree();
    }
}
