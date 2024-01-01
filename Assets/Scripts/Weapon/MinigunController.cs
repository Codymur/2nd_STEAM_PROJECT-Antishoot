using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce;

    public float FireRate;

    public Camera FpsCamera;

    public float NextTimeToFire;

    public Image CrossHair;

    public LayerMask CanHit;

    public GameObject ImpactEffect;
    public GameObject MuzzleEffect;
    public GameObject BloodSplatter;

    public GameObject MuzzleTip;


    public CameraRecoil CameraRecoilScript;
    public WeaponRecoil RecoilScript;

    public bool CanShoot = true;


    public bool NowShooting = false;

    private void Start()
    {
        RecoilScript = GetComponent<WeaponRecoil>();
        CameraRecoilScript = GameObject.FindGameObjectWithTag("CamRecoil").GetComponent<CameraRecoil>();
        //GunAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.JoystickButton5)) && Time.time >= NextTimeToFire && CanShoot)
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            RecoilScript.Fire();
            CameraRecoilScript.FireUzi();
            Shoot();
        }
        else
        {
            NowShooting = false;
        }
    }

    public void Shoot()
    {
        NowShooting = true;
        GameObject MuzzleOB = Instantiate(MuzzleEffect, MuzzleTip.transform.position, Quaternion.identity);
        Destroy(MuzzleOB, 2f);
        StartCoroutine(CrossHairScale());
        //GunAnim.Play("Shoot", -1, 0f);
        RaycastHit hit;
        if (Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, range, CanHit))
        {

            Target target = hit.transform.GetComponent<Target>();
            Bomb bombScript = hit.transform.GetComponent<Bomb>();
            Destroyer organs = hit.transform.GetComponent<Destroyer>();
            TrashHealth TrashTarget = hit.transform.GetComponent<TrashHealth>();
            HelicopterTarget HeliTarget = hit.transform.GetComponent<HelicopterTarget>();

            if (HeliTarget != null)
            {
                GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                HeliTarget.Explode(damage);
            }

            if (TrashTarget != null)
            {
                GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                TrashTarget.Explode();
            }

            if (organs != null)
            {
                GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (target != null)
            {
                GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (bombScript != null)
            {
                bombScript.Explode();
            }

            GameObject impactOB = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactOB, 2f);
        }

    }

    IEnumerator CrossHairScale()
    {
        CrossHair.rectTransform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        yield return new WaitForSeconds(0.1f);
        CrossHair.rectTransform.localScale = new Vector3(0.045f, 0.045f, 0.045f);
    }
}
